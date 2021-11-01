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
    public static string timeformat = "yyyy-MM-dd HH:mm:ss"; 
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
    public class TableStructure
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
        public TableStructure(string name, List<Attribute> attributes)
		{
            Name = name;
            Attributes = attributes;
		}
	}
    public class RecordStructure
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
    public class Schema
    {
        public static List<Data.TableStructure> tables = new List<TableStructure>()
        {
            new TableStructure("map", new List<TableStructure.Attribute>()
			{
                new TableStructure.Attribute("id", "INTEGER PRIMARY KEY"),
                new TableStructure.Attribute("name", "TEXT"),
                new TableStructure.Attribute("location", "TEXT"),
                new TableStructure.Attribute("latitude", "REAL"),
                new TableStructure.Attribute("longitude", "REAL"),
                new TableStructure.Attribute("zoom", "INTEGER"),
                new TableStructure.Attribute("guid", "TEXT"),
                new TableStructure.Attribute("date_created", "TEXT"),
                new TableStructure.Attribute("date_activated", "TEXT")
			}),
            new TableStructure("location", new List<TableStructure.Attribute>()
			{
                new TableStructure.Attribute("id", "INTEGER PRIMARY KEY"),
                new TableStructure.Attribute("map_id", "INTEGER"),
                new TableStructure.Attribute("address", "TEXT"),
                new TableStructure.Attribute("latitude", "REAL"),
                new TableStructure.Attribute("longitude", "REAL"),
                new TableStructure.Attribute("guid", "TEXT"),
                new TableStructure.Attribute("date_created", "TEXT")
			})
        };
        public class Table
		{
            public override string ToString()
			{
                var output = "";
                Type type = this.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    output += property.Name + ": " + property.GetValue(this, null) + Environment.NewLine;
                }
                return output;
			}
            public int ID { get;set; }
            public string GUID { get;set; }
            public DateTime DateCreated { get;set; }
            public class Location : Table
		    {
                public int MapID { get;set; }
                public string Address { get;set; }
                public float Latitude { get;set; }
                public float Longitude { get;set; }
                public override string ToString()
			    {
                    var output = "";
                    Type type = this.GetType();
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        output += property.Name + ": " + property.GetValue(this, null) + Environment.NewLine;
                    }
                    return output;
			    }
                
		    }
            public class Map : Table
		    {
                // public int ID { get;set; }
                public string Name { get;set; }
                public string Target { get;set; }
                public float Latitude { get;set; }
                public float Longitude { get;set; }
                public int Zoom { get;set; }
                // public string Guid { get;set; }
                // public DateTime DateCreated { get;set; }
                public DateTime DateActivated { get;set; }
                public override string ToString()
			    {
                    var output = "";
                    Type type = this.GetType();
                    PropertyInfo[] properties = type.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        output += property.Name + ": " + property.GetValue(this, null) + Environment.NewLine;
                    }
                    return output;
			    }
		    }
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
    public static void Clear()
	{
        // clear data
        foreach(var table in Schema.tables)
		{
            DropTable(table.Name);
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
    public static void Populate()
	{
        foreach(var table in Schema.tables)
		{
            CreateTable(table);
		}
        Insert("map", new List<RecordStructure.Attribute>()
        {
            new RecordStructure.Attribute("name", "Map of Buffalo, New York"),
            new RecordStructure.Attribute("location", "Buffalo, New York"),
            new RecordStructure.Attribute("latitude", "42.8865"),
            new RecordStructure.Attribute("longitude", "-78.8784"),
            new RecordStructure.Attribute("zoom", "12"),
            new RecordStructure.Attribute("guid", System.Guid.NewGuid().ToString()),
            new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat)),
            new RecordStructure.Attribute("date_activated", DateTime.Now.ToString(timeformat))
        });
        Insert("location", new List<RecordStructure.Attribute>()
        {
            new RecordStructure.Attribute("map_id", "1"),
            new RecordStructure.Attribute("address", "1 Main St, Buffalo, New York 14203"),
            new RecordStructure.Attribute("latitude", "42.8800"),
            new RecordStructure.Attribute("longitude", "-78.8764"),
            new RecordStructure.Attribute("guid", System.Guid.NewGuid().ToString()),
            new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat))
        });
    }
    public static void CreateTable(TableStructure table)
	{
        string query = 
            "CREATE TABLE IF NOT EXISTS " + table.Name;
        query += "(" + System.Environment.NewLine;
        var attributes = from a in table.Attributes select a;
        var last = attributes.Last<TableStructure.Attribute>();
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
    public static void Insert(string tableName, List<RecordStructure.Attribute> columnValuePairs, bool log = false)
	{
        // SQLite syntax:
        // INSERT INTO table (column1,column2 ,..)
        // VALUES( value1,	value2 ,...);
        var pairs = from p in columnValuePairs select p;
        string query = 
            "INSERT INTO " + tableName;
        query += "(" + System.Environment.NewLine;
        var last = pairs.Last<RecordStructure.Attribute>();
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
    public static void Reset()
	{
        Clear();
        Populate();
	}
    /*
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
    */
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
    public static class Select
	{
        public static List<Schema.Table.Location> Location()
	    {
            var returnList = new List<Schema.Table.Location>();      
            var query = "SELECT * FROM location;";
            var log = true;
			if(log) Log.Write("Used query: \"" + System.Environment.NewLine + query + "\"");
            string connectionString = "URI=file:" + File.fullName;
            try
		    {
                IDbConnection connection = new SqliteConnection(connectionString);
                IDbCommand command;
                IDataReader reader;
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = query;
                reader = command.ExecuteReader();
			    while(reader.Read())
		        {
                    Schema.Table.Location location = new Schema.Table.Location();
                    int index = 0;
                    int id;
                    Int32.TryParse(reader[index++].ToString(), out id);
                    location.ID = id;
                    int map_id;
                    Int32.TryParse(reader[index++].ToString(), out map_id);
                    location.MapID = map_id;
                    location.Address  = reader[index++].ToString();
                    float lat;
                    float.TryParse(reader[index++].ToString(), out lat);
                    location.Latitude = lat;
                    float lon;
                    float.TryParse(reader[index++].ToString(), out lon);
                    location.Longitude = lon;
                    location.GUID  = reader[index++].ToString();
                    location.DateCreated = DateTime.ParseExact(reader[index++].ToString(), timeformat, CultureInfo.InvariantCulture);
                    returnList.Add(location);
		        }
		    }
            catch
		    {
                Log.WriteError("Database connection failure at " + MethodBase.GetCurrentMethod().Name);
                Log.WriteWarning("Used query: \"" + System.Environment.NewLine + query + "\"");
		    }
            return returnList;
	    }
        public static List<Schema.Table.Map> Map()
	    {
            var maps = new List<Schema.Table.Map>();

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
                Schema.Table.Map map = new Schema.Table.Map();
                int id;
                // Debug.Log("fieldcount is" + reader.FieldCount);
                Int32.TryParse(reader[0].ToString(), out id);
                map.ID = id;
                map.Name = reader[1].ToString();
                map.Target  = reader[2].ToString();
                float lat;
                float.TryParse(reader[3].ToString(), out lat);
                map.Latitude = lat;
                float lon;
                float.TryParse(reader[4].ToString(), out lon);
                map.Longitude = lon;
                int zoomies;
                Int32.TryParse(reader[5].ToString(), out zoomies);
                map.Zoom = zoomies;
                map.GUID  = reader[6].ToString();
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
	}

}
