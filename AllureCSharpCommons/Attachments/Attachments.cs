using System;
using log4net;
using System.IO;
using AllureCSharpCommons.Utils;

namespace AllureCSharpCommons
{
    public static class Attachments
    {
        private static ILog logger = LogManager.GetLogger(typeof(Attachments));
        
        public static event EventHandler<AttachmentAddedEventArgs> Added;

        public static bool OnAdded(string mimeType, string title, object attachment, object context)
        {
            EventHandler<AttachmentAddedEventArgs> added = Added;
            
            logger.Debug("Inside Add method...");
            
            if (added != null)
            {
                added(null, new AttachmentAddedEventArgs(mimeType, title, attachment, context));
                return true;
            }
            
            logger.Debug("The Added event was empty.");
            return false;
        }
        
        // we are not using overloads so when we read the generated IL
        // the used method will be clear

        public static string WriteText(string text, string mimeType)
        {
            string path = GetFileName() + MimeTypes.ToExtension(mimeType);

            File.WriteAllText(path, text);

            return path;
        }


        public static string WriteBinary(byte[] bytes, string mimeType)
        {
            string path = GetFileName() + "." + MimeTypes.ToExtension(mimeType);

            File.WriteAllBytes(path, bytes);

            return path;
        }

        private static string GetFileName()
        {
            // generate a short hash for file names because too long hashs might
            // reach the limit of windows paths.
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}

