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
	private class Location
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
        foreach(var location in Location.List)
		{
            var pos = map.GeoToWorldPosition(location.coordinate);


            location.gameObject.transform.position = pos;
		}
    }
		
	
	IEnumerator UpdateGameObjects()
	{
        ready = false;
        // data to match
        var unassignedLocationGUIDs = (from u in Instance.UnassignedLocations 
                                       select u.GUID).ToList();
        // add stuff
        foreach(var g in unassignedLocationGUIDs)
		{
			////Debug.Log(g);
            var guids = (from loc in Location.List select loc.guid).ToList();
            foreach(var deb in guids)
			{
                //Debug.Log(deb);
			}
            if (guids.Contains(g))
			{
                //Debug.Log("instantiated guids contains g from unassignedlocationguids");
                continue;
			}
            //Debug.Log("should not reach here");
            var l = Instantiate(LocationEntity);
            l.GetComponent<Identifier>().GUID = g;
			l.transform.parent = this.transform;
            var v = (from u in Instance.UnassignedLocations
                            where u.GUID == g
                            select new Vector2d(u.Latitude, u.Longitude)).FirstOrDefault();
			Location.List.Add(new Location(l, g, v));
		}

        // remove stuff
        foreach(var l in Location.List)
		{
			if(unassignedLocationGUIDs.Contains(l.gameObject.GetComponent<Identifier>().GUID)) 
                continue;
            Location.Remove(l);
		}
        yield return wait;
        ready = true;
	}
}
