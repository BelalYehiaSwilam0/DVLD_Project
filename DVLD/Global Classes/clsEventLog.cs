using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace DVLD.Global_Classes
{
    public class clsEventLog
    {
        
        public static void EventLogs(string sourceName,string categoryName ,string message, EventLogEntryType eventLogEntryType)
        {
            try
            {
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, categoryName);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to create event source. Please run the application as Administrator.\n\nDetails: " + ex.Message,
                    "Event Log Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            EventLog.WriteEntry(sourceName, message, eventLogEntryType);
        }
    }
}
