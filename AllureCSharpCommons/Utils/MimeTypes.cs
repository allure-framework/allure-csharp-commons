using System;
using System.Collections.Generic;
using System.Linq;

namespace AllureCSharpCommons.Utils
{
    public static class MimeTypes
    {
        private static readonly ISet<string> TextMimeTypes = new HashSet<string>(
            new [] { "text/plain", "application/xml", "text/html", "application/json" },
            StringComparer.OrdinalIgnoreCase
        );
        
        private static readonly Dictionary<string, string> ExtensionToMimeType = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            {"txt", "text/plain"},
            {"xml", "application/xml"},
            {"html", "text/html"},
            {"png", "image/png"},
            {"jpg", "image/jpeg"},
            {"json", "application/json"},
        };
        
        private static readonly Dictionary<string, string> MimeTypeToExtension = 
            ExtensionToMimeType
                .Select(_ => new { Key = _.Value, Value = _.Key})
                .ToDictionary(_ => _.Key, _ => _.Value, StringComparer.OrdinalIgnoreCase);

        public static string ToMime(string extension)
        {
            string mimeType;
            if (ExtensionToMimeType.TryGetValue(extension, out mimeType))
            {
                return mimeType;
            }
            throw new ArgumentException("extension");
        }

        public static string ToExtension(string mime)
        {
            string extension;
            if (MimeTypeToExtension.TryGetValue(mime, out extension))
            {
                return extension;
            }
            throw new ArgumentException("mime");
        }
        
        public static bool IsText(string mimeType)
        {
            return TextMimeTypes.Contains(mimeType);
        }
    }
}