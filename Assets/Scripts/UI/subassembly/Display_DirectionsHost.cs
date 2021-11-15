using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Display_DirectionsHost : MonoBehaviour
{
    class Panel
	{
		public GameObject PanelGameObject;
		public string GUID;
		public Panel(GameObject panelGameObject, string guid)
		{
			PanelGameObject = panelGameObject;
			GUID = guid;
		}
		public static void Remove(string guid)
		{
			var found = (from p in PanelList where p.GUID == guid select p).FirstOrDefault();
			if(found == null) return;
			found.PanelGameObject.Destroy();
			PanelList.Remove(found);

		}
	}
	[SerializeField] GameObject panelPrefab;
	private static List<Panel> PanelList = new List<Panel>();
	
	[SerializeField] GameObject canvas;
	RectTransform canvasRectTransform;

	private void Awake()
	{
		canvasRectTransform = canvas.GetComponent<RectTransform>();
	}

	bool ready = true;
	private void Update()
	{
		if(ready) StartCoroutine(Process());
	}
	IEnumerator Process()
	{
		ready = false;
		yield return new WaitForSeconds(1);

		var panelsToAdd = new List<Panel>();
		var panelsToRemove = new List<Panel>();
		foreach(var p in PanelList.ToList())
		{
			if(p.PanelGameObject == null)
			{
				continue;
			}
			if(!p.PanelGameObject.activeInHierarchy)
			{
				p.PanelGameObject.SetActive(true);
			}
		}

		foreach(var directionLocation in Map_DirectionsHost.DirectionLocation.List.ToList())
		{
			var listofGUIDs = (from directionPanel in PanelList select directionPanel.GUID).ToList();
			if(listofGUIDs == null) continue;
			if(listofGUIDs.Contains(directionLocation.NodeGUID))
			{
				// update position
				Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, directionLocation.EndPosition);
				var panel = (from p in PanelList 
				 where p.GUID == directionLocation.NodeGUID 
				 select p.PanelGameObject).FirstOrDefault();
				if(panel == null)
				{
					continue;
				}
				// Debug.Log("transforming position of node " + directionLocation.NodeGUID);
				panel.GetComponent<RectTransform>().anchoredPosition = screenPoint - (canvasRectTransform.sizeDelta / 2f);

			}
			else
			{
				var dirhost = GameObject.Find("DirectionsHost");
				var count = dirhost.transform.childCount;
				float? routeTime = null;
				foreach(var i in Enumerable.Range(0, count))
				{
					var obj = dirhost.transform.GetChild(i).gameObject;
					if(obj.GetComponent<Identifier>().GUID == directionLocation.NodeGUID)
					{
						routeTime = obj.GetComponent<DisplayRouteTime>().RouteTime;
					}
				}
				if(routeTime == null)
				{
					continue;
				}
				
				var p = Instantiate(panelPrefab);
				listofGUIDs.Add(directionLocation.NodeGUID);
				p.transform.SetParent(this.transform);
				panelsToAdd.Add(new Panel(p, directionLocation.NodeGUID));
				p.SetActive(false);
				p.GetComponent<Identifier>().GUID = directionLocation.NodeGUID;
				p.GetComponent<DisplayRouteTime>().RouteTime = routeTime;
			}
		}
		foreach(var p in PanelList.ToList())
		{
			var guidList = (from loc in Map_NodeHost.NodeLocation.List select loc.guid);
			if(guidList == null) continue;
			if(!guidList.Contains(p.GUID))
			{
				p.PanelGameObject.Destroy();
				panelsToRemove.Add(p);
			}
		}



		// cleanup, get panels and check if they are in Map_LocationHost.Location.List
		foreach(var item in panelsToAdd)
		{
			PanelList.Add(item);
		}
		foreach(var item in panelsToRemove)
		{
			Panel.Remove(item.GUID);
		}


		ready = true;
		
	}
}
