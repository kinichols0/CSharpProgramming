using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
    public static class ThreadOpsService
    {
        private static ThreadLocal<ThreadOps> threadOps;

        /// <summary>
        /// Static constructor to initialize any static data on 
        /// on create that should only be done once.
        /// </summary>
        static ThreadOpsService()
        {
            threadOps = new ThreadLocal<ThreadOps>(() => new ThreadOps());
        }
    }
}
