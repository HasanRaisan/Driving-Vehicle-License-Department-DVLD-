using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsDataAccessLayer
{
    internal static class clsGlobalDataAccess
    {

        public static void LogError(Exception ex)
        {
            string sourceName = "DVLD";
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }

            string errorDetails = $"Message: {ex.Message}\nStackTrace: {ex.StackTrace}\nTargetSite: {ex.TargetSite}";
            EventLog.WriteEntry(sourceName, errorDetails, EventLogEntryType.Error);
        }
    }
}
