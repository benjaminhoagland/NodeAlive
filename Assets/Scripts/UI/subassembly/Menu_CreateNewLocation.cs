using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Linq;
using System;

public class Menu_CreateNewLocation : SerializedMonoBehaviour
{
    [ShowInInspector][SerializeField] List<(TMPro.TMP_InputField input, GameObject indicator)> InputIndicatorTuple;
    Button button;
    [SerializeField] GameObject parent;
    void Awake()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }

    void Clicked()
    {
        Instance.Message("Validating address...");
        Instance.Message("Validating coordinates...");
        var validated = true;
        foreach(var tuple in InputIndicatorTuple)
		{
            if(tuple.input.text.Length == 0)
			{
                tuple.indicator.SetActive(true);
                validated = false;
			}
            else
			{
                tuple.indicator.SetActive(false);
			}
		}
        if (!validated) return;
        Data.Data.Schema.Table.Location location = new Data.Data.Schema.Table.Location();
        location.Address = InputIndicatorTuple[0].input.text;
        float lat = 12.3456f;
        float lon = 12.3456f;
        try
		{
            float.TryParse(InputIndicatorTuple[1].input.text, out lat);
            float.TryParse(InputIndicatorTuple[2].input.text, out lon);
		}
        catch
		{
            Log.WriteError("Error parsing float input at Create New Location menu");
		}
        location.Latitude = lat;
        location.Longitude = lon;

        Data.Data.Insert("location", new List<Data.Data.RecordStructure.Attribute>()
        {
            new Data.Data.RecordStructure.Attribute("map_guid", 
                (from m 
                in Data.Data.Select.Map() 
                where m.GUID == Instance.ActiveMap.GUID
                select m.GUID).FirstOrDefault().ToString()),
            new Data.Data.RecordStructure.Attribute("address", location.Address),
            new Data.Data.RecordStructure.Attribute("latitude", location.Latitude.ToString()),
            new Data.Data.RecordStructure.Attribute("longitude", location.Longitude.ToString()),
            new Data.Data.RecordStructure.Attribute("guid", Guid.NewGuid().ToString()),
            new Data.Data.RecordStructure.Attribute("date_created", DateTime.Now.ToString(Data.Data.timeformat)),
            new Data.Data.RecordStructure.Attribute("child_guid", "unassigned"),
        }, true);
        Instance.Message("Added new unassigned location.");
        parent.SetActive(false);
    }
}
