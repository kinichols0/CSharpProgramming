using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml.Serialization;
using CSharpProgramming.Common.Interfaces;

namespace CSharpProgramming.Common.Implementations
{
    public class SerializationUtility : ISerializationUtility, IDisposable
    {
        /// <summary>
        /// Get the Json string representation of an object. The class
        /// must be decorated with DataContract and its members must be
        /// decorated with DataMember attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string SerializeToJsonString<T>(T obj ) where T : class
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(memoryStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Get the Xml string representation of an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string SerializeToXmlString<T>(T obj) where T : class
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Deserialize a json string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public T DeserializeJson<T>(string str) where T : class
        {
            byte[] utfEncodedBytes = Encoding.UTF8.GetBytes(str);
            using (MemoryStream stream = new MemoryStream(utfEncodedBytes))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Deserialize an Xml string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public T DeserializeXml<T>(string str) where T : class
        {
            using (StringReader reader = new StringReader(str))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }

        public void Dispose()
        {

        }
    }
}
