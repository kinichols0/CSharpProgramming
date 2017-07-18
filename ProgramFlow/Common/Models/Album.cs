using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        public Song[] Tracks { get; set; }

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
