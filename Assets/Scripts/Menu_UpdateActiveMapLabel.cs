using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_UpdateActiveMapLabel : MonoBehaviour
{
    TMPro.TMP_Text text;
	private void Awake()
	{
		text = gameObject.GetComponent<TMPro.TMP_Text>();
	}
	private void Update()
	{
		text.text = Instance.ActiveMap.Name;
	}
}
