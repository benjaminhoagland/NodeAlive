using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

public class NewBehaviourScript_test1 : MonoBehaviour
{
	private void Awake()
	{
		var locations = from l in Data.Select.Location() select l;
		foreach(var location in locations)
		{
			Debug.Log(location.ToString());
		}
	}
}
