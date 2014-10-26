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
using AllureAttachmentWeaver.Behaviors;

namespace AllureAttachmentWeaver
{
    class MainClass
    {
        private static readonly ILog logger = LogManager.GetLogger("AllureAttachmentWeaverLogger");
        
        public static void Main(string[] args)
        {
            // vNext should use XmlConfigurator with a logger.config file or app.config.
            BasicConfigurator.Configure();
            
            if (args.Length == 0)
            {
                logger.Fatal("You must specify the test assembly as an argument.");       
                return;
            }
            
            string fileName = args[0];

            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(fileName);

            IMethodWeaver behavior = new MultiWeaver(new IMethodWeaver[] { new AttachmentWeaver() });

            Weave(assembly, behavior);

            Save(assembly);
        }

        private static void Weave(AssemblyDefinition assembly, IMethodWeaver weaver)
        {
            foreach (ModuleDefinition module in assembly.Modules)
            {
                foreach (TypeDefinition type in module.Types)
                {
                    if (type.FullName == "<Module>")
                        continue;

                    logger.Debug("Found type: " + type.FullName);

                    foreach (MethodDefinition method in type.Methods)
                    {
                        weaver.Weave(method);
                    }
                }
            }
        }

        private static void Save(AssemblyDefinition assembly)
        {
            string[] args = Environment.GetCommandLineArgs();

            // already tested that atleast one argument is present.
            // user arguments array is 1 based.
            string fileName = args[1];

            string newFileName = GetFileNameForGeneratedAssembly(fileName);

            logger.Debug("Saving assembly to: " + newFileName);

            assembly.Write(newFileName);

            logger.Info("Assembly saved to: " + newFileName);
        }

        private static string GetFileNameForGeneratedAssembly(string filePath)
        {
            string newFileName = Path.GetFileNameWithoutExtension(filePath) + ".generated" + Path.GetExtension(filePath);
            return Path.Combine(Path.GetDirectoryName(filePath), newFileName);
        }
    }
}
