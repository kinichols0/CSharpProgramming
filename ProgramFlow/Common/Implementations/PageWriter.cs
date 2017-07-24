using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace CSharpProgramming.Common.Implementations
{
    /// <summary>
    /// Demo implementation of the Basic Disposable Pattern
    /// </summary>
    public class PageWriter : IDisposable
    {
        /* Use SafeHandle when available */
        private StringWriter stringWriter;

        public PageWriter()
        {
            stringWriter = new StringWriter();
        }

        public void WriteLine(string text)
        {
            stringWriter.WriteLine(text);
        }

        #region Basic Dispose Pattern

        public void Dispose()
        {
            Dispose(true);

            // Tell the garbage collector its now safe to destroy
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalize method override. Tells the garbage collector what
        /// dispose code to run before it prepares to dispose of this
        /// object.
        /// </summary>
        ~PageWriter()
        {
            Dispose(false);// pass false. Some objects may already be finalized.
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (stringWriter != null)
                {
                    stringWriter.Flush();
                    stringWriter.Close();
                    stringWriter.Dispose();
                }
            }
        }

        #endregion
    }
}
