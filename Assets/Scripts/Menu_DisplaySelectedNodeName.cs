using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_DisplaySelectedNodeName : MonoBehaviour
{
    TMPro.TMP_Text tmp_text;
	private void Awake()
	{
		tmp_text = GetComponent<TMPro.TMP_Text>();
	}

	private void OnEnable()
	{
		tmp_text.text = Instance.ActiveNode.Name;
	}
}
