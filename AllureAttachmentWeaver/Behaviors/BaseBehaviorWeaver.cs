using System;
using Mono.Cecil;
using Mono.Collections.Generic;
using System.Linq;
using AllureCSharpCommons;
using Mono.Cecil.Cil;
using System.Reflection;

namespace AllureAttachmentWeaver
{
    public abstract class BaseBehaviorWeaver : IBehaviorWeaver
    {
        protected const int INDEX_OF_MIMETYPE_ARGUMENT = 0;
        protected const int INDEX_OF_TITLE_ARGUMENT = 1;
        
        public abstract void Weave(MethodDefinition method);
        
        public abstract string AssemblyName { get; }
        
        protected AssemblyNameReference GetTestingAssemblyNameReference(ModuleDefinition module)
        {
            // assemlby names should be treated as case sensative. 
            // http://msdn.microsoft.com/en-us/library/k8xx4k69.aspx
            
            return module.AssemblyReferences.FirstOrDefault<AssemblyNameReference>(_ => _.Name == AssemblyName );
        }
        
        protected void PopUnusedReturnValue(MethodDefinition method)
        {
            method.Body.GetILProcessor().Append(Instruction.Create(OpCodes.Pop));   
        }
        
        protected string GetAttachmentMimeType(MethodDefinition method)
        {
            return GetAllureAttachmentArgument<string>(method, INDEX_OF_MIMETYPE_ARGUMENT);
        }
        
        protected string GetAttachmentTitle(MethodDefinition method)
        {
            return GetAllureAttachmentArgument<string>(method, INDEX_OF_TITLE_ARGUMENT); 
        }
        
        private T GetAllureAttachmentArgument<T>(MethodDefinition method, int argumentPosition)
        {
            CustomAttribute attachmentAttribute = method.CustomAttributes.First(_ => _.AttributeType.FullName == typeof(AllureAttachmentAttribute).FullName);

            Collection<CustomAttributeArgument> constructorArguments = attachmentAttribute.ConstructorArguments;

            // this argument wasn't supplied.
            if (constructorArguments.Count <= argumentPosition)
                return default(T);

            CustomAttributeArgument argument = constructorArguments[argumentPosition];

            if (argument.Type.FullName != typeof(T).FullName)
                return default(T);

            return (T)argument.Value;
        }

        protected void EmitPrintMessage(ILProcessor ilProcessor, string message)
        {
            // this should probably be use the Trace class...
            MethodInfo writeLineMethod = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) });
            MethodReference writeLine = ilProcessor.Body.Method.Module.Import(writeLineMethod);

            ilProcessor.Append(Instruction.Create(OpCodes.Ldstr, message));
            ilProcessor.Append(Instruction.Create(OpCodes.Call, writeLine));
        }
    }
}

