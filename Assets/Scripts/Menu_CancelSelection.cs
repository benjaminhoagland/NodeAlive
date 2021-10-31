using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using System.Linq;
public class Menu_CancelSelection : MonoBehaviour
{
    Button button;
    AbstractMap _map;
    void Awake()
    {
       button = GetComponent<Button>();

       button.onClick.AddListener(() => Clicked());
        _map = FindObjectOfType<AbstractMap>();
    }

    void Clicked()
    {
        Instance.Message("Updating map...");
        var map = (from m in Data.SelectMaps() 
                   where m.Guid == Instance.ActiveMap.GUID 
                   select m).ToList().FirstOrDefault();
        var c = new Mapbox.Utils.Vector2d((double) map.Latitude, (double) map.Longitude);
		_map.UpdateMap(c, map.Zoom);
    }
}
