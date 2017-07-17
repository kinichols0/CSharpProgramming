using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.IO;

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
                Tracks = new Song[]
                {
                    new Song{ Title = "Title 1", TrackNumber = 1 },
                    new Song{ Title = "Title 2", TrackNumber = 2 },
                    new Song{ Title = "Title 3", TrackNumber = 3 },
                    new Song{ Title = "Title 4", TrackNumber = 4 },
                    new Song{ Title = "Title 5", TrackNumber = 5 }
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
                Tracks = new Song[]
                {
                    new Song{ Title = "Title 1", TrackNumber = 1 },
                    new Song{ Title = "Title 2", TrackNumber = 2 },
                    new Song{ Title = "Title 3", TrackNumber = 3 },
                    new Song{ Title = "Title 4", TrackNumber = 4 },
                    new Song{ Title = "Title 5", TrackNumber = 5 }
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
                Tracks = new Song[]
                {
                    new Song{ Title = "Title 1", TrackNumber = 1 },
                    new Song{ Title = "Title 2", TrackNumber = 2 },
                    new Song{ Title = "Title 3", TrackNumber = 3 },
                    new Song{ Title = "Title 4", TrackNumber = 4 },
                    new Song{ Title = "Title 5", TrackNumber = 5 }
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
    }

    [DataContract]
    [Serializable]
    public class Album
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Artist { get; set; }

        [DataMember]
        public string Genre { get; set; }

        [DataMember]
        public Song[] Tracks { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("{");
            builder.AppendLine("\tTitle: " + Title + ",");
            builder.AppendLine("\tArtist: " + Artist + ",");
            builder.AppendLine("\tGenre: " + Genre + ",");
            builder.AppendLine("\tTracks: [" + string.Join(", ", Tracks.Select(t => "{ Title: " + t.Title + ", TrackNumber: " + t.TrackNumber + " }")) + "]");
            builder.AppendLine("}");
            return builder.ToString();
        }
    }

    [DataContract]
    [Serializable]
    public class Song
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int TrackNumber { get; set; }
    }
}
