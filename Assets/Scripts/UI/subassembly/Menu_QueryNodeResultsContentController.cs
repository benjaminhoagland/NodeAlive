using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mapbox.Geocoding;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using System;
using TMPro;

public class Menu_QueryNodeResultsContentController : MonoBehaviour
{
    [SerializeField]GameObject Content;
    [SerializeField]GameObject ClickableListItem;

	RectTransform rt;
	float heightIncrement = 32.0f;
	private void Awake()
	{
		rt = Content.GetComponent<RectTransform>();
		
	}
	private void OnDisable()
	{
		Instance.EnableNodeUI();
	}
	private void OnEnable()
	{
		Instance.DisableNodeUI();
		rt.sizeDelta = new Vector2(0, heightIncrement);
		var results = Data.Data.Select.Result();
		int counter = 0;
		foreach(var result in results)
		{
			var mapItem = Instantiate(ClickableListItem, Content.transform);
			var clickableListItem = mapItem.transform.GetChild(0);
			var itemsToSet = new List<string>()
			{
				(from n in Data.Data.Select.Node()
				 where n.GUID == result.NodeGUID
				 select n.Name).FirstOrDefault(), result.Status == true ? "Dead" : "Alive", result.DateFinishedExecution.ToString(Data.Data.timeformat), result.Contents
			};
			foreach(var item in Enumerable.Range(0,4))
			{
				var text = clickableListItem.GetChild(item).GetChild(0).GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>();
				text.text = itemsToSet[item];
				// Debug.Log(text);
			}
			// set the y offset of the mapitem
			var pos = mapItem.transform.position;
			mapItem.transform.position = new Vector3(pos.x, pos.y + -heightIncrement * counter);
			counter++;
		}
		rt.sizeDelta = new Vector2(0, heightIncrement * counter);
	}
	
}

