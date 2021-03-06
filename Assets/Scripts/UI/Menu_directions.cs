using UnityEngine;
using Mapbox.Directions;
using System.Collections.Generic;
using System.Linq;
using Mapbox.Unity.Map;
using Data;
using Mapbox.Utils;
using Mapbox.Unity.Utilities;
using System.Collections;
 using Mapbox;


namespace Mapbox.Unity.MeshGeneration.Factories
{

	class Menu_directions : MonoBehaviour
	{
		// modified from mapbox "Traffic and Directions"
		[SerializeField]
		AbstractMap _map;

		// [SerializeField]
		// MeshModifier[] MeshModifiers;
		// [SerializeField]
		// Material _material;
		// [SerializeField] GameObject cube;

		[SerializeField]
		Transform[] _waypoints;
		private List<Vector3> _cachedWaypoints;
		private Vector3[] _cachedPositions = new Vector3[0];

		[SerializeField]
		[Range(1,10)]
		private float UpdateFrequency = 2;

		private LineRenderer lr;

		private Directions.Directions _directions;
		private int _counter;

		GameObject _directionsGO;
		private bool _recalculateNext;

		protected virtual void Awake()
		{
			if (_map == null)
			{
				_map = FindObjectOfType<AbstractMap>();
			}
			_directions = MapboxAccess.Instance.Directions;
			_map.OnInitialized += Query;
			_map.OnUpdated += Query;
			lr = GetComponent<LineRenderer>();
		}

		private void OnEnable()
		{
			
		}
		private void Update()
		{
			
		}
		public void Start()
		{
			_cachedWaypoints = new List<Vector3>(_waypoints.Length);
			foreach (var item in _waypoints)
			{
				_cachedWaypoints.Add(item.position);
			}
			_recalculateNext = false;
			StartCoroutine(QueryTimer());
		}

		protected virtual void OnDestroy()
		{
			_map.OnInitialized -= Query; 
			_map.OnUpdated -= Query;
		}

		void Query()
		{
			var count = _waypoints.Length;
			var wp = new Vector2d[count];
			for (int i = 0; i < count; i++)
			{
				wp[i] = _waypoints[i].GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
			}
			var _directionResource = new DirectionResource(wp, RoutingProfile.Driving);
			_directionResource.Steps = true;
			_directions.Query(_directionResource, HandleDirectionsResponse);
		}

		public IEnumerator QueryTimer()
		{
			while (true)
			{
				
				_map.UpdateMap();
				yield return new WaitForSeconds(UpdateFrequency);
				for (int i = 0; i < _waypoints.Length; i++)
				{
					if (_waypoints[i].position != _cachedWaypoints[i])
					{
						_recalculateNext = true;
						_cachedWaypoints[i] = _waypoints[i].position;
					}
				}

				if (_recalculateNext)
				{
					Query();
					_recalculateNext = false;
				}
			}
		}

		void HandleDirectionsResponse(DirectionsResponse response)
		{
			
			if (response == null || null == response.Routes || response.Routes.Count < 1)
			{
				return;
			}

			var dat = new List<Vector3>();
			foreach (var point in response.Routes[0].Geometry)
			{
				var conv = Conversions.GeoToWorldPosition(point.x, point.y, _map.CenterMercator, _map.WorldRelativeScale).ToVector3xz();
				conv.y = 1f;
				dat.Add(conv);
			}
		
			// var feat = new VectorFeatureUnity();
			// feat.Points.Add(dat);

			foreach(var d in dat)
			{
				d.Set(d.x, d.y, 10f);
			}
			lr.positionCount = dat.Count;
			lr.SetPositions(dat.ToArray());
			_cachedPositions = dat.ToArray();
			GetComponent<DisplayRouteTime>().RouteTime = (float) response.Routes[0].Duration;
		}
	}

}
