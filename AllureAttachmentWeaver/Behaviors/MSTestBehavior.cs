using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using AllureCSharpCommons;

namespace AllureAttachmentWeaver
{
    public class MSTestAttachmentWeaver : BaseBehaviorWeaver
    {
        private AssemblyDefinition mUnitTestingAssembly;
        private TypeReference mTestContextType;

        const string TEXT_MIME_TYPE = "text/plain";

        public override void Weave(MethodDefinition method)
        {
            LoadUnitTestingAssembly(method.Module);

            PropertyDefinition testContextProperty = GetTestContextProperty(method.DeclaringType);

            if (testContextProperty == null)
            {
                testContextProperty = AddTestContextProperty(method.DeclaringType, "TestContext");
            }

            WriteResultFile(method, testContextProperty.GetMethod);
        }

        private void WriteResultFile(MethodDefinition method, MethodReference testContextGetter)
        {
            VariableDefinition filePath = new VariableDefinition("filePath" + DateTime.Now.Ticks.ToString(), method.Module.Import(typeof(string)));
            method.Body.Variables.Add(filePath);

            ILProcessor il = method.Body.GetILProcessor();

            MethodReference writeFile;

            string mimeType = GetAttachmentMimeType(method);
            if (string.Compare(mimeType, TEXT_MIME_TYPE, true) == 0)
            {
                writeFile = method.Module.Import(typeof(Attachments).GetMethod("WriteText", new[] { typeof(string) }));
            }
            else
            {
                writeFile = method.Module.Import(typeof(Attachments).GetMethod("WriteBinary", new[] { typeof(byte[]), typeof(string) }));
                il.Append(Instruction.Create(OpCodes.Ldstr, mimeType));
            }

            il.Append(Instruction.Create(OpCodes.Call, writeFile));

            il.Append(Instruction.Create(OpCodes.Stloc, filePath));

            il.Append(Instruction.Create(OpCodes.Ldarg_0));
            il.Append(Instruction.Create(OpCodes.Call, testContextGetter));
            il.Append(Instruction.Create(OpCodes.Ldloc, filePath));
            
            MethodReference addResultFile = GetAddResultFileMethodReference(method.Module);

            il.Append(Instruction.Create(OpCodes.Callvirt, addResultFile));
        }

        private MethodReference GetAddResultFileMethodReference(ModuleDefinition usedModule)
        {
            TypeDefinition testContextType = mTestContextType.Resolve();
            return usedModule.Import(testContextType.Methods.First<MethodDefinition>(_ => _.Name == "AddResultFile"));
        }

        private PropertyDefinition GetTestContextProperty(TypeDefinition type)
        {
            foreach (PropertyDefinition property in type.Properties.ToList())
            {
                if (property.PropertyType.FullName == "Microsoft.VisualStudio.TestTools.UnitTesting.TestContext")
                    return property;
            }
            
            return null;
        }

        private PropertyDefinition AddTestContextProperty(TypeDefinition type, string name)
        {
            PropertyDefinition testContextProperty = new PropertyDefinition(name, PropertyAttributes.None, mTestContextType);
            FieldDefinition field = AddBackingField(type, name);
            testContextProperty.GetMethod = AddGetMethod(type, name, field);
            testContextProperty.SetMethod = AddSetMethod(type, name, field);
            type.Properties.Add(testContextProperty);
            return testContextProperty;
        }
                
        private FieldDefinition AddBackingField(TypeDefinition type, string name)
        {
            FieldDefinition field = new FieldDefinition("m_" + name + "<>" + DateTime.Now.Ticks, Mono.Cecil.FieldAttributes.Private, mTestContextType);
            type.Fields.Add(field);
            return field;
        }

        private MethodDefinition AddGetMethod(TypeDefinition type, string name, FieldDefinition backingField)
        {
            MethodDefinition get = new MethodDefinition("get_" + name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, mTestContextType);
            ILProcessor il = get.Body.GetILProcessor();
            il.Append(Instruction.Create(OpCodes.Ldarg_0));
            il.Append(Instruction.Create(OpCodes.Ldfld, backingField));
            il.Append(Instruction.Create(OpCodes.Ret));
            get.SemanticsAttributes = MethodSemanticsAttributes.Getter;
            type.Methods.Add(get);
            return get;
        }

        private MethodDefinition AddSetMethod(TypeDefinition type, string name, FieldDefinition backingField)
        {
            MethodDefinition set = new MethodDefinition("set_" + name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, type.Module.Import(typeof(void)));
            ILProcessor il = set.Body.GetILProcessor();
            il.Append(Instruction.Create(OpCodes.Ldarg_0));
            il.Append(Instruction.Create(OpCodes.Ldarg_1));
            il.Append(Instruction.Create(OpCodes.Stfld, backingField));
            il.Append(Instruction.Create(OpCodes.Ret));
            set.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, mTestContextType));
            set.SemanticsAttributes = MethodSemanticsAttributes.Setter;
            type.Methods.Add(set);
            return set;
        }

        private void LoadUnitTestingAssembly(ModuleDefinition module)
        {
            AssemblyNameReference assemblyNameReference = module.AssemblyReferences.FirstOrDefault<AssemblyNameReference>(_ => _.Name == "Microsoft.VisualStudio.QualityTools.UnitTestFramework");
            DefaultAssemblyResolver assemblyResolver = new DefaultAssemblyResolver();
            
            mUnitTestingAssembly = assemblyResolver.Resolve(assemblyNameReference);

            mTestContextType = module.Import(mUnitTestingAssembly.MainModule.GetTypes().First<TypeDefinition>(_ => _.FullName == "Microsoft.VisualStudio.TestTools.UnitTesting.TestContext"));
        }
    }
}

