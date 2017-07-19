using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Interfaces
{
    public interface ICryptographyService
    {
        byte[] EncryptFile(string filePath);

        byte[] DecryptFileToBytes(byte[] encryptedFileBytes);

        string DecryptFileToString(byte[] encryptedFileBytes);

        byte[] EncryptString(string str);

        string DecryptStringToString(byte[] encryptedStringBytes);

        byte[] DecryptStringToBytes(byte[] encryptedStringBytes);

        byte[] ComputeHash(string message);

        byte[] ComputeHash<T>(T obj) where T : class;
    }
}
