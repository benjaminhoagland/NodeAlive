using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
public class Data
{
   public static class File
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
    public static void Clear()
	{
        // clear data
        Debug.Log("clear db");
	}
    public static void Populate()
	{
        // populate data
        Debug.Log("populate db");
	}
    public static void Reset()
	{
        Clear();
        Populate();
        Debug.Log("reset db");
	}
    public static void Initialize() 
    {
        Log.Write("Initializing database...");
        if(System.IO.File.Exists(File.fullName))
        {
			Log.Write("Database found at " + File.fullName);
            // Log.Write("Continuing...");
            
            // FLAG:TODO test connection
            // FLAG:TODO write log and exit app on connection failure
            // FLAG:TODO check data initialization
    
        }
		else
		{
			Log.WriteError("Database not found at " + File.fullName);
			Log.WriteError("Exiting Application...");
			Instance.InitializationFailure = true;
			Application.Quit();

            
		}

    }
    public static List<string> SelectWhatFromWhere (string what, string from, string where = null)
	{
        string query = null;
        List<string> results = new List<string>();

        if(what == null)
		{
            query = 
                "SELECT " + what +
                "FROM " + from + ";";
		}
        else
		{
            query = 
                "SELECT " + what +
                "FROM " + from +
                "WHERE " + where + ";";
		}
        try
		{
            string connectionString = "URI=file:" + File.fullName;
            IDbConnection connection = new SqliteConnection(connectionString);
            IDbCommand command;
            IDataReader reader;
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            reader = command.ExecuteReader();
            while(reader.Read())
			{
                results.Add(reader[0].ToString());
			}

		}
        catch
		{
            Log.WriteError("Database connection failure at Data.SelectWhatFromWhere()");
		}
        return results;
	}

}
