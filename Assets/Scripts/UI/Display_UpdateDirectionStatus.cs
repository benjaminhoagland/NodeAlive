using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Linq;
using Utility;
public class Display_UpdateDirectionStatus : SerializedMonoBehaviour
{
    [SerializeField] public TMPro.TMP_Text travelText;
	[SerializeField] public GameObject identifierTarget;
	private bool ready = true;
	private void Update()
	{
		if(ready) StartCoroutine(Process());
	}

	IEnumerator Process()
	{
		ready = false;
		var dirhost = GameObject.Find("DirectionsHost");
		var count = dirhost.transform.childCount;
		float? timeInSeconds = null;
		try
		{

		foreach(var i in Enumerable.Range(0, count))
		{
			var obj = dirhost.transform.GetChild(i).gameObject;
			if(obj.GetComponent<Identifier>().GUID == identifierTarget.GetComponent<Identifier>().GUID)
			{
				timeInSeconds = obj.GetComponent<DisplayRouteTime>().RouteTime;
			}
		}
		var roundedTimeInSeconds = Mathf.RoundToInt((float)timeInSeconds);
		// Debug.Log(roundedTimeInSeconds);
		var timeInMinutes = roundedTimeInSeconds / 60;

		travelText.text = "Est. Travel: " + timeInMinutes + "M";
		}
		catch
		{

		}

		if(timeInSeconds == null)
		{
			yield return new WaitForSeconds(0);
			ready = true;
		}
		yield return new WaitForSeconds(1);
		ready = true;
	}
}
