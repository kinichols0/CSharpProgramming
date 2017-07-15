using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CSharpProgramming.DataAccessFileIO
{
    public class Serialization
    {
        public static void SerializeObjectToXmlFileDemo()
        {
            Console.WriteLine("Serialize an Object to Xml File Demo.");

            // initialize the serializable object
            SerializableEmail email = new SerializableEmail()
            {
                ToEmails = new string[] { "test@test.com", "test2@test.com", "test3@test.com" },
                FromEmail = "sender@test.com",
                Subject = "Serializable Objects to XML",
                Body = "Objects can be serialized to xml and written to a file."
            };

            // initialize the serializer
            XmlSerializer serializer = new XmlSerializer(typeof(SerializableEmail));

            // define the file path
            string filePath = "../../OutputFiles/SerializedObjectFiles/SerializableObjectFile.xml";

            // open a file stream and write the serialized object to it
            using (FileStream stream = File.Create(filePath))
            {
                serializer.Serialize(stream, email);
            }
        }

        public static void DeserializeObjectFromXmlFileDemo()
        {
            Console.WriteLine("Deserialize an Object from an Xml file Demo");

            // initialize the serializer
            XmlSerializer serializer = new XmlSerializer(typeof(SerializableEmail));

            // define the file path
            string filePath = "../../OutputFiles/SerializedObjectFiles/SerializableObjectFile.xml";

            // open a stream to read the file
            using(FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                SerializableEmail obj = (SerializableEmail)serializer.Deserialize(stream);
                Console.WriteLine("From Email: {0}\nSubject: {1}\nBody: {2}\nTo Email(s): {3}", obj.FromEmail, obj.Subject, obj.Body, string.Join(",", obj.ToEmails));
            }
        }

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
    }

    [Serializable]
    public class SerializableEmail
    {
        public string[] ToEmails { get; set; }

        public string FromEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }

    [Serializable]
    public class Album
    {
        public string Title { get; set; }

        public string Artist { get; set; }

        public string Genre { get; set; }

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

    [Serializable]
    public class Song
    {
        public string Title { get; set; }

        public int TrackNumber { get; set; }
    }
}
