using System;
using System.Collections.Generic;
using System.Linq;

namespace AllureCSharpCommons.Utils
{
    public class MimeTypes
    {
        private static readonly Dictionary<string, string> _mimeTypes = new Dictionary<string, string>
        {
            {"txt", "text/plain"},
            {"xml", "application/xml"},
            {"html", "text/html"},
            {"png", "image/png"},
            {"jpg", "image/jpeg"},
            {"json", "application/json"},
        };

        public static string ToMime(string extension)
        {
            if (_mimeTypes.ContainsKey(extension))
            {
                return _mimeTypes[extension];
            }
            throw new ArgumentException("extension");
        }

        public static string ToExtension(string mime)
        {
            var extension = _mimeTypes.First(x => x.Value == mime).Key;
            if (extension != null)
            {
                return extension;
            }
            throw new ArgumentException("mime");
        }
    }
}