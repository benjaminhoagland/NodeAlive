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
                    new TableStructure.Attribute("date_created", "TEXT"),
                    new TableStructure.Attribute("child_guid", "TEXT")
			    }),
                new TableStructure("entity", new List<TableStructure.Attribute>()
			    {
                    new TableStructure.Attribute("id", "INTEGER PRIMARY KEY"),
                    new TableStructure.Attribute("guid", "TEXT"),
                    new TableStructure.Attribute("date_created", "TEXT"),
                    new TableStructure.Attribute("location_guid", "TEXT"),
                    new TableStructure.Attribute("type", "INTEGER"),
                    new TableStructure.Attribute("child_guid", "TEXT")
			    }),
                new TableStructure("node", new List<TableStructure.Attribute>()
			    {
                    new TableStructure.Attribute("id", "INTEGER PRIMARY KEY"),
                    new TableStructure.Attribute("entity_guid", "TEXT"),
                    new TableStructure.Attribute("name", "TEXT"),
                    new TableStructure.Attribute("guid", "TEXT"),
                    new TableStructure.Attribute("date_created", "TEXT"),
                    new TableStructure.Attribute("type", "INTEGER")
			    }),
                new TableStructure("cluster", new List<TableStructure.Attribute>()
			    {
                    new TableStructure.Attribute("id", "INTEGER PRIMARY KEY"),
                    new TableStructure.Attribute("entity_guid", "TEXT"),
                    new TableStructure.Attribute("name", "TEXT"),
                    new TableStructure.Attribute("guid", "TEXT"),
                    new TableStructure.Attribute("date_created", "TEXT"),
                    new TableStructure.Attribute("type", "INTEGER")
			    }),
                new TableStructure("dispatch", new List<TableStructure.Attribute>()
			    {
                    new TableStructure.Attribute("id", "INTEGER PRIMARY KEY"),
                    new TableStructure.Attribute("entity_guid", "TEXT"),
                    new TableStructure.Attribute("name", "TEXT"),
                    new TableStructure.Attribute("guid", "TEXT"),
                    new TableStructure.Attribute("date_created", "TEXT"),
                    new TableStructure.Attribute("type", "INTEGER")
			    })
            };
            public class Table
		    {
                public static Dictionary<int, string> EntityTypes = new Dictionary<int, string>()
			    {
                    { 0, "Node" },
                    { 1, "Cluster" },
                    { 2, "Dispatch" }
			    };
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
                public class Entity : Table
		        {
                    public string LocationGUID { get;set; }
                    public int Type { get;set; }
                    public string ChildGUID { get;set; }
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
                public class Node : Entity
		        {
                    public string EntityGUID { get;set; }
                    public string Name { get;set; }
                    public new int Type { get;set; } = 0;
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
                public class Cluster : Entity
		        {
                    public string EntityGUID { get;set; }
                    public string Name { get;set; }
                    public new int Type { get;set; } = 1;
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
                public class Dispatch : Entity
		        {
                    public string EntityGUID { get;set; }
                    public string Name { get;set; }
                    public new int Type { get;set; } = 2;
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
                public class Location : Table
		        {
                    public int MapID { get;set; }
                    public string Address { get;set; }
                    public float Latitude { get;set; }
                    public float Longitude { get;set; }
                    public string ChildGUID { get;set; }
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
	}    
}
