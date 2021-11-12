using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;

public class DataCollector : MonoBehaviour
{
	// this class collects information and populates it into the "instance" which is easily readable by the rest of the applicaiton via "Instance.member" style access
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
        foreach(var node in Instance.Nodes)
		{
            if((DateTime.Now - node.LastResponse).TotalSeconds > node.Timeout) Data.Data.Update.NodeOverdue(node.GUID);
		}
        Instance.Nodes = (from n in Data.Data.Select.Node()
                                        where n.MapGUID == Instance.ActiveMap.GUID
                                        select n).ToList();
        var offlineNodes = (from n in Instance.Nodes
                            where n.Alive == false
                            select n).ToList();
        if(offlineNodes.Count != 0)
        { Instance.NODEOFFLINE = true; }
        else
        { Instance.NODEOFFLINE = false; }

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
