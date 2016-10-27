using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CnSharp.VisualStudio.NuPack.Util
{
    public class XmlSerializerHelper
    {
        public static T Copy<T>(T t)
        {
            return LoadObjectFromXmlString<T>(GetXmlStringFromObject(t));
        }

        public static string GetXmlStringFromObject<T>(T obj)
        {
            string str2;
            using (var stream = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
                {
                    writer.Indentation = 3;
                    writer.IndentChar = ' ';
                    writer.Formatting = Formatting.Indented;
                    new XmlSerializer(obj.GetType()).Serialize(writer, obj);
                    var str = Encoding.UTF8.GetString(stream.ToArray());
                    var index = str.IndexOf("?>");
                    if (index > 0)
                    {
                        str = str.Substring(index + 2);
                    }
                    str2 = str.Trim();
                }
            }
            return str2;
        }

        public static T LoadObjectFromXml<T>(string filename)
        {
            using (var reader = new StreamReader(filename, Encoding.UTF8))
            {
                var serializer = new XmlSerializer(typeof (T));
                return (T) serializer.Deserialize(reader);
            }
        }

        public static T LoadObjectFromXmlString<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof (T));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                return (T) serializer.Deserialize(stream);
            }
        }
    }
}