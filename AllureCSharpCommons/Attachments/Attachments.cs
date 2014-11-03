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

        public static string WriteText(string text)
        {
            string path = GetFileName() + ".txt";

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
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}

