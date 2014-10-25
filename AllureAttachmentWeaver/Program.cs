using System;
using System.IO;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using log4net.Config;
using log4net;
using AllureCSharpCommons;
using Mono.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace AllureAttachmentWeaver
{
    class MainClass
    {
        private static readonly ILog logger = LogManager.GetLogger("AllureAttachmentWeaverLogger");
        
        public static void Main(string[] args)
        {
            BasicConfigurator.Configure();
            
            if (args.Length == 0)
            {
                Console.WriteLine("You must specify the test assembly as an argument.");       
                return;
            }
            
            string fileName = args[0];

            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(fileName);
            
            IEnumerable<MethodDefinition> methodsToAlter = GetMethods(assembly);
            
            if (!methodsToAlter.Any())
            {
                logger.Info("Didn't found any methods to process.");
                return;
            } 
            else if (logger.IsDebugEnabled)
            {
                logger.Debug("Found " + methodsToAlter.Count() + " methods");        
            }

            foreach (MethodDefinition method in methodsToAlter)
            {
                HandleMethod(method);
            }
            
            string newFileName = GetFileNameForGeneratedAssembly(fileName);
            
            logger.Debug("Saving assembly to " + newFileName);
            
            assembly.Write(newFileName);
            
            Console.WriteLine("Assembly saved to: " + newFileName);
        }

        private static void HandleMethod(MethodDefinition method)
        {
            method.Body.SimplifyMacros();
            
            Collection<Instruction> instructions =  method.Body.Instructions;

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

            ILProcessor ilProcessor = method.Body.GetILProcessor();

            // the module already uses the 'AllureAttachmentAttribute' so there is no need to import it into the module
            MethodInfo addAttachmentMethod = typeof(Attachments).GetMethod(
                "Add", 
                BindingFlags.Public | BindingFlags.Static, 
                null, 
                new Type[]{ typeof(object), typeof(object)}, 
                null);

            MethodReference addAttachment = method.Module.Import(addAttachmentMethod);

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
            
            //PrintMessage(ilProcessor, "About to call Add...");
            ilProcessor.Append(Instruction.Create(OpCodes.Call, addAttachment));
            ilProcessor.Append(Instruction.Create(OpCodes.Ret));
            
            method.Body.OptimizeMacros();
        }
        
        private static void PrintMessage(ILProcessor ilProcessor, string message)
        {
            MethodInfo writeLineMethod = typeof(Console).GetMethod("WriteLine", new Type[]{ typeof(string) });
            MethodReference writeLine = ilProcessor.Body.Method.Module.Import(writeLineMethod);
            
            ilProcessor.Append(Instruction.Create(OpCodes.Ldstr, message));
            ilProcessor.Append(Instruction.Create(OpCodes.Call, writeLine));
        }

        private static IEnumerable<MethodDefinition> GetMethods(AssemblyDefinition assembly)
        {
            IList<MethodDefinition> methods = new List<MethodDefinition>();
            
            foreach (ModuleDefinition module in assembly.Modules)
            {
                foreach (TypeDefinition type in module.Types)
                {
                    if (type.FullName == "<Module>")
                        continue;

                    logger.Debug("Found type: " + type.FullName);

                    foreach (MethodDefinition method in type.Methods)
                    {
                        if (!method.HasCustomAttributes)
                        {
                            logger.Debug("Skipping " + method.FullName + " because it doesnt have custom attributes.");
                            continue;
                        }
                                    
                        if (method.ReturnType.FullName == "System.Void")
                        {
                            logger.Debug("Skipping " + method.FullName + " because it doesnt have a return type.");
                            continue;
                        }

                        logger.Debug("Found method '" + method.FullName + "'");
                                    
                        foreach (CustomAttribute methodAttribute in method.CustomAttributes)
                        {
                            logger.Debug("Found attribute '" + methodAttribute.AttributeType.FullName + "'");
                            
                            if (methodAttribute.AttributeType.FullName == typeof(AllureAttachmentAttribute).FullName)
                            {
                                methods.Add(method);
                            }
                        }
                    }
                }
            }
            
            return methods;
        }
        
        private static string GetFileNameForGeneratedAssembly(string filePath)
        {
            // NUnit had trouble locating files with two dots in the name so a dash is used instead.
            string newFileName = Path.GetFileNameWithoutExtension(filePath) + ".generated" + Path.GetExtension(filePath);
            return Path.Combine(Path.GetDirectoryName(filePath), newFileName);
        }
    }
}
