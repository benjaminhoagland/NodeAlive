using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public static class Log
    {
        public static EventLog NAEventLog;
        
        static Log()
        {
            NAEventLog = new EventLog(); 
            if (!EventLog.SourceExists("NodeAliveSource"))
            {
                EventLog.CreateEventSource("NodeAliveSource", "NodeAlive");
            }
            NAEventLog.Source = "NodeAliveSource";
            NAEventLog.Log = "NodeAlive";
            Write("NodeAlive.exe logging initialized successfully.");
        }

        public static void Write(string message)
        {
            UnityEngine.Debug.LogError(message);
            NAEventLog.WriteEntry("NodeAlive.exe" + System.Environment.NewLine + message, System.Diagnostics.EventLogEntryType.Information);
        }
        public static void WriteWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
            NAEventLog.WriteEntry("NodeAlive.exe" + System.Environment.NewLine + message, System.Diagnostics.EventLogEntryType.Warning);
        }
        public static void WriteError(string message)
        {
            UnityEngine.Debug.LogError(message);
            NAEventLog.WriteEntry("NodeAlive.exe" + System.Environment.NewLine + message, System.Diagnostics.EventLogEntryType.Error);
        }
    }