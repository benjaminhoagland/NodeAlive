using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Mapbox.Unity.Map;
using Mapbox.Utils;

	using Mapbox.Unity.Utilities;

public class Map_ClusterHost : MonoBehaviour
{
	private class ClusterLocation
	{
        public static List<ClusterLocation> List = new List<ClusterLocation>();
        public GameObject gameObject { get; set; }
        public string guid { get; set; }
        public Vector2d coordinate { get; set; }
        public ClusterLocation(GameObject o, string g, Vector2d c)
		{
            gameObject = o;
            guid = g;
            coordinate = c;
		}
        public static void Remove(ClusterLocation location)
		{
            List.Remove(location);
            location.gameObject.Destroy();
		}
	}
    [SerializeField]GameObject ClusterEntity;

	Coroutine c;

	[SerializeField] float interval = 1f;

	bool ready = true;

    WaitForSeconds wait;

    AbstractMap map;

    void Awake()
    {
        wait = new WaitForSeconds(interval);
        map = FindObjectOfType<AbstractMap>();
        ClusterLocation.List = new List<ClusterLocation>();
    }
    
    void Update()
    {
        if(ready) StartCoroutine(UpdateGameObjects());
        foreach(var location in ClusterLocation.List)
		{
            var pos = map.GeoToWorldPosition(location.coordinate);


            location.gameObject.transform.position = pos;
		}
    }
		
	
	IEnumerator UpdateGameObjects()
	{
        ready = false;
        // data to match
        

        // add stuff
        var guids = (from loc in ClusterLocation.List select loc.guid).ToList();
        foreach(var cluster in Instance.Clusters)
        { 
            if (guids.Contains(cluster.GUID))
			{
                continue;
			}
            var l = Instantiate(ClusterEntity);
            l.GetComponent<Identifier>().GUID = cluster.GUID;
			l.transform.parent = this.transform;
            var v = (from location in Data.Data.Select.Location()
                     where location.ChildGUID == (from entity in Data.Data.Select.Entity()
                                                  where entity.ChildGUID == cluster.GUID
                                                  select entity.GUID).FirstOrDefault()
                     select new Vector2d(location.Latitude, location.Longitude)).FirstOrDefault();
			ClusterLocation.List.Add(new ClusterLocation(l, cluster.GUID, v));
		}

        // remove stuff
        foreach(var l in ClusterLocation.List)
		{
			if((from c in Instance.Clusters select c.GUID).Contains(l.gameObject.GetComponent<Identifier>().GUID)) 
                continue;
            ClusterLocation.Remove(l);
		}
        yield return wait;
        ready = true;
	}
}
