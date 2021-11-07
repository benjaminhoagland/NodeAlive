using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Mapbox.Unity.Map;
using Mapbox.Utils;

	using Mapbox.Unity.Utilities;

public class Map_NodeHost : MonoBehaviour
{
	public class NodeLocation
	{
        public static List<NodeLocation> List = new List<NodeLocation>();
        public GameObject gameObject { get; set; }
        public string guid { get; set; }
        public Vector2d coordinate { get; set; }
        public NodeLocation(GameObject o, string g, Vector2d c)
		{
            gameObject = o;
            guid = g;
            coordinate = c;
		}
        public static void Remove(NodeLocation location)
		{
            List.Remove(location);
            location.gameObject.Destroy();
		}
	}
    [SerializeField]GameObject NodeEntity;

	Coroutine c;

	[SerializeField] float interval = 1f;

	bool ready = true;

    WaitForSeconds wait;

    AbstractMap map;

    void Awake()
    {
        wait = new WaitForSeconds(interval);
        map = FindObjectOfType<AbstractMap>();
        NodeLocation.List = new List<NodeLocation>();
    }
    
    void Update()
    {
        if(ready) StartCoroutine(UpdateGameObjects());
        foreach(var location in NodeLocation.List)
		{
            var pos = map.GeoToWorldPosition(location.coordinate);


            location.gameObject.transform.position = pos;
		}
    }
		
	
	IEnumerator UpdateGameObjects()
	{
        ready = false;
        // data to match
        var nodes = (from node in Instance.Nodes 
                     select node).ToList();
        // add stuff
        var guids = (from loc in NodeLocation.List select loc.guid).ToList();
        foreach(var node in nodes)
		{
            if (guids.Contains(node.GUID))
			{
                continue;
			}
            var l = Instantiate(NodeEntity);
            l.GetComponent<Identifier>().GUID = node.GUID;
			l.transform.parent = this.transform;
            var v = (from location in Data.Data.Select.Location()
                     where location.ChildGUID == (from entity in Data.Data.Select.Entity()
                                                  where entity.ChildGUID == node.GUID
                                                  select entity.GUID).FirstOrDefault()
                     select new Vector2d(location.Latitude, location.Longitude)).FirstOrDefault();
			NodeLocation.List.Add(new NodeLocation(l, node.GUID, v));
		}

        // remove stuff
        foreach(var l in NodeLocation.List)
		{
			if((from n in nodes select n.GUID).ToList().Contains(l.gameObject.GetComponent<Identifier>().GUID)) 
                continue;
            NodeLocation.Remove(l);
		}
        yield return wait;
        ready = true;
	}
}
