using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Menu_DisplaySelectedLocationGUID : MonoBehaviour
{
    TMPro.TMP_Text input;

	private void Awake()
	{
		input = GetComponent<TMPro.TMP_Text>();
	}
	private void OnEnable()
	{
		input.text = (from l in Data.Data.Select.Location()
							 where l.GUID == Instance.SelectedLocationGUID
							 select l.Address).FirstOrDefault();
	}
}
