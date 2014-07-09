using System;
using System.Collections.Generic;
using System.Linq;

namespace AllureCSharpCommons.Utils
{
    internal class MimeTypes
    {
        private static readonly Dictionary<string, string> MimeTypesDictionary = new Dictionary<string, string>
        {
            {"txt", "text/plain"},
            {"xml", "application/xml"},
            {"html", "text/html"},
            {"png", "image/png"},
            {"jpg", "image/jpeg"},
            {"json", "application/json"},
        };

        internal static string ToMime(string extension)
        {
            if (MimeTypesDictionary.ContainsKey(extension))
            {
                return MimeTypesDictionary[extension];
            }
            throw new ArgumentException("extension");
        }

        internal static string ToExtension(string mime)
        {
            var extension = MimeTypesDictionary.First(x => x.Value == mime).Key;
            if (extension != null)
            {
                return extension;
            }
            throw new ArgumentException("mime");
        }
    }
}