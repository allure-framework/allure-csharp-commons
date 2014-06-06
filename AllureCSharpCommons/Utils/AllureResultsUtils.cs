﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AllureCSharpCommons.AllureModel;
using log4net;

namespace AllureCSharpCommons.Utils
{
    public static class AllureResultsUtils
    {
        private static string _resultsPath;

        public static string ResultsPath
        {
            get
            {
                return _resultsPath ?? (_resultsPath = "");
            }
            set
            {
                _resultsPath = value;
            }
        }

        private static readonly Object AttachmentsLock = new Object();
        private static readonly ILog Log = LogManager.GetLogger(typeof (Allure));

        public static void AddRange<T>(this T[] array, T[] elements)
        {
            
        }

        public static void Add<T>(this T[] array, T element)
        {
            
        }

        private static System.Xml.Serialization.XmlSerializer _serializer;

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((_serializer == null))
                {
                    _serializer = new System.Xml.Serialization.XmlSerializer(typeof(testsuiteresult));
                }
                return _serializer;
            }
        }

        public static string Serialize(this testsuiteresult testsuiteresult)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                Serializer.Serialize(memoryStream, testsuiteresult);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public static void SaveToFile(this testsuiteresult testsuiteresult, string fileName)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = testsuiteresult.Serialize();
                System.IO.FileInfo xmlFile = new System.IO.FileInfo(fileName);
                streamWriter = xmlFile.CreateText();
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        internal static long TimeStamp
        {
            get { return (long) (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds; }
        }

        internal static string GenerateUid()
        {
            return Guid.NewGuid().ToString();
        }

        internal static string GenerateSha256(byte[] data)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(data);
            return crypto.Aggregate(hash, (current, bit) => current + bit.ToString("x2"));
        }

        internal static string TestSuitePath
        {
            get { return ResultsPath + GenerateUid() + "-testsuite.xml"; }
        }

        /// <summary>
        ///     Writes attachments presented by byte array.
        /// </summary>
        /// <param name="attachment">Byte array presenting attachment</param>
        /// <param name="title">Title for internal use</param>
        /// <param name="type">Has to be valid MIME type of attachment</param>
        /// <returns></returns>
        internal static attachment WriteAttachmentSafely(byte[] attachment, string title, string type)
        {
            try
            {
                //TODO:
                //return string.IsNullOrEmpty(type)
                //    ? WriteAttachment(attachment, title)
                //    : WriteAttachment(attachment, title, type);
                return WriteAttachment(attachment, title, type);
            }
            catch (Exception ex)
            {
                return WriteAttachmentWithErrorMessage(ex, title);
            }
        }

        internal static attachment WriteAttachmentWithErrorMessage(Exception exception, string title)
        {
            string message = exception.Message;
            try
            {
                return WriteAttachment(Encoding.UTF8.GetBytes(message), title, "text/plain");
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Can't write attachment {0}", title), ex);
            }
            return new attachment();
        }

        internal static attachment WriteAttachment(byte[] attachment, string title, string type)
        {
            string path = GenerateSha256(attachment) + "-attachment." + MimeTypes.ToExtension(type);
            if (!File.Exists(path))
            {
                if (!type.Contains("image"))
                {
                    using (StreamWriter writer = File.AppendText(path))
                    {
                        lock (AttachmentsLock)
                        {
                            writer.Write(Encoding.UTF8.GetString(attachment));
                        }
                    }
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(attachment))
                    {
                        Image image = Image.FromStream(ms);
                        lock (AttachmentsLock)
                        {
                            image.Save(path);
                        }
                    }
                } 
            }
            return new attachment()
            {
                title = title,
                source = path,
                type = type,
                size = attachment.Length
            };
        }

        internal static attachment WriteAttachment(byte[] attachment, string title)
        {
            //TODO:
            return null;
        }
    }
}