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

namespace Data
{
    public partial class Data
    {
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
                        location.MapGUID = reader[index++].ToString();
                        location.Address  = reader[index++].ToString();
                        float lat;
                        float.TryParse(reader[index++].ToString(), out lat);
                        location.Latitude = lat;
                        float lon;
                        float.TryParse(reader[index++].ToString(), out lon);
                        location.Longitude = lon;
                        location.GUID  = reader[index++].ToString();
                        location.DateCreated = DateTime.ParseExact(reader[index++].ToString(), timeformat, CultureInfo.InvariantCulture);
                        location.ChildGUID = reader[index++].ToString();
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
            public static List<Schema.Table.Entity> Entity()
	        {
                var returnList = new List<Schema.Table.Entity>();      
                var query = "SELECT * FROM entity;";
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
                        Schema.Table.Entity entity = new Schema.Table.Entity();
                        int index = 0;
                        int i; Int32.TryParse(reader[index++].ToString(), out i); entity.ID = i; 
                        entity.GUID  = reader[index++].ToString();
                        entity.DateCreated = DateTime.ParseExact(reader[index++].ToString(), timeformat, CultureInfo.InvariantCulture);
                        entity.LocationGUID = reader[index++].ToString();
                        int t; Int32.TryParse(reader[index++].ToString(), out t); entity.Type = t;
                        entity.ChildGUID = reader[index++].ToString();
                        returnList.Add(entity);
		            }
		        }
                catch
		        {
                    Log.WriteError("Database connection failure at " + MethodBase.GetCurrentMethod().Name);
                    Log.WriteWarning("Used query: \"" + System.Environment.NewLine + query + "\"");
		        }
                return returnList;
	        }
            public static List<Schema.Table.Node> Node()
	        {
                var returnList = new List<Schema.Table.Node>();      
                var query = "SELECT * FROM node;";
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
                        Schema.Table.Node node = new Schema.Table.Node();
                        int index = 0;
                        int i; Int32.TryParse(reader[index++].ToString(), out i); node.ID = i; 
                        node.EntityGUID = reader[index++].ToString();
                        node.Name = reader[index++].ToString();
                        node.GUID  = reader[index++].ToString();
                        node.DateCreated = DateTime.ParseExact(reader[index++].ToString(), timeformat, CultureInfo.InvariantCulture);
                        int t; Int32.TryParse(reader[index++].ToString(), out t); node.Type = t;
                        node.MapGUID  = reader[index++].ToString();
                        returnList.Add(node);
		            }
		        }
                catch
		        {
                    Log.WriteError("Database connection failure at " + MethodBase.GetCurrentMethod().Name);
                    Log.WriteWarning("Used query: \"" + System.Environment.NewLine + query + "\"");
		        }
                return returnList;
	        }
            public static List<Schema.Table.Cluster> Cluster()
	        {
                var returnList = new List<Schema.Table.Cluster>();      
                var query = "SELECT * FROM cluster;";
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
                        Schema.Table.Cluster cluster = new Schema.Table.Cluster();
                        int index = 0;
                        int i; Int32.TryParse(reader[index++].ToString(), out i); cluster.ID = i; 
                        cluster.EntityGUID = reader[index++].ToString();
                        cluster.Name = reader[index++].ToString();
                        cluster.GUID  = reader[index++].ToString();
                        cluster.DateCreated = DateTime.ParseExact(reader[index++].ToString(), timeformat, CultureInfo.InvariantCulture);
                        int t; Int32.TryParse(reader[index++].ToString(), out t); cluster.Type = t;
                        cluster.MapGUID  = reader[index++].ToString();
                        returnList.Add(cluster);
		            }
		        }
                catch
		        {
                    Log.WriteError("Database connection failure at " + MethodBase.GetCurrentMethod().Name);
                    Log.WriteWarning("Used query: \"" + System.Environment.NewLine + query + "\"");
		        }
                return returnList;
	        }
            public static List<Schema.Table.Dispatch> Dispatch()
	        {
                var returnList = new List<Schema.Table.Dispatch>();      
                var query = "SELECT * FROM dispatch;";
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
                        Schema.Table.Dispatch dispatch = new Schema.Table.Dispatch();
                        int index = 0;
                        int i; Int32.TryParse(reader[index++].ToString(), out i); dispatch.ID = i; 
                        dispatch.EntityGUID = reader[index++].ToString();
                        dispatch.Name = reader[index++].ToString();
                        dispatch.GUID  = reader[index++].ToString();
                        dispatch.DateCreated = DateTime.ParseExact(reader[index++].ToString(), timeformat, CultureInfo.InvariantCulture);
                        int t; Int32.TryParse(reader[index++].ToString(), out t); dispatch.Type = t;
                        dispatch.MapGUID  = reader[index++].ToString();
                        returnList.Add(dispatch);
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

}