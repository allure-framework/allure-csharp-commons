using System;
using Mono.Cecil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;
using Mono.Cecil.Cil;

namespace AllureAttachmentWeaver
{
    public abstract class BaseWeaver : IMethodWeaver
    {
        protected virtual bool ShouldWeaveMethod(MethodDefinition method)
        {
            return true;
        }
        
        protected abstract void CustomWeave(MethodDefinition method);

        public virtual void Weave(MethodDefinition method)
        {
            if (!ShouldWeaveMethod(method)) return;

            method.Body.SimplifyMacros();

            ClearReturnStatments(method);

            CustomWeave(method);

            Return(method);

            method.Body.OptimizeMacros();
        }
        
        private static void ClearReturnStatments(Mono.Cecil.MethodDefinition method)
        {
            Collection<Instruction> instructions = method.Body.Instructions;

            Instruction lastInstruction = instructions[instructions.Count - 1];

            if (lastInstruction.OpCode != OpCodes.Ret)
                throw new Exception("The last instruction wasn't OpCodes.Ret.");

            // all the current br instructions (short and long) are valid because
            // the target is the same, its content changed but the location is the same
            // either way the call to OptimizeMacros will fix the short and long br's

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
            instructions[instructions.Count - 1] = Instruction.Create(OpCodes.Dup);
        }
        
        private static void Return(Mono.Cecil.MethodDefinition method)
        {
            ILProcessor ilProcessor = method.Body.GetILProcessor();
            ilProcessor.Append(Instruction.Create(OpCodes.Ret));
        }
    }
}

