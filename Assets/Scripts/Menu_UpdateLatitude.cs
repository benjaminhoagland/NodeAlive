using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mapbox.Unity.Map;
public class Menu_UpdateLatitude : MonoBehaviour
{
	// Start is called before the first frame update
	TMP_InputField _TMP_inputField;
	AbstractMap _map;
	[SerializeField]TMP_InputField tmpi;
	private void Awake()
	{
		_TMP_inputField = GetComponent<TMPro.TMP_InputField>();
		_TMP_inputField.onEndEdit.AddListener(HandleUserInput);
		_map = FindObjectOfType<AbstractMap>();
	}
	void HandleUserInput(string latString)
	{
		if(!string.IsNullOrEmpty(latString))
		{
			var latlon = _map.CenterLatitudeLongitude;
			double.TryParse(latString, out latlon.x);
			_map.SetCenterLatitudeLongitude(latlon);
		}
	}
}
