using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
public class Data
{
   public static class Filesystem
    {
        // public static string filePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public static string filePath = @"C:\NodeAlive\NASVC\NASVC\bin\Debug\";
        public static string directoryPath = System.IO.Path.GetDirectoryName(filePath);
        public const string filename = "NADB.sqlite";
        public static string fullName
        {
            // example: "C:\NodeAlive\NASVC\NASVC\bin\Debug\NADB.sqlite"
            get
            {
                return Path.Combine(directoryPath, filename);
            }
        }
    }
    public static void Initialize() 
    {
        Log.Write("Initializing database...");
        if(File.Exists(Filesystem.fullName))
        {
            Log.Write("Database found at " + Filesystem.fullName);
            Log.Write("Continuing...");

            // FLAG:TODO test connection
            // FLAG:TODO write log and exit app on connection failure
            // FLAG:TODO check data initialization
    
        }
		else
		{
            Log.WriteError("Database not found at " + Filesystem.fullName);
            Log.WriteError("Exiting Application...");
            Application.Quit();
            
		}

    }
    public static string GetDBName()
    {
        return "The database name is " + Filesystem.fullName;
    }

}
