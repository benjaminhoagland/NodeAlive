using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
public class Data
{
    public static string connectionString = "URI=file:" + File.fullName;
    public class Table
	{
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
        public class Attribute
		{
            public string Name { get; set; }
            public string Type { get; set; }
            public Attribute(string name, string type)
			{
                Name = name;
                Type = type;
			}

		}
        public Table(string name, List<Attribute> attributes)
		{
            Name = name;
            Attributes = attributes;
		}
	}

    public class Schema
    {
        public static List<Table> tables = new List<Table>()
        {
            new Table("map", new List<Table.Attribute>()
			{
                new Table.Attribute("id", "INTEGER PRIMARY KEY"),
                new Table.Attribute("name", "TEXT"),
                new Table.Attribute("location", "TEXT"),
                new Table.Attribute("latitude", "REAL"),
                new Table.Attribute("longitude", "REAL"),
                new Table.Attribute("zoom", "INTEGER"),
                new Table.Attribute("guid", "TEXT"),
                new Table.Attribute("date_created", "TEXT"),
                new Table.Attribute("date_activated", "TEXT")
			})
        };
        public class Map
		{
            public int ID { get;set; }
            public string Name { get;set; }
            public string Location { get;set; }
            public float Latitude { get;set; }
            public float Longitude { get;set; }
            public int Zoom { get;set; }
            public string Guid { get;set; }
            public DateTime DateCreated { get;set; }
            public DateTime DateActivated { get;set; }
            
            public override string ToString()
			{
                var s = "";
                s += ID.ToString() + System.Environment.NewLine; 
                s += Name + System.Environment.NewLine; 
                s += Location + System.Environment.NewLine; 
                s += Latitude.ToString() + System.Environment.NewLine; 
                s += Longitude.ToString() + System.Environment.NewLine;
                s += Zoom.ToString() + System.Environment.NewLine;
                s += Guid + System.Environment.NewLine;
                s += DateCreated.ToString(timeformat) + System.Environment.NewLine;
                s += DateActivated.ToString(timeformat);
                return s;
			}

		}
	}
    public static List<Schema.Map> SelectMaps()
	{
        var maps = new List<Schema.Map>();

        string query = null;
      
        query = "SELECT * FROM map;";
        var log = false;
        if(log) Log.Write("Used query: \"" + System.Environment.NewLine + query + "\"");
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
            Schema.Map map = new Schema.Map();
            int id;
            // Debug.Log("fieldcount is" + reader.FieldCount);
            Int32.TryParse(reader[0].ToString(), out id);
            map.ID = id;
            map.Name = reader[1].ToString();
            map.Location  = reader[2].ToString();
            float lat;
            float.TryParse(reader[3].ToString(), out lat);
            map.Latitude = lat;
            float lon;
            float.TryParse(reader[4].ToString(), out lon);
            map.Longitude = lon;
            int zoomies;
            Int32.TryParse(reader[5].ToString(), out zoomies);
            map.Zoom = zoomies;
            map.Guid  = reader[6].ToString();
            map.DateCreated = DateTime.ParseExact(reader[7].ToString(), timeformat, CultureInfo.InvariantCulture);
            map.DateActivated = DateTime.ParseExact(reader[8].ToString(), timeformat, CultureInfo.InvariantCulture); 
            maps.Add(map);
		}

        try
		{
		}
        catch
		{
            Log.WriteError("Database connection failure at " + MethodBase.GetCurrentMethod().Name);
            Log.WriteWarning("Used query: \"" + System.Environment.NewLine + query + "\"");
		}
        return maps;
	}
    public class Record
	{
        public class Attribute
		{
            public string Name { get; set; }
            public string Value { get; set; }
            public Attribute()
            { 
                Name = null;
                Value = null;
            }
            public Attribute(string column, string value)
			{
                Name = column;
                Value = value;
			}
		}
        public List<Attribute> attributes;
	}
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
        foreach(var table in Schema.tables)
		{
            DropTable(table.Name);
		}
    }
    public static string timeformat = "yyyy-MM-dd HH:mm:ss";
    public static void Populate()
	{
        foreach(var table in Schema.tables)
		{
            CreateTable(table);
		}
        Insert("map", new List<Record.Attribute>()
        {
            new Record.Attribute("name", "Map of Buffalo, New York"),
            new Record.Attribute("location", "Buffalo, New York"),
            new Record.Attribute("latitude", "42.8865"),
            new Record.Attribute("longitude", "-78.8784"),
            new Record.Attribute("zoom", "12"),
            new Record.Attribute("guid", System.Guid.NewGuid().ToString()),
            new Record.Attribute("date_created", DateTime.Now.ToString(timeformat)),
            new Record.Attribute("date_activated", DateTime.Now.ToString(timeformat))
        });
    }
    public static void Reset()
	{
        Clear();
        Populate();
	}
    public static void Insert(string tableName, List<Record.Attribute> columnValuePairs, bool log = false)
	{
        // SQLite syntax:
        // INSERT INTO table (column1,column2 ,..)
        // VALUES( value1,	value2 ,...);
        var pairs = from p in columnValuePairs select p;
        string query = 
            "INSERT INTO " + tableName;
        query += "(" + System.Environment.NewLine;
        var last = pairs.Last<Record.Attribute>();
        foreach(var p in pairs)
		{
            query += p.Name;
            if(!p.Equals(last)) { query += ","; }
            query += System.Environment.NewLine;
		}
        query += ")" + System.Environment.NewLine;
        query += "VALUES (" + System.Environment.NewLine;
        foreach(var p in pairs)
		{
            query += @"'" + p.Value + @"'";
            if(!p.Equals(last)) { query += ","; }
            query += System.Environment.NewLine;
		}
        query += ");";

        if(log) Log.Write("Used query: \"" + System.Environment.NewLine + query + "\"");
        // Debug.Log(query);
        // return;
        string connectionString = "URI=file:" + File.fullName;
        IDbConnection connection = new SqliteConnection(connectionString);
        IDbCommand command;
        try
		{
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
		}
        catch
		{
            Log.WriteError("Database connection failure at " + MethodBase.GetCurrentMethod().Name);
		    Log.WriteWarning("Used query: " + System.Environment.NewLine + query);
        }
	}
    public static void CreateTable(Table table)
	{
        string query = 
            "CREATE TABLE IF NOT EXISTS " + table.Name;
        query += "(" + System.Environment.NewLine;
        var attributes = from a in table.Attributes select a;
        var last = attributes.Last<Table.Attribute>();
        foreach(var a in attributes)
		{
            query += a.Name + " " + a.Type; 
            if(!a.Equals(last)) { query += ","; }
            query += System.Environment.NewLine;
		}
        query += ");";
        string connectionString = "URI=file:" + File.fullName;
        IDbConnection connection = new SqliteConnection(connectionString);
        IDbCommand command;
        try
		{
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
		}
        catch
		{
            Log.WriteError("Database connection failure at " + MethodBase.GetCurrentMethod().Name);
		    Log.WriteWarning("Used query: " + System.Environment.NewLine + query);
        }
	}
    public static void DropTable(string tableName)
	{
        string query = "DROP TABLE IF EXISTS " + tableName;
        string connectionString = "URI=file:" + File.fullName;
        IDbConnection connection = new SqliteConnection(connectionString);
        IDbCommand command;
        try
		{
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
		}
        catch
		{
            Log.WriteError("Database connection failure at " + MethodBase.GetCurrentMethod().Name);
		    Log.WriteWarning("Used query: " + System.Environment.NewLine + query);
        }
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
    public static List<Record> SelectStar (string from, bool log = false)
	{
        string query = null;
        List<Record> records = new List<Record>();

        query = 
            "SELECT * " +
            "FROM " + from + ";";

        if(log) Log.Write("Used query: \"" + System.Environment.NewLine + query + "\"");
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
                Record record = new Record();
                foreach(int i in Enumerable.Range(1, reader.FieldCount))
				{
                    Record.Attribute attribute = new Record.Attribute();
                    attribute.Name = reader.GetName(i); 
                    Debug.Log(reader.GetName(0));
                    Debug.Log(reader.GetName(i));
                    attribute.Value = reader[i].ToString();
                    record.attributes.Add(attribute);
				}
                records.Add(record);
			}

        try
		{
		}
        catch
		{
            Log.WriteError("Database connection failure at " + MethodBase.GetCurrentMethod().Name);
            Log.WriteWarning("Used query: \"" + System.Environment.NewLine + query + "\"");
		}
        return records;
	}
    public static List<string> SelectWhatFromWhere (string what, string from, string where = null, bool log = false)
	{
        string query = null;
        List<string> results = new List<string>();

        if(where == null)
		{
            query = 
                "SELECT " + what +
                "FROM " + from + ";";
		}
        else
		{
            query = 
                "SELECT " + what +
                " FROM " + from +
                " WHERE " + where + ";";
		}
        if(log) Log.Write("Used query: \"" + System.Environment.NewLine + query + "\"");
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
            Log.WriteError("Database connection failure at " + MethodBase.GetCurrentMethod().Name);
            Log.WriteWarning("Used query: \"" + System.Environment.NewLine + query + "\"");
		}
        return results;
	}

}
