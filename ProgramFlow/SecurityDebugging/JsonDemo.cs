using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpProgramming.TypesClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSharpProgramming.SecurityDebugging
{
    public class JsonDemo
    {
        public static void JsonSerializationDemo()
        {
            Console.WriteLine("Serialization JSON data demo");
            Console.WriteLine("Initializing StudentProfileData object\n");

            // initialize the object we are going to serialize to json
            StudentProfileData data = new StudentProfileData("John", "Doe", "Computer Science", ClassStanding.Junior)
            {
                DateOfBirth = DateTime.Now
            };
            Console.WriteLine(data + "\n");

            // serialize the object to a json string
            var json = JsonConvert.SerializeObject(data);
            Console.WriteLine("StudentProfileData as serialized json string:\n{0}", json);
            Console.WriteLine("Is valid json: {0}\n", IsValidJson(json));

            // deserialize the json string back to object
            StudentProfileData deserializedData = JsonConvert.DeserializeObject<StudentProfileData>(json);
            Console.WriteLine("Deseserialized the string to StudentProfileData object:\n{0}\n", deserializedData);

            // test an invalid json string
            string jsonStr = json.Remove(0, 1);
            Console.WriteLine("Removed first character:\n{0}", jsonStr);
            Console.WriteLine("Is valid json: {0}\n", IsValidJson(jsonStr));
        }

        private static bool IsValidJson(string json)
        {
            try
            {
                JToken.Parse(json);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
