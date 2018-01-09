using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Models
{
    public static class Serialization
    {
        /// <summary>
        /// Serializes the object to the binary form.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>
        /// Byte array containing serialized object.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">obj</exception>
        public static byte[] SerializeBinary<T>(T obj) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Deserializes the binary object to the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the expected object.</typeparam>
        /// <param name="objData">The object data as byte array.</param>
        /// <returns>
        /// Deserialized object with type <c>T</c>.
        /// </returns>
        public static T DeserializeBinary<T>(byte[] objData) where T : class
        {
            if (objData == null)
                return default(T);

            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream(objData))
            {
                return formatter.Deserialize(ms) as T;
            }
        }

        /// <summary>
        /// Serializes object to the XML string.
        /// </summary>
        /// <typeparam name="T">Type of the expected object.</typeparam>
        /// <param name="data">The object to serialize.</param>
        /// <returns>
        /// Serialized string.
        /// </returns>
        public static string SerializeXml<T>(T data)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms))
            {
                serializer.Serialize(ms, data);
                ms.Seek(0, SeekOrigin.Begin);
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// Deserializes the XML string to the object with specified type.
        /// </summary>
        /// <typeparam name="T">Type of the expected object.</typeparam>
        /// <param name="data">The XML string.</param>
        /// <returns>Deserialized object.</returns>
        public static T DeserializeXml<T>(string data) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var strReader = new StringReader(data))
            {
                var obj = serializer.Deserialize(strReader) as T;
                strReader.Close();
                return obj;
            }
        }
    }
}