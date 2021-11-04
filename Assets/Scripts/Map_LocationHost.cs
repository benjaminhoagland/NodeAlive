using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
public class Map_LocationHost : MonoBehaviour
{
	[SerializeField]GameObject LocationEntity;
	List<GameObject> locations = new List<GameObject>();
    List<string> guids = new List<string>();

	Coroutine c;

	[SerializeField] float interval = 1f;

	bool ready = true;

    WaitForSeconds wait;

    void Awake()
    {
        wait = new WaitForSeconds(interval);
    }
    
    void Update()
    {
        if(ready) StartCoroutine(UpdateGameObjects());
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
			if (guids.Contains(g))
                continue;
            var l = Instantiate(LocationEntity);
			l.transform.parent = this.transform;
			locations.Add(l);
            l.GetComponent<Identifier>().GUID = g;
            guids.Add(g);
		}

        // remove stuff
        foreach(var l in locations)
		{
            if(unassignedLocationGUIDs.Contains(l.GetComponent<Identifier>().GUID));
            
            locations.Remove(l);
            guids.Remove(l.GetComponent<Identifier>().GUID);
		}
        yield return wait;
        ready = true;
	}
}
