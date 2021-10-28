// repurposed from Mapbox Examples
using Mapbox.Geocoding;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using UnityEngine;
using System;
using System.Collections;
using TMPro;

public class Menu_UpdateMapLocation : MonoBehaviour
{
	Camera _camera;
	Vector3 _cameraStartPos;
	AbstractMap _map;

	[SerializeField] Menu_ForwardGeocodeResponse _forwardGeocoder;

	[SerializeField] Slider _zoomSlider;
	[SerializeField] TMP_InputField zoom;

	[SerializeField] TMP_InputField latitude;
	[SerializeField] TMP_InputField longitude;

	Coroutine _reloadRoutine;

	WaitForSeconds _wait;

	void Awake()
	{
		_camera = Camera.main;
		_cameraStartPos = _camera.transform.position;
		_map = FindObjectOfType<AbstractMap>();
		if(_map == null)
		{
			Debug.LogError("Error: No Abstract Map component found in scene.");
			return;
		}
		if (latitude != null)
		{
			latitude.onEndEdit.AddListener(SetLatitude);
		}
		if (zoom != null)
		{
			zoom.onEndEdit.AddListener(SetZoom);
		}
		if (longitude != null)
		{
			longitude.onEndEdit.AddListener(SetLongitude);
		}
		if (_zoomSlider != null)
		{
			_map.OnUpdated += () => { _zoomSlider.value = _map.Zoom; zoom.text = _map.Zoom.ToString(); };
			_zoomSlider.onValueChanged.AddListener(Reload);
		}
		if(_forwardGeocoder != null)
		{
			_forwardGeocoder.OnGeocoderResponse += ForwardGeocoder_OnGeocoderResponse;
		}
		_wait = new WaitForSeconds(.1f);
	}
	void SetZoom(string zoomString)
	{
		if(!string.IsNullOrEmpty(zoomString))
		{
			var toZoom = _map.Zoom;
			float.TryParse(zoomString, out toZoom);
			_map.UpdateMap(_map.CenterLatitudeLongitude, toZoom);
			_zoomSlider.value = toZoom;
		}
	}
	void SetLatitude(string latString)
	{
		if(!string.IsNullOrEmpty(latString))
		{
			var latlon = _map.CenterLatitudeLongitude;
			double.TryParse(latString, out latlon.x);
			_map.SetCenterLatitudeLongitude(latlon);
			_map.UpdateMap(_map.CenterLatitudeLongitude);
		}
	}
	void SetLongitude(string lonString)
	{
		if(!string.IsNullOrEmpty(lonString))
		{
			var latlon = _map.CenterLatitudeLongitude;
			double.TryParse(lonString, out latlon.y);
			_map.SetCenterLatitudeLongitude(latlon);
			_map.UpdateMap(_map.CenterLatitudeLongitude);
		}
	}
	void ForwardGeocoder_OnGeocoderResponse(ForwardGeocodeResponse response)
	{
		if (null != response.Features && response.Features.Count > 0)
		{
			int zoom = _map.AbsoluteZoom;
			_map.UpdateMap(response.Features[0].Center, zoom);
		}
	}

	void ForwardGeocoder_OnGeocoderResponse(ForwardGeocodeResponse response, bool resetCamera)
	{
		if (response == null)
		{
			return;
		}
		if (resetCamera)
		{
			_camera.transform.position = _cameraStartPos;
		}
		ForwardGeocoder_OnGeocoderResponse(response);
	}

	void Reload(float value)
	{
		if (_reloadRoutine != null)
		{
			StopCoroutine(_reloadRoutine);
			_reloadRoutine = null;
		}
		_reloadRoutine = StartCoroutine(ReloadAfterDelay((int)value));
	}

	IEnumerator ReloadAfterDelay(int zoom)
	{
		yield return _wait;
		_camera.transform.position = _cameraStartPos;
		_map.UpdateMap(_map.CenterLatitudeLongitude, zoom);
		_reloadRoutine = null;
	}
}