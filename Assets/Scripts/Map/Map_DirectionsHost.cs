using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using System;

	using Mapbox.Unity.Utilities;

public class Map_DirectionsHost : MonoBehaviour
{
	public class DirectionLocation
	{
        public static List<DirectionLocation> List = new List<DirectionLocation>();
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
        public GameObject dLGameObject { get; set; }
        public GameObject EndObject { get; set; }
        public GameObject StartObject { get; set; }
        public string NodeGUID { get; set; }
        public Vector2d StartCoordinate { get; set; }
        public Vector2d EndCoordinate { get; set; }
        public DirectionLocation(Vector3 start, Vector3 end, GameObject gameObject, GameObject endObject, GameObject startObject, string nodeGUID, Vector2d startCoordinate, Vector2d endCoordinate)
		{
            StartPosition = start;
            EndPosition = end;
            dLGameObject = gameObject;
            StartObject = startObject;
            EndObject = endObject;
            NodeGUID = nodeGUID;
            StartCoordinate = startCoordinate;
            EndCoordinate = endCoordinate;
		}
        public static void Remove(DirectionLocation directionLocation)
		{
            List.Remove(directionLocation);
		}
	}
    [SerializeField]GameObject DirectionsEntity;
    [SerializeField]GameObject NodeHost;
    [SerializeField]GameObject DispatchHost;
	Coroutine c;
	[SerializeField] float interval = 1f;
	bool ready = true;
    WaitForSeconds wait;
    AbstractMap map;

    void Awake()
    {
        wait = new WaitForSeconds(interval);
        map = FindObjectOfType<AbstractMap>();
        DirectionLocation.List = new List<DirectionLocation>();
    }
    
    void Update()
    {
        if(ready) StartCoroutine(UpdateGameObjects());
        foreach(var directionLocation in DirectionLocation.List)
		{
            var startPosition = map.GeoToWorldPosition(directionLocation.StartCoordinate);
            directionLocation.StartPosition = startPosition;
            // Debug.Log(startPosition);
            var endPosition = map.GeoToWorldPosition(directionLocation.EndCoordinate);
            directionLocation.EndPosition = endPosition;
            // Debug.Log(endPosition);
		}
        // Debug.Log(DirectionLocation.List.Count);
    }
		
	
	IEnumerator UpdateGameObjects()
	{
        Debug.Log("UpdateGameObjects is starting now");
        ready = false;
        // data to match
        var deadNodes = (from node in Data.Data.Select.Node()
                         where node.Alive == false && node.MapGUID == Instance.ActiveMap.GUID
                         select node).ToList();
        if(deadNodes == null)
		{
            Debug.LogWarning("deadNodes is found or returned null at map_directions host");
            ready = true;
		    yield return new WaitForSeconds(0f);
		}
        var toAdd = new List<DirectionLocation>();
        var toRemove = new List<DirectionLocation>();

        var dispatch = (from d in Data.Data.Select.Dispatch()
                        where d.MapGUID == Instance.ActiveMap.GUID
                        select d).FirstOrDefault();
        if(dispatch == null)
		{
            Debug.LogWarning("dispatch is found or returned null at map_directions host");
            ready = true;
		    yield return new WaitForSeconds(0f);
		}
        // add stuff
        var instantiatedDispatches = new List<GameObject>();
        var count = transform.childCount;
        var instantiatedGUIDs = new List<string>();
        if(transform.childCount > 0)
		{
            foreach(int i in Enumerable.Range(0 , transform.childCount))
	        {
                instantiatedDispatches.Add(transform.GetChild(i).gameObject);
		    }
	    }    
        foreach(var id in instantiatedDispatches)
		{
            instantiatedGUIDs.Add(id.GetComponent<Identifier>().GUID);
            // Debug.Log(instantiatedGUIDs);
		}
        foreach(var node in deadNodes)
		{
            if (instantiatedGUIDs.Contains(node.GUID))
			{
                continue;
			}
            

            Vector3 startWaypoint = Vector3.zero;
            try
			{

                startWaypoint = DispatchHost.transform.GetChild(0).position;
			}
            catch
			{
                // Debug.Log("failure at startwaypoint assignment");
			}
            if(startWaypoint == Vector3.zero)
            {
                // Debug.Log("startWaypoint is null");
                continue;
            }
            var nodeObjects = new List<GameObject>();
            Vector3 endWaypoint = Vector3.zero;
            if(NodeHost.transform.childCount >= 1)
			{
                foreach(int i in Enumerable.Range(0 , NodeHost.transform.childCount))
			    {
                    var child = NodeHost.transform.GetChild(i);
                    if(child.gameObject.GetComponent<Identifier>().GUID == node.GUID)
				    {
                        endWaypoint = child.position;
				    }
			    }
			}
            if(endWaypoint == Vector3.zero)
            {
                // Debug.Log("endWaypoint is null");
                continue;
            }
            var startdispatch = (from d in Data.Data.Select.Dispatch()
                            where d.MapGUID == Instance.ActiveMap.GUID
                            select d).FirstOrDefault();
            if(startdispatch == null)
			{
                // Debug.Log("startdispatch is null");
                ready = true;
		        yield return new WaitForSeconds(0f);
			}
            var startEntity = (from e in Data.Data.Select.Entity()
                               where e.ChildGUID == startdispatch.GUID
                               select e).FirstOrDefault();
            if(startEntity == null)
			{
                // Debug.Log("startEntity is null");
                ready = true;
		        yield return new WaitForSeconds(0f);
			}
            var startLocation = (from l in Data.Data.Select.Location()
                                 where l.ChildGUID == startEntity.GUID
                                 select l).FirstOrDefault();
            if(startLocation == null)
			{
                // Debug.Log("startLocation is null");
                ready = true;
		        yield return new WaitForSeconds(0f);
			}
            Vector2d startCoordinate = new Vector2d(startLocation.Latitude, startLocation.Longitude);
            // Debug.Log("start coord: " + startCoordinate);
                               // where l.ChildGUID == (from e in Data.Data.Select.Entity()
                                  //                   where e.GUID == node.GUID
                                    //                 select e.LocationGUID).FirstOrDefault()
                               //select l).FirstOrDefault();

            var endNode = (from n in Data.Data.Select.Node()
                            where n.GUID == node.GUID
                            select n).FirstOrDefault();
            if(endNode == null)
			{
                // Debug.Log("endNode is null");
                ready = true;
		        yield return new WaitForSeconds(0f);
			}
            var endEntity = (from e in Data.Data.Select.Entity()
                               where e.ChildGUID == endNode.GUID
                               select e).FirstOrDefault();
            if(endEntity == null)
			{
                // Debug.Log("endEntity is null");
                ready = true;
		        yield return new WaitForSeconds(0f);
			}
            var endLocation = (from l in Data.Data.Select.Location()
                                 where l.ChildGUID == endEntity.GUID
                                 select l).FirstOrDefault();
            if(endLocation == null)
			{
                // Debug.Log("endLocation is null");
                ready = true;
		        yield return new WaitForSeconds(0f);
			}
            Vector2d endCoordinate = new Vector2d(endLocation.Latitude, endLocation.Longitude);
            // Debug.Log("end coord: " + endCoordinate);



            // this block is hard to undo, other elements should be checked thoroughly before executing the below statements
            var dirloc = Instantiate(DirectionsEntity);
            dirloc.GetComponent<Identifier>().GUID = node.GUID;
			dirloc.transform.parent = this.transform;
            var v = (from location in Data.Data.Select.Location()
                     where location.ChildGUID == (from entity in Data.Data.Select.Entity()
                                                  where entity.ChildGUID == node.GUID
                                                  select entity.GUID).FirstOrDefault()
                     select new Vector2d(location.Latitude, location.Longitude)).FirstOrDefault();

            if(v == null)
			{
                // Debug.Log("v is null");
                ready = true;
		        yield return new WaitForSeconds(0f);
			}

            var sObject = dirloc.transform.GetChild(0).gameObject;
            var eObject = dirloc.transform.GetChild(1).gameObject;
            sObject.transform.position = startWaypoint;
            eObject.transform.position = endWaypoint;

			toAdd.Add(new DirectionLocation(startWaypoint, endWaypoint, dirloc, sObject, eObject, node.GUID, startCoordinate, endCoordinate));
		}

        // remove stuff
        foreach(var l in DirectionLocation.List)
		{
			if((from n in deadNodes select n.GUID).ToList().Contains(l.dLGameObject.GetComponent<Identifier>().GUID)) 
                continue;
            toRemove.Add(l);
		}

        foreach(var l in toAdd)
		{
            // Debug.Log("adding " + l.dLGameObject.name + " to DirectionLocation.List");
            DirectionLocation.List.Add(l);
		}
        foreach(var l in toRemove)
		{
            DirectionLocation.Remove(l);
            // 6667Debug.Log("removing l from dl.list");
		}
        ready = true;
        yield return wait;
	}
}
