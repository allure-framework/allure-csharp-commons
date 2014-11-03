using System;
using Mono.Cecil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;
using Mono.Cecil.Cil;
using System.Reflection;
using AllureCSharpCommons;
using System.Linq;
using log4net;

namespace AllureAttachmentWeaver
{
    public class AttachmentWeaver : IMethodWeaver
    {
        private ILog mLogger = LogManager.GetLogger(typeof(AttachmentWeaver));
        
        public AttachmentWeaver()
        {
            LastReturnInstruction = Instruction.Create(OpCodes.Ret);   
        }
        
        protected virtual bool ShouldWeaveMethod(MethodDefinition method)
        {
            if (!method.HasCustomAttributes)
            {
                mLogger.Debug("Skipping " + method.FullName + " because it doesnt have custom attributes.");
                return false;
            }
            
            if (method.ReturnType.FullName == "System.Void")
            {
                mLogger.Debug("Skipping " + method.FullName + " because it doesnt have a return type.");
                return false;
            }

            mLogger.Debug("Found method '" + method.FullName + "'");

            foreach (CustomAttribute methodAttribute in method.CustomAttributes)
            {
                mLogger.Debug("Found attribute '" + methodAttribute.AttributeType.FullName + "'");

                if (methodAttribute.AttributeType.FullName == typeof(AllureAttachmentAttribute).FullName)
                {
                    return true;
                }
            }

            return false;
        }
        
        protected Instruction LastReturnInstruction { get; private set; }

        public virtual void Weave(MethodDefinition method)
        {
            if (!ShouldWeaveMethod(method)) return;

            method.Body.SimplifyMacros();

            ClearReturnStatments(method);

            DuplicateReturnValue(method);
            
            WeaveEventCallingBehavior(method);

            DuplicateReturnValue(method);
            
            WeaveMSTestBehavior(method);
            
            DuplicateReturnValue(method);
            
            WeaveNUnitBehavior(method);
            
            Return(method);

            method.Body.OptimizeMacros();
        }
        
        private void WeaveEventCallingBehavior(MethodDefinition method)
        {
            IBehaviorWeaver eventBehavior = new RaiseEventBehavior();
            eventBehavior.Weave(method);
        }
        
        private void WeaveMSTestBehavior(MethodDefinition method)
        {
            IBehaviorWeaver mstestBehavior = new MSTestAttachmentWeaver();
            mstestBehavior.Weave(method);
        }
        
        private void WeaveNUnitBehavior(MethodDefinition method)
        {
            IBehaviorWeaver nunitBehavior = new NUnitAttachmentWeaver();
            nunitBehavior.Weave(method);
        }
        
        private void ClearReturnStatments(Mono.Cecil.MethodDefinition method)
        {
            Collection<Instruction> instructions = method.Body.Instructions;

            Instruction lastInstruction = instructions[instructions.Count - 1];

            if (lastInstruction.OpCode != OpCodes.Ret)
                throw new Exception("The last instruction wasn't OpCodes.Ret.");

            // all the current br instructions (short and long) are valid because
            // the target is the same, its content changed but the location is the same
            // either way the call to OptimizeMacros will fix the short and long br'assemblyResolver

            for (int i = 0; i < instructions.Count - 1; i++)
            {
                if (instructions[i].OpCode == OpCodes.Ret)
                {
                    // always use long, will be optimized to short when calling OptimizeMacros
                    instructions[i] = Instruction.Create(OpCodes.Br, lastInstruction);
                }
            }

            // at this point we have the return value on the stack so instead of the old Ret
            // opcode we duplicate the return value on the stack for future use
            instructions[instructions.Count - 1] = Instruction.Create(OpCodes.Nop);
        }
        
        private void Return(MethodDefinition method)
        {
            ILProcessor ilProcessor = method.Body.GetILProcessor();
            ilProcessor.Append(LastReturnInstruction);
        }
        
        private void DuplicateReturnValue(MethodDefinition method)
        {
            ILProcessor ilProcessor = method.Body.GetILProcessor();
            ilProcessor.Append(Instruction.Create(OpCodes.Dup));
        }
    }
}

