using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
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
