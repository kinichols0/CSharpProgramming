using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Interfaces
{
    public interface ISerializationUtility
    {
        string SerializeToJsonString<T>(T obj) where T : class;

        string SerializeToXmlString<T>(T obj) where T : class;

        T DeserializeJson<T>(string json) where T : class;

        T DeserializeXml<T>(string str) where T : class;
    }
}
