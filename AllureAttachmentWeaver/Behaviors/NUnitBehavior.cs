using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Linq;
using AllureCSharpCommons;
using AllureCSharpCommons.Events;

namespace AllureAttachmentWeaver
{
    public class NUnitAttachmentWeaver : BaseBehaviorWeaver
    {
        public override string AssemblyName
        {
            get { return "nunit.framework"; }
        }
        
        /// <summary>
        /// Weaves the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        public override void Weave(MethodDefinition method)
        {
            AssemblyNameReference assemblyNameReference = GetTestingAssemblyNameReference(method.Module);
            
            if (assemblyNameReference == null)
            {
                PopUnusedReturnValue(method);
                return;
            }
            
            ModuleDefinition module = method.Module;
            
            VariableDefinition attachmentEvent = new VariableDefinition(module.Import(typeof(MakeAttachmentEvent)));
            method.Body.Variables.Add(attachmentEvent);
            
            MethodReference lifecycleGetter = module.Import(typeof(Allure).GetProperty("Lifecycle", typeof(Allure)).GetGetMethod());
            
            string title = GetAttachmentTitle(method);
            string mimeType = GetAttachmentMimeType(method);
            
            ILProcessor il = method.Body.GetILProcessor();
            
            if (method.ReturnType.FullName == typeof(string).FullName)
            {
                MethodReference toBinary = module.Import(typeof(Attachments).GetMethod("ToBinary", new []{ typeof(string) }));
                il.Append(Instruction.Create(OpCodes.Call, toBinary));
            }
            
            Instruction loadTitle = (title != null) ? Instruction.Create(OpCodes.Ldstr, title) : Instruction.Create(OpCodes.Ldnull);
            
            il.Append(loadTitle);
            
            il.Append(Instruction.Create(OpCodes.Ldstr, mimeType));
            
            MethodReference attachmentEventCtor = module.Import(typeof(MakeAttachmentEvent).GetConstructor(new [] { typeof(byte[]), typeof(string), typeof(string) }));
            il.Append(Instruction.Create(OpCodes.Newobj, attachmentEventCtor));
            il.Append(Instruction.Create(OpCodes.Stloc, attachmentEvent));
            
            il.Append(Instruction.Create(OpCodes.Call, lifecycleGetter));
            il.Append(Instruction.Create(OpCodes.Ldloc, attachmentEvent));
            
            MethodReference fireEvent = module.Import(typeof(Allure).GetMethod("Fire", new [] { typeof(ITestCaseEvent) }));
            
            il.Append(Instruction.Create(OpCodes.Callvirt, fireEvent));
        }
    }
}

