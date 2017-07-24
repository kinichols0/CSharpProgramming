using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpProgramming.Common.Models
{
    internal class Customer
    {
        string account;
        bool verified;

        public Customer() { }

        public Customer(string account, bool verified)
        {
            this.account = account;
            this.verified = verified;
        }

        public string Account
        {
            get
            {
                return account;
            }

            set
            {
                account = value;
            }
        }

        protected bool Verified
        {
            get
            {
                return verified;
            }

            set
            {
                verified = value;
            }
        }
    }
}
