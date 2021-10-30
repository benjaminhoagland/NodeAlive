using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System.Reflection;
using System.Linq;
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
                new Table.Attribute("ID", "INTEGER PRIMARY KEY"),
                new Table.Attribute("name", "TEXT"),
                new Table.Attribute("location", "TEXT"),
                new Table.Attribute("latitude", "REAL"),
                new Table.Attribute("longitude", "REAL"),
                new Table.Attribute("zoom", "INTEGER"),
                new Table.Attribute("guid", "text")
			})
        };
	}
    public class Record
	{
        public class ColumnValuePair
		{
            public string Column { get; set; }
            public string Value { get; set; }
            public ColumnValuePair()
            { 
                Column = null;
                Value = null;
            }
            public ColumnValuePair(string column, string value)
			{
                Column = column;
                Value = value;
			}
		}
        public List<ColumnValuePair> columnValuePairs;
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
    public static void Populate()
	{
        foreach(var table in Schema.tables)
		{
            CreateTable(table);
		}
        Insert("map", new List<Record.ColumnValuePair>()
        {
            new Record.ColumnValuePair("name", "Map of Buffalo, New York"),
            new Record.ColumnValuePair("location", "Buffalo, New York"),
            new Record.ColumnValuePair("latitude", "42.8865"),
            new Record.ColumnValuePair("longitude", "-78.8784"),
            new Record.ColumnValuePair("zoom", "12"),
            new Record.ColumnValuePair("guid", System.Guid.NewGuid().ToString())
        });
    }
    public static void Reset()
	{
        Clear();
        Populate();
	}
    public static void Insert(string tableName, List<Record.ColumnValuePair> columnValuePairs, bool log = false)
	{
        // SQLite syntax:
        // INSERT INTO table (column1,column2 ,..)
        // VALUES( value1,	value2 ,...);
        var pairs = from p in columnValuePairs select p;
        string query = 
            "INSERT INTO " + tableName;
        query += "(" + System.Environment.NewLine;
        var last = pairs.Last<Record.ColumnValuePair>();
        foreach(var p in pairs)
		{
            query += p.Column;
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
                Record record = new Record();
                foreach(int i in Enumerable.Range(1, reader.FieldCount))
				{
                    Record.ColumnValuePair columnValuePair = new Record.ColumnValuePair();
                    columnValuePair.Column = reader.GetName(i);
                    columnValuePair.Value = reader[i].ToString();
                    record.columnValuePairs.Add(columnValuePair);
				}
                records.Add(record);
			}

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
