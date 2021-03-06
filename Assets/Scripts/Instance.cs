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
    public static Queue<(string message, float showForDelay)> MessageQueue = 
        new Queue<(string message, float showForDelay)>
        (
            new[] { ("Instance initialized.", 0.25f) }
        );
    public static bool NODEOFFLINE = false;
    public static List<Data.Data.Schema.Table.Location> UnassignedLocations = new List<Data.Data.Schema.Table.Location>();
    public static List<Data.Data.Schema.Table.Dispatch> Dispatches = new List<Data.Data.Schema.Table.Dispatch>();
    public static List<Data.Data.Schema.Table.Node> Nodes = new List<Data.Data.Schema.Table.Node>();
    public static List<Data.Data.Schema.Table.Cluster> Clusters = new List<Data.Data.Schema.Table.Cluster>();
    public static List<Data.Data.Schema.Table.Result> Results = new List<Data.Data.Schema.Table.Result>();
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
    public static class ActiveLocation
    { 
        public static string MapGUID { get;set; }
        public static string Address { get;set; }
        public static float Latitude { get;set; }
        public static float Longitude { get;set; }
        public static string ChildGUID { get;set; }
        
        public static string GUID { get; set; }
    }
    public static Data.Data.Schema.Table.Node ActiveNode = new Data.Data.Schema.Table.Node();
    public static Data.Data.Schema.Table.Dispatch ActiveDispatch = new Data.Data.Schema.Table.Dispatch();
    public static void ClearActiveNode()
	{
        ActiveNode = new Data.Data.Schema.Table.Node();
	}
    
    public static string SetActiveNodeNullReferenceMessageInput = "GUID input to SetActiveNode is null or empty. Active node not set.";
    public static void SetActiveNode(string guid)
	{
        
        
        if(String.IsNullOrEmpty(guid))
        {
            throw new NullReferenceException(SetActiveNodeNullReferenceMessageInput);
        }
        var node = (from n in Data.Data.Select.Node()
                    where n.GUID == guid
                    select n).FirstOrDefault();
        if(node == null)
        {
            throw new NullReferenceException("LINQ failure at SetActiveNode assignment in Instance.cs");
        }
        ActiveNode = node;
	}
    public static string SetActiveLocationNullReferenceMessageInput = "GUID input to SetActiveLocation is null or empty. Active location not set.";
    public static void SetActiveLocation(string guid)
	{
        if(String.IsNullOrEmpty(guid))
        {
            throw new NullReferenceException(SetActiveLocationNullReferenceMessageInput);
        }
        ActiveLocation.GUID = guid;
        var location = (from l in Data.Data.Select.Location()
                        where l.GUID == guid
                        select l).FirstOrDefault();
        if(location == null)
        {
            throw new NullReferenceException("LINQ failure at SetActiveLocation assignment in Instance.cs");
        }
        ActiveLocation.MapGUID = location.MapGUID;
        ActiveLocation.Address = location.Address;
        ActiveLocation.Latitude = location.Latitude;
        ActiveLocation.Longitude = location.Longitude;
        ActiveLocation.ChildGUID = "unassigned";
	}
    public static string SetActiveMapNullReferenceMessageInput = "GUID input to SetActiveMap is null or empty. Active map not set.";
    public static void SetActiveMap(string guid)
	{
        if(String.IsNullOrEmpty(guid))
        {
            throw new NullReferenceException(SetActiveMapNullReferenceMessageInput);
        }
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
    public static string SelectedLocationGUID;
    
    public static bool AdminLocked { get; private set;}
    public static void AdminLock()
	{
        AdminLocked = true;
	}
    public static void AdminUnlock()
	{
        AdminLocked = false;
	}
 
    public static string SelectedMapGUID;
    public static void SelectMapGUID(string guid)
	{
        SelectedMapGUID = guid;
	}
    public static void SelectLocationGUID(string guid)
	{
        SelectedLocationGUID = guid;
	}
    public static void Message(string status, float showForDelay = 0.25f)
	{
        MessageQueue.Enqueue((status, showForDelay));
	}
    public static void DisableNodeUI()
	{

	}
    public static void EnableNodeUI()
	{

	}
    public static string DebugProperties(object obj)
	{
        string log = "";
        foreach(var property in obj.GetType().GetProperties())
		{ 
			log += property.Name + " : " + property.GetValue(obj, null) + System.Environment.NewLine;
		}
        return log;
	}

}

