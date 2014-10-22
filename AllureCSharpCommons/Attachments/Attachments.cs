using System;
using log4net;

namespace AllureCSharpCommons
{
    public static class Attachments
    {
        private static ILog logger = LogManager.GetLogger(typeof(Attachments));
        
        public static event EventHandler<AttachmentAddedEventArgs> Added;

        public static void Add(object attachment, object context)
        {
            EventHandler<AttachmentAddedEventArgs> added = Added;
            
            logger.Debug("Inside Add method...");
            
            if (added != null)
            {
                added(null, new AttachmentAddedEventArgs(attachment, context));
            }
            else
            {
                logger.Debug("The Added event was empty.");
            }
        }
    }
}

