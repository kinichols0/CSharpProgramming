using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;
using CSharpProgramming.Common.Models;
using System.Collections.ObjectModel;
using System.CodeDom;

namespace CSharpProgramming.Common.Implementations
{
    public class DataContractSurrogate : IDataContractSurrogate
    {
        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the data contract type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Type GetDataContractType(Type type)
        {
            if (typeof(ArtistInfo).IsAssignableFrom(type))
                return typeof(ArtistInfoSurrogated);
            return type;
        }

        /// <summary>
        /// Mapp the serialized object to the intended deserialized object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public object GetDeserializedObject(object obj, Type targetType)
        {
            if(obj is ArtistInfoSurrogated artistInfoSurr)
            {
                return new ArtistInfo()
                {
                    Id = artistInfoSurr.Id,
                    Alias = artistInfoSurr.Alias,
                    HomeCountry = artistInfoSurr.HomeCountry,
                    HomeState = artistInfoSurr.HomeState,
                    HomeTown = artistInfoSurr.HomeTown
                };
            }
            return obj;
        }

        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Map the object to serialize to a surrogated type
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if(obj is ArtistInfo artistInfo)
            {
                ArtistInfoSurrogated artistInfoSurrogated = new ArtistInfoSurrogated()
                {
                    Id = artistInfo.Id,
                    Alias = artistInfo.Alias,
                    HomeCountry = artistInfo.HomeCountry,
                    HomeState = artistInfo.HomeState,
                    HomeTown = artistInfo.HomeTown
                };
                return artistInfo;
            }
            return obj;
        }

        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
    }
}
