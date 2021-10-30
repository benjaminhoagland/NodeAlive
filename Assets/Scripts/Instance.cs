using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static List<string> MessageQueue = new List<string>() { "Instance initialized." };
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
        var id = Data.SelectWhatFromWhere("id", "map", "guid = " + guid)[0];
        ActiveMap.ID = id;
        ActiveMap.Name = Data.SelectWhatFromWhere("name", "map", "id = " + id)[0];
        ActiveMap.Location = Data.SelectWhatFromWhere("location", "map", "id = " + id)[0];
        ActiveMap.Latitude = Data.SelectWhatFromWhere("latitude", "map", "id = " + id)[0];
        ActiveMap.Longitude = Data.SelectWhatFromWhere("longitude", "map", "id = " + id)[0];
        ActiveMap.Zoom = Data.SelectWhatFromWhere("zoom", "map", "id = " + id)[0];

        Debug.Log(
            ActiveMap.ID + System.Environment.NewLine +
            ActiveMap.Name + System.Environment.NewLine + 
            ActiveMap.Location + System.Environment.NewLine +
            ActiveMap.Latitude + System.Environment.NewLine +
            ActiveMap.Longitude + System.Environment.NewLine +
            ActiveMap.Zoom + System.Environment.NewLine +
            ActiveMap.GUID);
	}
    public static void Message(string status)
	{
        MessageQueue.Add(status);
	}
}
