// repurposed from Mapbox Examples

using Mapbox.Unity;
using UnityEngine;
using UnityEngine.UI;
using System;
using Mapbox.Geocoding;
using Mapbox.Utils;
using TMPro;
using Mapbox.Unity.Map;

[RequireComponent(typeof(TMP_InputField))]
public class Menu_ForwardGeocodeResponse : MonoBehaviour
{
	TMP_InputField _TMP_inputField;

	ForwardGeocodeResource _resource;

	[SerializeField] TMP_InputField latitude;
	[SerializeField] TMP_InputField longitude;
	[SerializeField] TMP_InputField zoom;
	[SerializeField] TMP_InputField mapName;

	AbstractMap map;
	Vector2d _coordinate;
	public Vector2d Coordinate
	{
		get
		{
			return _coordinate;
		}
	}

	bool _hasResponse;
	public bool HasResponse
	{
		get
		{
			return _hasResponse;
		}
	}

	public ForwardGeocodeResponse Response { get; private set; }

	public event Action<ForwardGeocodeResponse> OnGeocoderResponse = delegate { };

	void Awake()
	{
		_TMP_inputField = GetComponent<TMPro.TMP_InputField>();
		_TMP_inputField.onEndEdit.AddListener(HandleUserInput);
		_resource = new ForwardGeocodeResource("");
		map = FindObjectOfType<AbstractMap>();
	}
	void HandleUserInput(string searchString)
	{
		_hasResponse = false;
		if (!string.IsNullOrEmpty(searchString))
		{
			_resource.Query = searchString;
			MapboxAccess.Instance.Geocoder.Geocode(_resource, HandleGeocoderResponse);
		}
		if(mapName.text.Length == 0) mapName.text = "Map of " + _TMP_inputField.text;
	}

	void HandleGeocoderResponse(ForwardGeocodeResponse res)
	{
		_hasResponse = true;
		if (null == res)
		{
			_TMP_inputField.text = "no geocode response";
		}
		else if (null != res.Features && res.Features.Count > 0)
		{
			var center = res.Features[0].Center;
			_coordinate = res.Features[0].Center;
			latitude.text = res.Features[0].Center.x.ToString();
			longitude.text = res.Features[0].Center.y.ToString();
			zoom.text = map.Zoom.ToString();
		}
		Response = res;
		OnGeocoderResponse(res);
	}
}
