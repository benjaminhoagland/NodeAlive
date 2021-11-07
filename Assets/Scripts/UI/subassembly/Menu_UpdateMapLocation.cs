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
			zoom.onEndEdit.AddListener(SetZoomHere);
		}
		if (longitude != null)
		{
			longitude.onEndEdit.AddListener(SetLongitude);
		}
		if (_zoomSlider != null)
		{
			_map.OnUpdated += () => { _zoomSlider.value = _map.Zoom; zoom.text = _map.Zoom.ToString(); };
			_zoomSlider.onValueChanged.AddListener(SliderToField);
		}
		if(_forwardGeocoder != null)
		{
			_forwardGeocoder.OnGeocoderResponse += ForwardGeocoder_OnGeocoderResponse;
		}
		_wait = new WaitForSeconds(2.0f);
	}
	void SliderToField(float zoomFloat)
	{
		if(!float.IsNaN(zoomFloat))
		{
			var toZoom = _map.Zoom;
			float.TryParse(zoomFloat.ToString(), out toZoom);
			_zoomSlider.value = toZoom;
			_map.SetZoom(toZoom);
			zoom.text = zoomFloat.ToString();
			Instance.Message("Updating map...");
			Reload();
		}
	}
	void SetZoomHere(string zoomString)
	{
		if(!string.IsNullOrEmpty(zoomString))
		{
			var toZoom = _map.Zoom;
			float.TryParse(zoomString, out toZoom);
			_zoomSlider.value = toZoom;
			_map.SetZoom(toZoom);
			Reload();
		}
	}
	void SetLatitude(string latString)
	{
		if(!string.IsNullOrEmpty(latString))
		{
			var latlon = _map.CenterLatitudeLongitude;
			double.TryParse(latString, out latlon.x);
			_map.SetCenterLatitudeLongitude(latlon);
			Reload();
		}
	}
	void SetLongitude(string lonString)
	{
		if(!string.IsNullOrEmpty(lonString))
		{
			var latlon = _map.CenterLatitudeLongitude;
			double.TryParse(lonString, out latlon.y);
			_map.SetCenterLatitudeLongitude(latlon);
			Reload();
		}
	}
	void ForwardGeocoder_OnGeocoderResponse(ForwardGeocodeResponse response)
	{
		if (null != response.Features && response.Features.Count > 0)
		{
			int zoom = _map.AbsoluteZoom;
			_map.SetZoom(zoom);
			_map.SetCenterLatitudeLongitude(response.Features[0].Center);
			Reload();
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

	void Reload()
	{
		if (_reloadRoutine != null)
		{
			StopCoroutine(_reloadRoutine);
			_reloadRoutine = null;
		}
		_reloadRoutine = StartCoroutine(ReloadAfterDelay());
	}

	IEnumerator ReloadAfterDelay()
	{
		yield return _wait;
		Instance.Message("Updating map...");
		_map.UpdateMap(_map.CenterLatitudeLongitude, _map.Zoom);
		_reloadRoutine = null;
	}
}