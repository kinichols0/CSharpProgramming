﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.IO;
using CSharpProgramming.Common.Models;
using CSharpProgramming.Common.Implementations;

namespace CSharpProgramming.DataAccessFileIO
{
    public class Serialization
    {
        /// <summary>
        /// Serialize an object to xml and write to a file
        /// </summary>
        public static void SerializeObjectToXmlFileDemo()
        {

            Console.WriteLine("Serialize an Object to Xml File Demo.");

            // initialize the object
            Album album = new Album()
            {
                Artist = "Rapper Sid",
                Title = "Sidtastic",
                Genre = "Rap/Hip-Hop",
                Tracks = new Track[]
                {
                    new Track{ Title = "Title 1", TrackNumber = 1 },
                    new Track{ Title = "Title 2", TrackNumber = 2 },
                    new Track{ Title = "Title 3", TrackNumber = 3 },
                    new Track{ Title = "Title 4", TrackNumber = 4 },
                    new Track{ Title = "Title 5", TrackNumber = 5 }
                }
            };

            // initialize the serializer
            XmlSerializer serializer = new XmlSerializer(typeof(Album));

            // define the file path
            string filePath = "../../OutputFiles/SerializedObjectFiles/SerializableObjectFile.xml";

            // open a file stream and write the serialized object to it
            using (FileStream stream = File.Create(filePath))
            {
                serializer.Serialize(stream, album);
            }
        }

        /// <summary>
        /// Read xml from a file and deserialize to an object
        /// </summary>
        public static void DeserializeObjectFromXmlFileDemo()
        {
            Console.WriteLine("Deserialize an Object from an Xml file Demo");

            // initialize the serializer
            XmlSerializer serializer = new XmlSerializer(typeof(Album));

            // define the file path
            string filePath = "../../OutputFiles/SerializedObjectFiles/SerializableObjectFile.xml";

            // open a stream to read the file
            using(FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                Album obj = (Album)serializer.Deserialize(stream);
                Console.WriteLine(obj);
            }
        }

        /// <summary>
        /// Serialize an object to binary and write it to a file
        /// </summary>
        public static void SerializeObjectToBinaryFileDemo()
        {
            Console.WriteLine("Serialize an object to binary format and write it to a text file demo");

            string filePath = "../../OutputFiles/SerializedObjectFiles/SerializableObjectBinaryFile.txt";

            Album album = new Album()
            {
                Artist = "Rapper Sid",
                Title = "Sidtastic",
                Genre = "Rap/Hip-Hop",
                Tracks = new Track[]
                {
                    new Track{ Title = "Title 1", TrackNumber = 1 },
                    new Track{ Title = "Title 2", TrackNumber = 2 },
                    new Track{ Title = "Title 3", TrackNumber = 3 },
                    new Track{ Title = "Title 4", TrackNumber = 4 },
                    new Track{ Title = "Title 5", TrackNumber = 5 }
                }
            };

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using(FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                binaryFormatter.Serialize(stream, album);
            }
        }

        /// <summary>
        /// Read the binary data from a file and deserialize it to an object
        /// </summary>
        public static void DeserializeObjectFromBinaryFileDemo()
        {
            Console.WriteLine("Deserialize an object in binary format from a text file demo");

            string filePath = "../../OutputFiles/SerializedObjectFiles/SerializableObjectBinaryFile.txt";

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var album = (Album)formatter.Deserialize(stream);
                Console.WriteLine(album);
            }
        }

        /// <summary>
        /// Serializes an object to json and writes the json to a file
        /// </summary>
        public static void SerializeObjectToJsonDemo()
        {
            Console.WriteLine("Serialize an object to json and write it to a text file demo");

            // file path
            string filePath = "../../OutputFiles/SerializedObjectFiles/SerializableObjectJson.txt";

            // initialize the object
            Album album = new Album()
            {
                Artist = "Rapper Sid",
                Title = "Sidtastic",
                Genre = "Rap/Hip-Hop",
                Tracks = new Track[]
                {
                    new Track{ Title = "Title 1", TrackNumber = 1 },
                    new Track{ Title = "Title 2", TrackNumber = 2 },
                    new Track{ Title = "Title 3", TrackNumber = 3 },
                    new Track{ Title = "Title 4", TrackNumber = 4 },
                    new Track{ Title = "Title 5", TrackNumber = 5 }
                }
            };

            // initialize the json serializer
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Album));

            // open a file stream for the output file
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                // serialize the object and write the json to the file stream
                jsonSerializer.WriteObject(stream, album);
            }
        }

        /// <summary>
        /// Deserialize a json string in text file to an object
        /// </summary>
        public static void DeserializeObjectFromJsonDemo()
        {
            Console.WriteLine("Deserialize an object in json format from a text file demo");

            // file path
            string filePath = "../../OutputFiles/SerializedObjectFiles/SerializableObjectJson.txt";

            // read and display the json string from the file
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // use stream reader to read the contents of the file
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    // write the contents of the file to the console
                    Console.WriteLine(streamReader.ReadToEnd() + "\n");

                    // restart the start position of the string to the beginning
                    stream.Seek(0, SeekOrigin.Begin);

                    // initialize the json serializer we'll use the deserialize the json string
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Album));

                    // deserialize the json string and cast to the object
                    var album = (Album)jsonSerializer.ReadObject(stream);

                    // write the object to the file
                    Console.WriteLine(album);
                }
            }
        }

        public static void MemoryStreamJsonSerializationDemo()
        {
            Console.WriteLine("Serialization to MemoryStream Demo.");

            // initialize the json serializer
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Album));

            // Initialize Album object
            Album album = new Album()
            {
                Title = "90s R&B Classics",
                Artist = "Various Artists",
                Genre = "R&B",
                Tracks = new Track[]
                {
                    new Track(){ Title = "Title 1", TrackNumber = 1 },
                    new Track(){ Title = "Title 2", TrackNumber = 2 },
                    new Track(){ Title = "Title 3", TrackNumber = 3 },
                    new Track(){ Title = "Title 4", TrackNumber = 4 },
                    new Track(){ Title = "Title 5", TrackNumber = 5 }
                }
            };
            Console.WriteLine("Album object initialized:\n{0}", album);

            // Serialize and write to MemoryStream
            byte[] memoryStreamBytes;
            using(MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, album);
                memoryStreamBytes = stream.ToArray();
                Console.WriteLine("Bytes of Album object serialized to JSON and written to a MemoryStream object:\n{0}\n",
                    string.Join(" ", stream.ToArray().Select(t => t.ToString())));
            }

            // open another MemoryStream to read the bytes and deserialize the JSON String
            using (MemoryStream stream = new MemoryStream(memoryStreamBytes))
            {
                var deserializedAlbum = (Album)serializer.ReadObject(stream);
                Console.WriteLine("Album deserialized:\n{0}\n", album);
            }
        }

        public static void ObjectToXmlStringSerializationDemo()
        {
            Console.WriteLine("Object Serialization to Xml String Demo.");

            // initialize the json serializer
            XmlSerializer serializer = new XmlSerializer(typeof(Album));

            // Initialize Album object
            Album album = new Album()
            {
                Title = "90s R&B Classics",
                Artist = "Various Artists",
                Genre = "R&B",
                Tracks = new Track[]
                {
                    new Track(){ Title = "Title 1", TrackNumber = 1 },
                    new Track(){ Title = "Title 2", TrackNumber = 2 },
                    new Track(){ Title = "Title 3", TrackNumber = 3 },
                    new Track(){ Title = "Title 4", TrackNumber = 4 },
                    new Track(){ Title = "Title 5", TrackNumber = 5 }
                }
            };
            Console.WriteLine("Album object initialized:\n{0}\n", album);

            // Serialize to xml string
            string xmlString = null;
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, album);
                xmlString = writer.ToString();
                Console.WriteLine("Xml string of Album object:\n{0}\n", xmlString);
            }

            // Deserialize xml string to object
            using (StringReader reader = new StringReader(xmlString))
            {
                var deserializedAlbum = (Album)serializer.Deserialize(reader);
                Console.WriteLine("Derialized Album Object:\n{0}\n", deserializedAlbum);
            }
        }

        /// <summary>
        /// Use data contract surrogate to serialize members with
        /// members that do not have [DataContract] attributes
        /// </summary>
        public static void DataContractSurrogateDemo()
        {
            // initialize the object
            Album album = new Album()
            {
                Artist = "Rapper Sid",
                Title = "Sidtastic",
                Genre = "Rap/Hip-Hop",
                Tracks = new Track[]
                {
                    new Track{ Title = "Title 1", TrackNumber = 1 },
                    new Track{ Title = "Title 2", TrackNumber = 2 },
                    new Track{ Title = "Title 3", TrackNumber = 3 },
                    new Track{ Title = "Title 4", TrackNumber = 4 },
                    new Track{ Title = "Title 5", TrackNumber = 5 }
                },
                ArtistInformation = new ArtistInfo
                {
                    Alias = "Rapper Sid",
                    Id = 1001,
                    HomeCountry = "United States",
                    HomeState = "MD",
                    HomeTown = "Baltimore"
                }
            };

            string json = null;
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Album), 
                    new List<Type>() { typeof(ArtistInfo), typeof(Track) },
                    int.MaxValue, 
                    false, 
                    new DataContractSurrogate(),
                    false);
                serializer.WriteObject(stream, album);
                using (StreamReader reader = new StreamReader(stream))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    json = reader.ReadToEnd();
                }
            }

            Console.WriteLine("Album Object:\n\n{0}", json);
        }
    }
}
