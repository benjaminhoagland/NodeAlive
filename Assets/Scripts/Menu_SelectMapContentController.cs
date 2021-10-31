using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Menu_SelectMapContentController : MonoBehaviour
{
    [SerializeField]GameObject Content;
    [SerializeField]GameObject ClickableListItem;

	RectTransform rt;
	float heightIncrement = 32.0f;
	private void Awake()
	{
		rt = Content.GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		rt.sizeDelta = new Vector2(0, heightIncrement);





		var maps = Data.SelectMaps();
		int counter = 0;
		foreach(var map in maps)
		{
			var mapItem = Instantiate(ClickableListItem, Content.transform);
			var clickableListItem = mapItem.transform.GetChild(0);
			var itemsToSet = new List<string>()
			{
				map.Name, map.Location, map.DateCreated.ToString(Data.timeformat), map.Guid
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

