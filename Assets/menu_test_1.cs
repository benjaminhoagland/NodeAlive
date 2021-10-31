using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_test_1 : MonoBehaviour
{
	private void Awake()
	{
		var r = gameObject.GetComponent<RectTransform>();
		r.sizeDelta = new Vector2(0, 30);
	}
}
