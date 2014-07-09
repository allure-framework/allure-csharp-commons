using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using AllureCSharpCommons.AllureModel;
using AllureCSharpCommons.Exceptions;
using log4net;

namespace AllureCSharpCommons.Utils
{
    public static class AllureResultsUtils
    {
        private static readonly Object AttachmentsLock = new Object();
        private static readonly ILog Log = LogManager.GetLogger(typeof (Allure));

        private static XmlSerializer _serializer;

        private static XmlSerializer Serializer
        {
            get
            {
                if ((_serializer == null))
                {
                    _serializer = new XmlSerializer(typeof (testsuiteresult));
                }
                return _serializer;
            }
        }

        internal static long TimeStamp
        {
            get { return (long) (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds; }
        }

        internal static string TestSuitePath
        {
            get
            {
                if (!Directory.Exists(AllureConfig.ResultsPath))
                {
                    throw new AllureException("Results path directory doesn't exist." +
                                              "Please create results path directory.");
                }
                return AllureConfig.ResultsPath + GenerateUid() + "-testsuite.xml";
            }
        }

        internal static string Serialize(this testsuiteresult testsuiteresult)
        {
            StreamReader streamReader = null;
            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream();
                Serializer.Serialize(memoryStream, testsuiteresult);
                memoryStream.Seek(0, SeekOrigin.Begin);
                streamReader = new StreamReader(memoryStream);
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

        internal static void SaveToFile(this testsuiteresult testsuiteresult, string fileName)
        {
            StreamWriter streamWriter = null;
            try
            {
                var xmlString = testsuiteresult.Serialize();
                var xmlFile = new FileInfo(fileName);
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

        public static byte[] TakeScreenShot()
        {
            var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            return (byte[]) new ImageConverter().ConvertTo(bitmap, typeof (byte[]));
        }


        internal static string GenerateUid()
        {
            return Guid.NewGuid().ToString();
        }

        internal static string GenerateSha256(byte[] data)
        {
            var crypt = new SHA256Managed();
            var hash = String.Empty;
            var crypto = crypt.ComputeHash(data);
            return crypto.Aggregate(hash, (current, bit) => current + bit.ToString("x2"));
        }

        /// <summary>
        ///     Writes attachments presented by byte array.
        /// </summary>
        /// <param name="attachment">Byte array presenting attachment</param>
        /// <param name="title">Title for internal use</param>
        /// <param name="type">Has to be valid MIME type of attachment</param>
        /// <see cref="MimeTypes" />
        /// <returns></returns>
        internal static attachment WriteAttachmentSafely(byte[] attachment, string title, string type)
        {
            try
            {
                return WriteAttachment(attachment, title, type);
            }
            catch (Exception ex)
            {
                return WriteAttachmentWithErrorMessage(ex, title);
            }
        }

        internal static attachment WriteAttachmentWithErrorMessage(Exception exception, string title)
        {
            var message = exception.Message;
            try
            {
                return WriteAttachment(Encoding.UTF8.GetBytes(message), title, "text/plain");
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Can't write attachment {0}, exception: {1}", title, ex);
            }
            return new attachment();
        }

        internal static attachment WriteAttachment(byte[] attachment, string title, string type)
        {
            var relativePath = GenerateSha256(attachment) + "-attachment." + MimeTypes.ToExtension(type);
            var path = AllureConfig.ResultsPath + Path.DirectorySeparatorChar + relativePath;
            if (!File.Exists(path))
            {
                if (!type.Contains("image"))
                {
                    using (var writer = File.AppendText(path))
                    {
                        lock (AttachmentsLock)
                        {
                            writer.Write(Encoding.UTF8.GetString(attachment));
                        }
                    }
                }
                else
                {
                    using (var ms = new MemoryStream(attachment))
                    {
                        var image = Image.FromStream(ms);
                        lock (AttachmentsLock)
                        {
                            image.Save(path);
                        }
                    }
                }
            }
            return new attachment
            {
                title = title,
                source = relativePath,
                type = type,
                size = attachment.Length
            };
        }
    }
}