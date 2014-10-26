using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Mono.Cecil;
using AllureCSharpCommons;
using Mono.Collections.Generic;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System.Reflection;

namespace AllureAttachmentWeaver.Behaviors
{
    /// <summary>
    /// Weaves a call to AllureCSharpCommons.Attachments.Add with the result of a method marked
    /// with the AllureAttachmentAttribute.
    /// </summary>
    public class AttachmentWeaver : IMethodWeaver
    {
        private ILog mLogger = LogManager.GetLogger(typeof(AttachmentWeaver));

        private bool ShouldWeaveMethod(Mono.Cecil.MethodDefinition method)
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

        public void Weave(Mono.Cecil.MethodDefinition method)
        {
            if (!ShouldWeaveMethod(method)) return;

            method.Body.SimplifyMacros();

            ClearReturnStatments(method);

            LoadContext(method);

            CallAddMethod(method);

            Return(method);

            method.Body.OptimizeMacros();
        }

        private static void Return(Mono.Cecil.MethodDefinition method)
        {
            ILProcessor ilProcessor = method.Body.GetILProcessor();
            ilProcessor.Append(Instruction.Create(OpCodes.Ret));
        }

        private static ILProcessor CallAddMethod(Mono.Cecil.MethodDefinition method)
        {
            ILProcessor ilProcessor = method.Body.GetILProcessor();
            // the module already uses the 'AllureAttachmentAttribute' so there is no need to import it into the module
            MethodInfo addAttachmentMethod = typeof(Attachments).GetMethod(
                "Add",
                BindingFlags.Public | BindingFlags.Static,
                null,
                new Type[] { typeof(object), typeof(object) },
                null);

            MethodReference addAttachment = method.Module.Import(addAttachmentMethod);

            //EmitPrintMessage(ilProcessor, "About to call Add...");
            ilProcessor.Append(Instruction.Create(OpCodes.Call, addAttachment));
            return ilProcessor;
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

        private static void LoadContext(MethodDefinition method)
        {
            ILProcessor ilProcessor = method.Body.GetILProcessor();

            if (method.ReturnType.IsValueType)
            {
                ilProcessor.Append(Instruction.Create(OpCodes.Box, method.ReturnType));
            }

            Instruction loadContextInstruction;

            if (method.IsStatic)
            {
                loadContextInstruction = Instruction.Create(OpCodes.Ldnull);
            }
            else
            {
                // arg0 is the argument used to pass the instance referenced by 'this'
                loadContextInstruction = Instruction.Create(OpCodes.Ldarg_0);
            }

            ilProcessor.Append(loadContextInstruction);
        }

        private static void EmitPrintMessage(ILProcessor ilProcessor, string message)
        {
            // this should probably be use the Trace class...
            MethodInfo writeLineMethod = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) });
            MethodReference writeLine = ilProcessor.Body.Method.Module.Import(writeLineMethod);

            ilProcessor.Append(Instruction.Create(OpCodes.Ldstr, message));
            ilProcessor.Append(Instruction.Create(OpCodes.Call, writeLine));
        }
    }
}
