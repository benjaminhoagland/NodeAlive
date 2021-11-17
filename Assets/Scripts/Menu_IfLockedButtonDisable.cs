using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Menu_IfLockedButtonDisable : MonoBehaviour
{
    Button button;
	TMPro.TMP_Text text;
	private void Awake()
	{
		button = GetComponent<Button>();
		try
		{
		text = button.transform.GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>();
		}
		catch
		{
			// some buttons have no text component and will cause errors
			// it doens't matter for now, because we only want to capture 
			// text components where they exist
		}
	}
	private void Update()
	{
		button.enabled = Instance.AdminLocked ? false : true;
		try
		{
			text.color = Instance.AdminLocked ? Color.gray : Color.white;
		}
		catch
		{
			// some buttons have no text component and will cause errors
			// it doens't matter for now, because we only want to capture 
			// text components where they exist
		}
	}
}
