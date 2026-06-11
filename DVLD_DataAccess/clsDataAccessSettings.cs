using System;
using System.Diagnostics;
using System.Configuration;

namespace DVLD_DataAccess
{
    static class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DVLDConnectionString"].ConnectionString;


        public static void LogException(Exception ex)
        {
            string SourceName = "DVLD System";

            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, "Application");
                }
            }
            catch (Exception ex2)
            {
                Debug.WriteLine($"Logger init failed : {ex2.ToString()}");
            }


            EventLog.WriteEntry(SourceName, ex.ToString(), EventLogEntryType.Error);
        }

    }
}
