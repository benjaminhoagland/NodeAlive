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
                new RecordStructure.Attribute("guid", "75da11ac-7922-4aa9-8cf9-35906d5592dd"),
                new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat)),
                new RecordStructure.Attribute("child_guid", "39bef964-1ae8-4193-b5cd-6b9bc5f587b2")
            });
            Insert("entity", new List<RecordStructure.Attribute>()
            {
                new RecordStructure.Attribute("guid", "39bef964-1ae8-4193-b5cd-6b9bc5f587b2"),
                new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat)),
                new RecordStructure.Attribute("location_guid", "75da11ac-7922-4aa9-8cf9-35906d5592dd"),
                new RecordStructure.Attribute("type", 0.ToString()),
                new RecordStructure.Attribute("child_guid", "273d8c97-9c61-4452-9b9d-9b03562e0029")
            });
            Insert("node", new List<RecordStructure.Attribute>()
            {
                new RecordStructure.Attribute("entity_guid", "39bef964-1ae8-4193-b5cd-6b9bc5f587b2"),
                new RecordStructure.Attribute("name", "this is a node"),
                new RecordStructure.Attribute("guid", "273d8c97-9c61-4452-9b9d-9b03562e0029"),
                new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat)),
                new RecordStructure.Attribute("type", 0.ToString())
            });

            Insert("location", new List<RecordStructure.Attribute>()
            {
                new RecordStructure.Attribute("map_id", "1"),
                new RecordStructure.Attribute("address", "100 Main St, Buffalo, New York 14203"),
                new RecordStructure.Attribute("latitude", "42.8795"),
                new RecordStructure.Attribute("longitude", "-78.8766"),
                new RecordStructure.Attribute("guid", "0b525bc3-e5b0-4d21-b4f3-e7d60d606640"),
                new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat)),
                new RecordStructure.Attribute("child_guid", "7bbfd29d-74bd-46d4-b2d0-0b10701012cd")
            });
            Insert("entity", new List<RecordStructure.Attribute>()
            {
                new RecordStructure.Attribute("guid", "7bbfd29d-74bd-46d4-b2d0-0b10701012cd"),
                new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat)),
                new RecordStructure.Attribute("location_guid", "0b525bc3-e5b0-4d21-b4f3-e7d60d606640"),
                new RecordStructure.Attribute("type", 1.ToString()),
                new RecordStructure.Attribute("child_guid", "064b1f51-a3d3-458a-9ac2-fa12c3eacc6a")
            });
            Insert("cluster", new List<RecordStructure.Attribute>()
            {
                new RecordStructure.Attribute("entity_guid", "7bbfd29d-74bd-46d4-b2d0-0b10701012cd"),
                new RecordStructure.Attribute("name", "this is a cluster"),
                new RecordStructure.Attribute("guid", "064b1f51-a3d3-458a-9ac2-fa12c3eacc6a"),
                new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat)),
                new RecordStructure.Attribute("type", 1.ToString())
            });

            Insert("location", new List<RecordStructure.Attribute>()
            {
                new RecordStructure.Attribute("map_id", "1"),
                new RecordStructure.Attribute("address", "1000 Main St, Buffalo, New York 14203"),
                new RecordStructure.Attribute("latitude", "42.9011"),
                new RecordStructure.Attribute("longitude", "-78.8696"),
                new RecordStructure.Attribute("guid", "9b213a9b-6d95-4a7f-9558-ab1f58d739c1"),
                new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat)),
                new RecordStructure.Attribute("child_guid", "7a55e1cc-b43a-4b4c-889a-f9e54c5797b8")
            });
            Insert("entity", new List<RecordStructure.Attribute>()
            {
                new RecordStructure.Attribute("guid", "7a55e1cc-b43a-4b4c-889a-f9e54c5797b8"),
                new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat)),
                new RecordStructure.Attribute("location_guid", "9b213a9b-6d95-4a7f-9558-ab1f58d739c1"),
                new RecordStructure.Attribute("type", 2.ToString()),
                new RecordStructure.Attribute("child_guid", "b742cfcc-6e43-44c6-9697-4af3030f4117")
            });
            Insert("dispatch", new List<RecordStructure.Attribute>()
            {
                new RecordStructure.Attribute("entity_guid", "7a55e1cc-b43a-4b4c-889a-f9e54c5797b8"),
                new RecordStructure.Attribute("name", "this is a dispatch"),
                new RecordStructure.Attribute("guid", "b742cfcc-6e43-44c6-9697-4af3030f4117"),
                new RecordStructure.Attribute("date_created", DateTime.Now.ToString(timeformat)),
                new RecordStructure.Attribute("type", 2.ToString())
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
	}
}