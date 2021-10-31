using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using System.Threading;
using System.Linq;

public class Menu_Selection : MonoBehaviour
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
        // do stuff
        Instance.SelectGUID(gameObject.transform
            .GetChild(3)
            .GetChild(0)
            .GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>().text);
        // Instance.Message(gameObject.transform
        //    .GetChild(3)
         //   .GetChild(0)
        //    .GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>().text);
		Instance.Message("Updating map...");
        var map = (from m in Data.SelectMaps() 
                   where m.Guid == Instance.SelectedMapGUID 
                   select m).ToList().FirstOrDefault();
        var c = new Mapbox.Utils.Vector2d((double) map.Latitude, (double) map.Longitude);
		_map.UpdateMap(c, map.Zoom);
    }
}
