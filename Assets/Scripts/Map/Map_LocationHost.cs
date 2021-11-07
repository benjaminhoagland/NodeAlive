using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Mapbox.Unity.Map;
using Mapbox.Utils;

	using Mapbox.Unity.Utilities;

public class Map_LocationHost : MonoBehaviour
{
	public class Location
	{
        public static List<Location> List = new List<Location>();
        public GameObject gameObject { get; set; }
        public string guid { get; set; }
        public Vector2d coordinate { get; set; }
        public Location(GameObject o, string g, Vector2d c)
		{
            gameObject = o;
            guid = g;
            coordinate = c;
		}
        public static void Remove(Location location)
		{
            List.Remove(location);
            location.gameObject.Destroy();
		}
	}
    [SerializeField]GameObject LocationEntity;

	Coroutine c;

	[SerializeField] float interval = 1f;

	bool ready = true;

    WaitForSeconds wait;

    AbstractMap map;

    void Awake()
    {
        wait = new WaitForSeconds(interval);
        map = FindObjectOfType<AbstractMap>();
        Location.List = new List<Location>();
    }
    
    void Update()
    {
        if(ready) StartCoroutine(UpdateGameObjects());
        foreach(var location in Location.List.ToList())
		{
            var pos = map.GeoToWorldPosition(location.coordinate);



            location.gameObject.transform.position = pos;
		}
    }
		
	
	IEnumerator UpdateGameObjects()
	{
        ready = false;
        var locationsToAdd = new List<Location>();
        var locationsToRemove = new List<Location>();
        // data to match
        var unassignedLocationGUIDs = (from u in Instance.UnassignedLocations 
                                       select u.GUID).ToList();
        // add stuff
        foreach(var g in unassignedLocationGUIDs.ToList())
		{
            var guids = (from loc in Location.List select loc.guid).ToList();
            if (guids.Contains(g))
			{
                continue;
			}
            var l = Instantiate(LocationEntity);
            l.GetComponent<Identifier>().GUID = g;
			l.transform.parent = this.transform;
            var v = (from u in Instance.UnassignedLocations
                            where u.GUID == g
                            select new Vector2d(u.Latitude, u.Longitude)).FirstOrDefault();
			locationsToAdd.Add(new Location(l, g, v));
		}

        // remove stuff
        foreach(var l in Location.List.ToList())
		{
			if(unassignedLocationGUIDs.Contains(l.gameObject.GetComponent<Identifier>().GUID)) 
                continue;
            locationsToRemove.Add(l);
		}

        foreach(var item in locationsToAdd)
		{
			Location.List.Add(item);
		}
		foreach(var item in locationsToRemove)
		{
			Location.Remove(item);
		}
        yield return wait;
        ready = true;
	}
}
