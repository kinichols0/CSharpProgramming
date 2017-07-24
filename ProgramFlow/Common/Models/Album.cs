using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace CSharpProgramming.Common.Models
{
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
        public Track[] Tracks { get; set; }

        [DataMember]
        public ArtistInfo ArtistInformation { get; set; }

        /// <summary>
        /// Returns an XElement representation of the Album
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            XElement element = new XElement("Album");
            element.Add(new XElement("Title", Title));
            element.Add(new XElement("Artist", Artist));
            element.Add(new XElement("Genre", Genre));

            if (Tracks != null && Tracks.Length > 0)
            {
                element.Add(new XElement("Tracks", Tracks.Select(t => new XElement("Track",
                new XElement("Title", t.Title),
                new XElement("TrackNumber", t.TrackNumber))).ToList()));
            }
            else
                element.Add("Tracks");

            return element;
        }

        /// <summary>
        /// Override the ToString() and return a JSON representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // open a MemoryStream object
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // data contract serializer
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Album));

                // serialize the object to json and write to the memory stream
                serializer.WriteObject(memoryStream, this);

                // open a StreamReader to read the string from the MemoryStream
                using (StreamReader streamReader = new StreamReader(memoryStream))
                {
                    // set the position back to the beginning of the MemoryStream and read out and return the entire string
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}