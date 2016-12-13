using System;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using AllureCSharpCommons;
using AllureCSharpCommons.Utils;
using System.IO;

namespace AllureAttachmentWeaver
{
    public class MSTestAttachmentWeaver : BaseBehaviorWeaver
    {
        private AssemblyDefinition mUnitTestingAssembly;
        private TypeReference mTestContextType;
        private MethodReference mAddResultFileMethodReference = null;
        private MethodReference mTestResultsDirectoryGetter = null;
        
        public override string AssemblyName
        {
            get { return "Microsoft.VisualStudio.QualityTools.UnitTestFramework"; }
        }
        
        public override void Weave(MethodDefinition method)
        {
            if (!LoadUnitTestingAssembly(method.Module))
            {
                PopUnusedReturnValue(method);                    
                return;
            }

            PropertyDefinition testContextProperty = GetTestContextProperty(method.DeclaringType);

            if (testContextProperty == null)
            {
                TestContextWeaver testContextWeaver = new TestContextWeaver(method.DeclaringType, "TestContext", mTestContextType);
                testContextProperty = testContextWeaver.AddTestContextProperty();
            }

            WriteResultFile(method, testContextProperty.GetMethod);
        }

        private void WriteResultFile(MethodDefinition method, MethodReference testContextGetter)
        {
            // there's a bug with calling AddResultFile in parallel:
            // https://connect.microsoft.com/VisualStudio/feedback/details/1062039/value-cannot-be-null-parameter-name-path-in-test-result-when-running-tests-in-parallel-and-adding-files-to-the-test-context-using-addresultfile
            // so instead of calling AddResultFile the file is written directly to TestResultsDirectory
                                    
            //VariableDefinition filePath = new VariableDefinition("filePath" + DateTime.Now.Ticks.ToString(), method.Module.Import(typeof(string)));
            //method.Body.Variables.Add(filePath);
            
            ILProcessor il = method.Body.GetILProcessor();

            MethodReference writeFile;
            MethodReference createDirectory = method.Module.Import(typeof(Directory).GetMethod("CreateDirectory", new [] { typeof(string)}));
            MethodReference getTestResultsDirectory = GetTestResultsDirectoryGetter(method.Module);
            string mimeType = GetAttachmentMimeType(method);            
            Type argumentType;
            
            argumentType = MimeTypes.IsText(mimeType) ? typeof(string) : typeof(byte[]);
            
            writeFile = method.Module.Import(typeof(Attachments).GetMethod("Write", new[] { argumentType, typeof(string), typeof(string) }));

            il.Append(Instruction.Create(OpCodes.Ldarg_0));
            il.Append(Instruction.Create(OpCodes.Call, testContextGetter));
            il.Append(Instruction.Create(OpCodes.Callvirt, getTestResultsDirectory));
            il.Append(Instruction.Create(OpCodes.Call, createDirectory));
            il.Append(Instruction.Create(OpCodes.Pop));
            
            il.Append(Instruction.Create(OpCodes.Ldstr, mimeType));
            il.Append(Instruction.Create(OpCodes.Ldarg_0));
            il.Append(Instruction.Create(OpCodes.Call, testContextGetter));
            il.Append(Instruction.Create(OpCodes.Callvirt, getTestResultsDirectory));
            il.Append(Instruction.Create(OpCodes.Call, writeFile));
            il.Append(Instruction.Create(OpCodes.Pop));
            //il.Append(Instruction.Create(OpCodes.Stloc, filePath));

            //il.Append(Instruction.Create(OpCodes.Ldarg_0));
            //il.Append(Instruction.Create(OpCodes.Call, testContextGetter));
            //il.Append(Instruction.Create(OpCodes.Ldloc, filePath));
            
            //MethodReference addResultFile = GetAddResultFileMethodReference(method.Module);
            //il.Append(Instruction.Create(OpCodes.Callvirt, addResultFile));
        }

        private MethodReference GetTestResultsDirectoryGetter(ModuleDefinition usedModule)
        {
            if (mTestResultsDirectoryGetter == null)
            {
                TypeDefinition textContextType = mTestContextType.Resolve();
                mTestResultsDirectoryGetter = usedModule.Import(textContextType.Properties.First<PropertyDefinition>(_ => _.Name == "TestResultsDirectory").GetMethod);
            }
            
            return mTestResultsDirectoryGetter;
        }
        
        private MethodReference GetAddResultFileMethodReference(ModuleDefinition usedModule)
        {
            if (mAddResultFileMethodReference == null)
            {
                TypeDefinition testContextType = mTestContextType.Resolve();
                mAddResultFileMethodReference = usedModule.Import(testContextType.Methods.First<MethodDefinition>(_ => _.Name == "AddResultFile"));
            }
            
            return mAddResultFileMethodReference;
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
        
        private bool LoadUnitTestingAssembly(ModuleDefinition module)
        {
            AssemblyNameReference assemblyNameReference = GetTestingAssemblyNameReference(module);
            
            if (assemblyNameReference == null)
                return false;
            
            DefaultAssemblyResolver assemblyResolver = new DefaultAssemblyResolver();
            
            mUnitTestingAssembly = assemblyResolver.Resolve(assemblyNameReference);

            mTestContextType = module.Import(mUnitTestingAssembly.MainModule.GetTypes().First<TypeDefinition>(_ => _.FullName == "Microsoft.VisualStudio.TestTools.UnitTesting.TestContext"));
            
            return true;
        }
        
        private class TestContextWeaver
        {
            private string mName;
            private TypeDefinition mTargetType;
            private FieldDefinition mBackingField;
            private TypeReference mTestContextType;
            
            public TestContextWeaver(TypeDefinition targetType, string name, TypeReference testContextType)
            {
                mName = name;   
                mTargetType = targetType; 
                mTestContextType = testContextType;
            }
            
            public PropertyDefinition AddTestContextProperty()
            {
                PropertyDefinition testContextProperty = new PropertyDefinition(mName, PropertyAttributes.None, mTestContextType);
                AddBackingField();
                testContextProperty.GetMethod = AddGetMethod();
                testContextProperty.SetMethod = AddSetMethod();
                mTargetType.Properties.Add(testContextProperty);
                return testContextProperty;
            }

            private FieldDefinition AddBackingField()
            {
                FieldDefinition field = new FieldDefinition("m_" + mName + "<>" + DateTime.Now.Ticks, Mono.Cecil.FieldAttributes.Private, mTestContextType);
                mTargetType.Fields.Add(field);
                mBackingField = field;
                return field;
            }

            private MethodDefinition AddGetMethod()
            {
                MethodDefinition get = new MethodDefinition("get_" + mName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, mTestContextType);
                ILProcessor il = get.Body.GetILProcessor();
                il.Append(Instruction.Create(OpCodes.Ldarg_0));
                il.Append(Instruction.Create(OpCodes.Ldfld, mBackingField));
                il.Append(Instruction.Create(OpCodes.Ret));
                get.SemanticsAttributes = MethodSemanticsAttributes.Getter;
                mTargetType.Methods.Add(get);
                return get;
            }

            private MethodDefinition AddSetMethod()
            {
                MethodDefinition set = new MethodDefinition("set_" + mName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, mTargetType.Module.Import(typeof(void)));
                ILProcessor il = set.Body.GetILProcessor();
                il.Append(Instruction.Create(OpCodes.Ldarg_0));
                il.Append(Instruction.Create(OpCodes.Ldarg_1));
                il.Append(Instruction.Create(OpCodes.Stfld, mBackingField));
                il.Append(Instruction.Create(OpCodes.Ret));
                set.Parameters.Add(new ParameterDefinition("value", ParameterAttributes.None, mTestContextType));
                set.SemanticsAttributes = MethodSemanticsAttributes.Setter;
                mTargetType.Methods.Add(set);
                return set;
            }
        }
    }
}

