using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Serilog;

namespace CSharpProgramming.Common.Utilities
{
    public static class LogTraceTrackUtility
    {
        #region callable code

        /// <summary>
        /// Configure logging, tracing, event logging, etc.
        /// </summary>
        public static void Configure()
        {
            ConfigureSerilog();
            CreateTraceListeners();
        }

        /// <summary>
        /// Writes to an event log. Use the event log to record important software
        /// and hardware events.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="eventStr"></param>
        public static void WriteToEventLog(string source, string eventStr, EventLogEntryType? entryType = null, int? eventId = null)
        {
            try
            {
                // event log file
                string log = ConfigurationManager.AppSettings["EventLogFile"];

                // if the source does not exist then create it
                if (!EventLog.SourceExists(source))
                    EventLog.CreateEventSource(source, log);

                // write to the event log
                if (entryType.HasValue && eventId.HasValue)
                    EventLog.WriteEntry(source, eventStr, entryType.Value, eventId.Value);
                else
                    EventLog.WriteEntry(source, eventStr);
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Error during Event Log writing.");
            }
        }

        /// <summary>
        /// Write a message to Tracing output
        /// </summary>
        /// <param name="traceMsg"></param>
        public static void WriteTraceMsg(string traceMsg)
        {
            try
            {
                Trace.WriteLine(traceMsg);
                Trace.Flush();
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Error during Trace writing.");
            }
        }

        public static void WriteTraceSwitchMsg(string msg)
        {
            try
            {
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Error during Trace Switch writing.");
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// Configure Serilog for this application
        /// </summary>
        private static void ConfigureSerilog()
        {
            // configure the logger
            ILogger logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            // configure the global logger
            Log.Logger = logger;
        }

        /// <summary>
        /// Add trace listeners
        /// </summary>
        private static void CreateTraceListeners()
        {
            // add file trace listener
            string traceFile = ConfigurationManager.AppSettings["TraceFile"];
            TextWriterTraceListener textFileListener = new TextWriterTraceListener(traceFile);
            Trace.Listeners.Add(textFileListener);
        }

        #endregion
    }
}
