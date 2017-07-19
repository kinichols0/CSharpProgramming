using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Interfaces
{
    public interface ISecurityUtility
    {
        byte[] SHA256Hash(string message);
    }
}
