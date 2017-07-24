using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CSharpProgramming.Common.Models
{
    [DataContract(Name = "ArtistInfo")]
    public class ArtistInfoSurrogated
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Alias { get; set; }

        [DataMember]
        public string HomeTown { get; set; }

        [DataMember]
        public string HomeState { get; set; }

        [DataMember]
        public string HomeCountry { get; set; }
    }
}
