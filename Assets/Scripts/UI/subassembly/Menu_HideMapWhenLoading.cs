// repurposed from Mapbox Examples
using Mapbox.Geocoding;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Menu_HideMapWhenLoading : MonoBehaviour
{
	// Start is called before the first frame update
	AbstractMap _map;
	[SerializeField]GameObject target;

	[SerializeField]
	TMP_Text _text;

	[SerializeField]
	AnimationCurve _curve;
	private void Awake()
	{
		_map = FindObjectOfType<AbstractMap>();
		if(_map == null)
		{
			Debug.LogError("Error: No Abstract Map component found in scene.");
			return;
		}
		_map.OnInitialized += _map_OnInitialized;

	}
	void _map_OnInitialized()
	{
		_map.MapVisualizer.OnMapVisualizerStateChanged += (s) =>
		{

			if (this == null)
				return;

			if (s == ModuleState.Finished)
			{
				target.SetActive(false);
			}
			else if (s == ModuleState.Working)
			{

				// Uncommment me if you want the loading screen to show again
				// when loading new tiles.
				target.SetActive(true);
			}

		};
	}
	void Update()
	{
		var t = _curve.Evaluate(Time.time);
		_text.color = Color.Lerp(Color.clear, Color.white, t);
	}
}
