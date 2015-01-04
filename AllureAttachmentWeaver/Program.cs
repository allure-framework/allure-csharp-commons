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
using log4net.Core;
using log4net.Appender;
using log4net.Layout;

namespace AllureAttachmentWeaver
{
    class MainClass
    {
        private static readonly ILog logger = LogManager.GetLogger("AllureAttachmentWeaverLogger");
        
        public static void Main(string[] args)
        {
            Level loggingLevel = Level.Info;
            
            if (args.Length == 0)
            {
                logger.Fatal("You must specify the test assembly as an argument.");       
                return;
            }
            
            if (args.Length > 1 && args.Contains("-v"))
            {
                loggingLevel = Level.Debug;
            }
            
            // vNext should use XmlConfigurator with a logger.config file or app.config.
            SetupLogging(loggingLevel);
            
            string fileName = args[0];

            AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(fileName);

            IMethodWeaver weaver = new AttachmentWeaver();

            try
            {
                Weave(assembly, weaver);

                Save(assembly);
            }
            catch(Exception e)
            {
                logger.Error("Error writing assembly.", e);
            }
        }

        private static void Weave(AssemblyDefinition assembly, IMethodWeaver weaver)
        {
            // because we might be adding types/methods the enumerations must be eager.
            foreach (ModuleDefinition module in assembly.Modules.ToList())
            {
                foreach (TypeDefinition type in module.Types.ToList())
                {
                    if (type.FullName == "<Module>")
                        continue;

                    logger.Debug("Found type: " + type.FullName);

                            
                    foreach (MethodDefinition method in type.Methods.ToList())
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

            string backup = GetFileNameWithKeyword(fileName, "original");
            string generated = GetFileNameWithKeyword(fileName, "generated");
            
            logger.Debug("Saving assembly to: " + fileName);

            if (File.Exists(generated))
                File.Delete(generated);

            if (File.Exists(backup))
                File.Delete(backup);

            assembly.Write(generated);

            File.Move(fileName, backup);
            File.Move(generated, fileName);

            logger.Info("Assembly saved to: " + fileName);
        }

        private static string GetFileNameWithKeyword(string filePath, string keyword)
        {
            string backup = Path.GetFileNameWithoutExtension(filePath) + "." + keyword + Path.GetExtension(filePath);
            return Path.Combine(Path.GetDirectoryName(filePath), backup);
        }
        
        private static void SetupLogging(Level level)
        {
            ConsoleAppender appender = new ConsoleAppender();
            appender.Layout = new SimpleLayout();
            appender.Threshold = level;
            BasicConfigurator.Configure(appender);
        }
    }
}
