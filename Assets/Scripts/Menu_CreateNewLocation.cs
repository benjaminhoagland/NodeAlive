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
        location.Latitude = float.Parse(InputIndicatorTuple[1].input.text);
        location.Longitude = float.Parse(InputIndicatorTuple[1].input.text);

        Data.Data.Insert("location", new List<Data.Data.RecordStructure.Attribute>()
        {
            new Data.Data.RecordStructure.Attribute("map_id", 
                (from m 
                in Data.Data.Select.Map() 
                where m.GUID == Instance.ActiveMap.GUID
                select m.ID).ToString()),
            new Data.Data.RecordStructure.Attribute("address", location.Address),
            new Data.Data.RecordStructure.Attribute("latitude", location.Latitude.ToString()),
            new Data.Data.RecordStructure.Attribute("longitude", location.Longitude.ToString()),
            new Data.Data.RecordStructure.Attribute("guid", Guid.NewGuid().ToString()),
            new Data.Data.RecordStructure.Attribute("date_created", DateTime.Now.ToString(Data.Data.timeformat)),
            new Data.Data.RecordStructure.Attribute("child_guid", "undefined"),
        }, true);

        parent.SetActive(false);
    }
}
