using Mapbox.Unity;
using UnityEngine;
using UnityEngine.UI;
using System;
using Mapbox.Geocoding;
using Mapbox.Utils;
using TMPro;
using Sirenix.OdinInspector;


public class Menu_NewLocationAddressHandler : MonoBehaviour
{
	ForwardGeocodeResource forwardResource;
	ReverseGeocodeResource reverseResource;
	[SerializeField] TMP_InputField address;
	[SerializeField] TMP_InputField latitude;
	[SerializeField] TMP_InputField longitude;

	Vector2d coordinate;
	public Vector2d Coordinate
	{
		get
		{
			return coordinate;
		}
	}

	bool hasResponse;
	public bool HasResponse
	{
		get
		{
			return hasResponse;
		}
	}

	public ForwardGeocodeResponse ForwardGeocodeResponse { get; private set; }
	public ReverseGeocodeResponse ReverseGeocodeResponse { get; private set; }

	// public event Action<ForwardGeocodeResponse> OnGeocoderResponse = delegate { };

	void Awake()
	{
		address.onEndEdit.AddListener(HandleAddressInput);
		latitude.onEndEdit.AddListener(HandleLatitudeInput);
		longitude.onEndEdit.AddListener(HandleLongitudeInput);
		forwardResource = new ForwardGeocodeResource("");
		reverseResource = new ReverseGeocodeResource(new Vector2d(0,0));
	}
	void HandleAddressInput(string searchString)
	{
		hasResponse = false;
		if (!string.IsNullOrEmpty(searchString))
		{
			forwardResource.Query = searchString;
			MapboxAccess.Instance.Geocoder.Geocode(forwardResource, HandleForward);
		}
	}
	void HandleLatitudeInput(string latitudeString)
	{
		hasResponse = false;
		if (!string.IsNullOrEmpty(latitudeString))
		{
			var x = 0f;
			float.TryParse(latitudeString, out x);
			var y = 0f;
			float.TryParse(longitude.text.ToString(), out y);
			var v2 = new Vector2d(x, y);
			reverseResource.Query = v2;
			Debug.Log(v2.ToString());
			MapboxAccess.Instance.Geocoder.Geocode(reverseResource, HandleReverse);
		}
	}
	void HandleLongitudeInput(string longitudeString)
	{
		hasResponse = false;
		if (!string.IsNullOrEmpty(longitudeString))
		{
			var x = 0f;
			float.TryParse(latitude.text.ToString(), out x);
			var y = 0f;
			float.TryParse(longitudeString, out y);
			var v2 = new Vector2d(x, y);
			reverseResource.Query = v2;
			// Debug.Log(v2.ToString());
			MapboxAccess.Instance.Geocoder.Geocode(reverseResource, HandleReverse);
		}
	}
	void HandleReverse(ReverseGeocodeResponse res)
	{
		hasResponse = true;
		if (null == res)
		{
			address.text = "no geocode response";
		}
		else if (null != res.Features && res.Features.Count > 0)
		{
			// var center = res.Features[0].Center;
			// Debug.Log(Instance.DebugProperties(res.Features[0]));
			coordinate = res.Features[0].Center;
			latitude.text = res.Features[0].Center.x.ToString();
			longitude.text = res.Features[0].Center.y.ToString();
			address.text = res.Features[0].PlaceName;
		}
		ReverseGeocodeResponse = res;
		// OnGeocoderResponse(res);
	}
	void HandleForward(ForwardGeocodeResponse res)
	{
		hasResponse = true;
		if (null == res)
		{
			address.text = "no geocode response";
		}
		else if (null != res.Features && res.Features.Count > 0)
		{
			// var center = res.Features[0].Center;
			// Debug.Log(Instance.DebugProperties(res.Features[0]));
			coordinate = res.Features[0].Center;
			latitude.text = res.Features[0].Center.x.ToString();
			longitude.text = res.Features[0].Center.y.ToString();
			address.text = res.Features[0].PlaceName;
		}
		ForwardGeocodeResponse = res;
		// OnGeocoderResponse(res);
	}
}
