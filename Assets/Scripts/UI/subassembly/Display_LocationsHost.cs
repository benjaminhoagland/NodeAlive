using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Display_LocationsHost : MonoBehaviour
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
	}
	[SerializeField] GameObject panelPrefab;
	private List<Panel> List = new List<Panel>();
	
	[SerializeField] GameObject canvas;
	RectTransform canvasRectTransform;

	private void Awake()
	{
		canvasRectTransform = canvas.GetComponent<RectTransform>();
	}


	private void Update()
	{
		var panelsToAdd = new List<Panel>();
		var panelsToRemove = new List<Panel>();
		foreach(var p in List.ToList())
		{
			if(!p.PanelGameObject.activeInHierarchy)
			{
				p.PanelGameObject.SetActive(true);
			}
		}

		foreach(var loc in Map_LocationHost.Location.List.ToList())
		{
			if((from p in List select p.GUID).ToList().Contains(loc.guid))
			{
				// update position
				Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, loc.gameObject.transform.position);
				(from p in List 
				 where p.GUID == loc.guid 
				 select p.PanelGameObject).FirstOrDefault().GetComponent<RectTransform>().anchoredPosition = screenPoint - (canvasRectTransform.sizeDelta / 2f);
			}
			else
			{
				var p = Instantiate(panelPrefab);
				p.transform.SetParent(this.transform);
				panelsToAdd.Add(new Panel(p, loc.guid));
				p.SetActive(false);
				p.GetComponent<Identifier>().GUID = loc.guid;
			}
		}
		foreach(var p in List.ToList())
		{
			if(!(from loc in Map_LocationHost.Location.List select loc.guid).Contains(p.GUID))
			{
				p.PanelGameObject.Destroy();
				panelsToRemove.Add(p);
			}
		}



		// cleanup, get panels and check if they are in Map_LocationHost.Location.List
		foreach(var item in panelsToAdd)
		{
			List.Add(item);
		}
		foreach(var item in panelsToRemove)
		{
			List.Remove(item);
		}
	}
}
