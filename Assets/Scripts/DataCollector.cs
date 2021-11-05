using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class DataCollector : MonoBehaviour
{
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
        if(ready) StartCoroutine(Collect());
    }
		
	
	IEnumerator Collect()
	{
        
        ready = false;
        // do sql stuff
        Instance.UnassignedLocations = (from l in Data.Data.Select.Location()
                                        where l.ChildGUID == "unassigned" && l.MapGUID == Instance.ActiveMap.GUID
                                        select l).ToList();
        Instance.Nodes = (from n in Data.Data.Select.Node()
                                        where n.MapGUID == Instance.ActiveMap.GUID
                                        select n).ToList();
        Instance.Clusters = (from c in Data.Data.Select.Cluster()
                                        where c.MapGUID == Instance.ActiveMap.GUID
                                        select c).ToList();
        Instance.Dispatches = (from d in Data.Data.Select.Dispatch()
                                        where d.MapGUID == Instance.ActiveMap.GUID
                                        select d).ToList();
        // Debug.Log((from u in Instance.UnassignedLocations select u.GUID).ToList().Count);
        yield return wait;
        ready = true;
	}

}
