using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Implementations
{
    /// <summary>
    /// Formats phone numbers. Implements the IFormatProvider and
    /// ICustomeFormatter interfaces.
    /// </summary>
    public class PhoneNumberFormatter : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// Return the formatter when requested
        /// </summary>
        /// <param name="formatType"></param>
        /// <returns></returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        /// <summary>
        /// Format the "arg" to a phone number representation
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string fmt = !string.IsNullOrEmpty(format) ? format.ToUpper() : string.Empty;
            string result = string.Empty;

            if (arg is string phoneNumber && phoneNumber.Length >= 9)
            {
                switch (fmt)
                {
                    case "P":
                        result = string.Format("({0})-{1}-{2}", phoneNumber.Substring(0, 3), phoneNumber.Substring(3, 3), phoneNumber.Substring(6, 3));
                        break;
                    default:
                        result = phoneNumber;
                        break;
                }   
            }
            return result;
        }
    }
}
