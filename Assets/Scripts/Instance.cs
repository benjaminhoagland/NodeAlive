using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;
using Mapbox.Geocoding;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using System;
using TMPro;
using Mapbox;
using Data;

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
        // "Initalizing", 
        // "Initalizing.",
        // "Initalizing..", 
        // "Initalizing...", 
        "Instance initialized." 
    };
    public static class ActiveMap
	{
        public static string ID { get; set; }
        public static string Name { get; set; } = "unassigned";
        public static string Location { get; set; }
        public static string Latitude { get; set; }
        public static string Longitude { get; set; }
        public static string Zoom { get; set; }
        public static string GUID { get; set; }
	}
    public static void SetActiveMap(string guid)
	{
        if(guid.Length == 0 || guid == null) Log.WriteError("Failure at SetActiveMap from null input.");
        var id = Data.Data.SelectWhatFromWhere("id", "map", "guid = \'" + guid + "\'").FirstOrDefault();
        
        var log = false;
        ActiveMap.ID = id;
        if(log) Debug.Log(ActiveMap.ID);
        ActiveMap.Name = Data.Data.SelectWhatFromWhere("name", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.Name);
        ActiveMap.Location = Data.Data.SelectWhatFromWhere("location", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.Location);
        ActiveMap.Latitude = Data.Data.SelectWhatFromWhere("latitude", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.Latitude);
        ActiveMap.Longitude = Data.Data.SelectWhatFromWhere("longitude", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.Longitude);
        ActiveMap.Zoom = Data.Data.SelectWhatFromWhere("zoom", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.Zoom);
        ActiveMap.GUID = Data.Data.SelectWhatFromWhere("guid", "map", "id = \'" + id + "\'").FirstOrDefault();
        if(log) Debug.Log(ActiveMap.GUID);
	}
 
    public static string SelectedMapGUID;
    public static void SelectGUID(string guid)
	{
        SelectedMapGUID = guid;
	}
    public static void Message(string status)
	{
        MessageQueue.Add(status);
	}
    public static void DisableNodeUI()
	{

	}
    public static void EnableNodeUI()
	{

	}

}
