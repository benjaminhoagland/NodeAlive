using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public static class Instance
{
    public static bool UIEngaged = false;
    public static List<string> Status = new List<string>() 
    { 
        "Starting",
        "Loading database",
        "Checking database connection",
        "Connecting to map resources",
        "Finding NASVC service",
        "Starting",
        "Starting",
        "Complete",
    };
    public static List<string> FailureStatus = new List<string>() 
    { 
        "Starting",
        "Loading database failure",
        "Database connection failure",
        "Map resources connection failure",
        "NASVC service failure",
        "Starting",
        "Starting",
        "Exiting",
    };
    public static bool InitializationFailure = false;
    public static List<string> MessageQueue = new List<string>() 
    { 
        "Initalizing", 
        "Initalizing.",
        "Initalizing..", 
        "Initalizing...", 
        "Instance initialized." 
    };
    static class ActiveMap
	{
        public static string ID { get; set; }
        public static string Name { get; set; }
        public static string Location { get; set; }
        public static string Latitude { get; set; }
        public static string Longitude { get; set; }
        public static string Zoom { get; set; }
        public static string GUID { get; set; }
	}
    public static void SetActiveMap(string guid)
	{
        
        var id = Data.SelectWhatFromWhere("id", "map", "guid = \'" + guid + "\'").FirstOrDefault();
        var log = false;
        ActiveMap.ID = id;
        if(log) Debug.Log(ActiveMap.ID);
        ActiveMap.Name = Data.SelectWhatFromWhere("name", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.Name);
        ActiveMap.Location = Data.SelectWhatFromWhere("location", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.Location);
        ActiveMap.Latitude = Data.SelectWhatFromWhere("latitude", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.Latitude);
        ActiveMap.Longitude = Data.SelectWhatFromWhere("longitude", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.Longitude);
        ActiveMap.Zoom = Data.SelectWhatFromWhere("zoom", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.Zoom);
        ActiveMap.GUID = Data.SelectWhatFromWhere("guid", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.GUID);
        
		try
		{
        if(log) Debug.Log(
            ActiveMap.ID.ToString() + System.Environment.NewLine +
            ActiveMap.Name.ToString() + System.Environment.NewLine + 
            ActiveMap.Location.ToString() + System.Environment.NewLine +
            ActiveMap.Latitude.ToString() + System.Environment.NewLine +
            ActiveMap.Longitude.ToString() + System.Environment.NewLine +
            ActiveMap.Zoom.ToString() + System.Environment.NewLine +
            ActiveMap.GUID.ToString());
		}
        catch
		{
            Log.WriteError("Error logging active map.");
		}
	}
    public static void Message(string status)
	{
        MessageQueue.Add(status);
	}
}
