using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Display_UpdateDirectionNameOnEnable : MonoBehaviour
{
    [SerializeField] GameObject directionNameGameObject;
	TMPro.TMP_Text directionName;
	[SerializeField] GameObject travelNameGameObject;
	TMPro.TMP_Text travelTime;
	[SerializeField] GameObject identifierTarget;

	private void Awake()
	{
		directionName = directionNameGameObject.GetComponent<TMPro.TMP_Text>();
		directionName.text = "Name: parsing...";
	}
	private void OnEnable()
	{
		var guid = identifierTarget.GetComponent<Identifier>().GUID;
		var node = (from n in Data.Data.Select.Node()
						 where n.GUID == guid
						 select n).FirstOrDefault();
		if(node == null)
		{ 
			// Debug.Log("no node returned by LINQ at display_updatenodenameonenable"); 
			return; 
		}
		directionName.text = "Offline: " + node.Name;
		travelTime.text = "Est. Travel: " + identifierTarget.GetComponent<DisplayRouteTime>().RouteTime;

	}
}
