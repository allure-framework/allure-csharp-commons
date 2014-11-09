using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Reflection;
using AllureCSharpCommons;

namespace AllureAttachmentWeaver
{
    public class RaiseEventBehavior : BaseBehaviorWeaver
    {
        public override void Weave(MethodDefinition method)
        {
            LoadContext(method);

            CallAddMethod(method);
        }
        
        public override string AssemblyName
        {
            get
            {
                return "Raising Event Behavior";
            }
        }
        
        private void CallAddMethod(MethodDefinition method)
        {
            ILProcessor ilProcessor = method.Body.GetILProcessor();
            // the module already uses the 'AllureAttachmentAttribute' so there is no need to import it into the module
            MethodInfo addAttachmentMethod = typeof(Attachments).GetMethod(
                "OnAdded",
                BindingFlags.Public | BindingFlags.Static,
                null,
                new Type[] { typeof(string), typeof(string), typeof(object), typeof(object) },
                null);

            MethodReference addAttachment = method.Module.Import(addAttachmentMethod);

            //EmitPrintMessage(ilProcessor, "About to call Add...");
            ilProcessor.Append(Instruction.Create(OpCodes.Call, addAttachment));

            Instruction nop = Instruction.Create(OpCodes.Nop);
            
            ilProcessor.Append(Instruction.Create(OpCodes.Brfalse, nop));
                
            ilProcessor.Append(Instruction.Create(OpCodes.Ret));
            
            ilProcessor.Append(nop);
        }

        private void LoadContext(MethodDefinition method)
        {
            ILProcessor ilProcessor = method.Body.GetILProcessor();

            string mimeType = GetAttachmentMimeType(method);

            Instruction loadMimeTypeInstruction = mimeType != null ? Instruction.Create(OpCodes.Ldstr, mimeType) : Instruction.Create(OpCodes.Ldnull);

            string title = GetAttachmentTitle(method);

            Instruction loadTitleInstruction = title != null ? Instruction.Create(OpCodes.Ldstr, title) : Instruction.Create(OpCodes.Ldnull);

            ilProcessor.Append(loadMimeTypeInstruction);
            ilProcessor.Append(loadTitleInstruction);            

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
    }
}

