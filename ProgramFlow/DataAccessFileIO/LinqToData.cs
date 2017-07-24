using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

using CSharpProgramming.Common.Models;

namespace CSharpProgramming.DataAccessFileIO
{
    public static class LinqToData
    {
        /// <summary>
        /// Linq to Xml to change element values, remove elements
        /// </summary>
        public static void LinqToXmlQueryDemo()
        {
            // initialize the Album object
            var album = new Album()
            {
                Artist = "Jayde Peters",
                Genre = "Hip-Hop/Rap",
                Title = "Growth",
                Tracks = new Track[]
                {
                    new Track{ Title = "Track 1", TrackNumber = 1 },
                    new Track{ Title = "Track 2", TrackNumber = 2 },
                    new Track{ Title = "Track 3", TrackNumber = 3 },
                    new Track{ Title = "Track 4", TrackNumber = 4 },
                    new Track{ Title = "Track 5", TrackNumber = 5 },
                    new Track{ Title = "Track 6", TrackNumber = 6 }
                }
            };

            // serialize the Album to xml and load into an XElement object
            XElement xElement;
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Album));
                serializer.Serialize(stream, album);
                xElement = XElement.Parse(Encoding.UTF8.GetString(stream.ToArray()));
            }

            // print the xml string
            Console.WriteLine("\n" + xElement.ToString() + "\n");

            // get and remove tracks
            var tracks = from track in xElement.Descendants("Tracks").Descendants("Track")
                         select track;
            tracks.ToList().ForEach(t => t.Remove());

            // Change the title name
            var title = (from record in xElement.Descendants("Title")
                        select record).FirstOrDefault();           
            if(title != null)
                title.Value = "New Title";

            // write the resulting xelement
            Console.WriteLine("\n" + xElement.ToString() + "\n");
        }

        public static void BuildXmlWithAttributesDemo()
        {
            XElement element = new XElement("Reservation", 
                new XElement("Passenger", new XAttribute("Name", "John Doe"), new XAttribute("FrequentFlyerNumber", 12345)));
            Console.WriteLine(element.ToString());
        }
    }
}
