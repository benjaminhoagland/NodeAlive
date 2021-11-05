using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Mapbox.Unity.Map;
using Mapbox.Utils;

	using Mapbox.Unity.Utilities;

public class Map_DispatchHost : MonoBehaviour
{
	private class DispatchLocation
	{
        public static List<DispatchLocation> List = new List<DispatchLocation>();
        public GameObject gameObject { get; set; }
        public string guid { get; set; }
        public Vector2d coordinate { get; set; }
        public DispatchLocation(GameObject o, string g, Vector2d c)
		{
            gameObject = o;
            guid = g;
            coordinate = c;
		}
        public static void Remove(DispatchLocation location)
		{
            List.Remove(location);
            location.gameObject.Destroy();
		}
	}
    [SerializeField]GameObject DispatchEntity;

	Coroutine c;

	[SerializeField] float interval = 1f;

	bool ready = true;

    WaitForSeconds wait;

    AbstractMap map;

    void Awake()
    {
        wait = new WaitForSeconds(interval);
        map = FindObjectOfType<AbstractMap>();
        DispatchLocation.List = new List<DispatchLocation>();
    }
    
    void Update()
    {
        if(ready) StartCoroutine(UpdateGameObjects());
        foreach(var location in DispatchLocation.List)
		{
            var pos = map.GeoToWorldPosition(location.coordinate);


            location.gameObject.transform.position = pos;
		}
    }
		
	
	IEnumerator UpdateGameObjects()
	{
        ready = false;
        // data to match
        var dispatches = (from d in Instance.Dispatches
                     select d).ToList();
        // add stuff
        var guids = (from loc in DispatchLocation.List select loc.guid).ToList();
        foreach(var disp in dispatches)
		{
            if (guids.Contains(disp.GUID))
			{
                continue;
			}
            var l = Instantiate(DispatchEntity);
            l.GetComponent<Identifier>().GUID = disp.GUID;
			l.transform.parent = this.transform;
            var v = (from i in Data.Data.Select.Location()
                     where i.ChildGUID == disp.GUID
                     select new Vector2d(i.Latitude, i.Longitude)).FirstOrDefault();
			DispatchLocation.List.Add(new DispatchLocation(l, disp.GUID, v));
		}

        // remove stuff
        foreach(var l in DispatchLocation.List)
		{
			if((from d in dispatches select d.GUID).ToList().Contains(l.gameObject.GetComponent<Identifier>().GUID)) 
                continue;
            DispatchLocation.Remove(l);
		}
        yield return wait;
        ready = true;
	}
}
