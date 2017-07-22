using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using CSharpProgramming.Common;
using CSharpProgramming.Common.Utilities;

namespace CSharpProgramming.TypesClasses.Implementations
{
    /// <summary>
    /// IComparable implementation
    /// </summary>
    [DataContract]
    public class ComparableEntity : IComparable
    {
        [DataMember]
        public string CompareName { get; set; }

        [DataMember]
        public int CompareId { get; set; }

        public ComparableEntity() : this(null, 0) { }

        public ComparableEntity(string name, int id)
        {
            CompareName = name;
            CompareId = id;
        }

        /// <summary>
        /// "obj" is the ComparableEntity being compared to this ComparableEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 0;

            if (obj is ComparableEntity incomingComparable)
                return this.CompareId.CompareTo(incomingComparable.CompareId);

            throw new ArgumentException("Object is not a ComparableEntity.");
        }

        /// <summary>
        /// Overridden equals message
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {   
            if(obj != null && obj is ComparableEntity inObj)
                return inObj.CompareId == CompareId 
                    && inObj.CompareName == CompareName;
            return false;
        }

        /// <summary>
        /// Overridden GetHashCode method. Since Equals method was overriden
        /// then GetHashCode should be overridden to reflect the Equals method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (!string.IsNullOrEmpty(CompareName))
                return CompareId + CompareName.GetHashCode();
            return CompareId;
        }

        public override string ToString()
        {
            // return json string representation
            return SerializationUtility.SerializeToJsonString(this);
        }
    }
}
