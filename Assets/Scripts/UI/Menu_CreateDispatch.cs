using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public class Menu_CreateDispatch : SerializedMonoBehaviour
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
        Instance.Message("Validating node information...");
        var validated = true;
        if(InputIndicatorTuple.Count < 1)
        { 
            throw new NullReferenceException("Input fields are null at Menu_CreateNode");
        }
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

        var existingDispatch = (from d in Data.Data.Select.Dispatch()
                                where d.MapGUID == Instance.ActiveMap.GUID
                                select d).ToList();
        if(existingDispatch.Count > 0)
		{
            // handle existing dispatch
            Instance.Message("Remove existing dispatch before adding.", 0.5f);
            return;
		}


        Instance.Message("Updating location...");

        Data.Data.Schema.Table.Location location = new Data.Data.Schema.Table.Location();
        location.GUID = Instance.SelectedLocationGUID;
        location.ChildGUID = Guid.NewGuid().ToString();

        Data.Data.Update.Location(location.GUID, new List<Data.Data.RecordStructure.Attribute>() 
                                                    { 
                                                        new Data.Data.RecordStructure.Attribute("child_guid", location.ChildGUID)
                                                    });

        Instance.Message("Updated location.");
        Instance.Message("Creating new entity...");
        Data.Data.Schema.Table.Entity entity = new Data.Data.Schema.Table.Entity();
        entity.LocationGUID = Instance.ActiveLocation.GUID;
        entity.GUID = location.ChildGUID;
        entity.ChildGUID = Guid.NewGuid().ToString();
        entity.Type = 2;
        entity.DateCreated = DateTime.Now;
        Data.Data.Insert("entity", new List<Data.Data.RecordStructure.Attribute>()
        {
            new Data.Data.RecordStructure.Attribute("guid", entity.GUID),
            new Data.Data.RecordStructure.Attribute("date_created", entity.DateCreated.ToString(Data.Data.timeformat)),
            new Data.Data.RecordStructure.Attribute("location_guid", entity.LocationGUID),
            new Data.Data.RecordStructure.Attribute("type", entity.Type.ToString()),
            new Data.Data.RecordStructure.Attribute("child_guid", entity.ChildGUID)
        }, true);

        Instance.Message("Created new entity.");

        Instance.Message("Creating new dispatch...");
        Data.Data.Schema.Table.Dispatch dispatch = new Data.Data.Schema.Table.Dispatch();
        
        // this linq is very brittle, and will need heavy error handling
        // we should also disable inputs to the other ui elements to prevent other locations from becoming the "selected location"

        dispatch.EntityGUID = (from e in Data.Data.Select.Entity()
                           where e.LocationGUID == Instance.SelectedLocationGUID
                           select e.GUID).FirstOrDefault();
        if(dispatch.EntityGUID == null)
        {
            throw new NullReferenceException("LINQ failure at node.EntityGUID assignment in Menu_CreateNode.cs");
        }
        dispatch.Name = InputIndicatorTuple[0].input.text;
        
        dispatch.GUID = entity.ChildGUID;
       
        Data.Data.Insert("dispatch", new List<Data.Data.RecordStructure.Attribute>()
        {
            new Data.Data.RecordStructure.Attribute("entity_guid", dispatch.EntityGUID),
            new Data.Data.RecordStructure.Attribute("name", dispatch.Name),
            new Data.Data.RecordStructure.Attribute("guid", dispatch.GUID),
            new Data.Data.RecordStructure.Attribute("date_created", DateTime.Now.ToString(Data.Data.timeformat)),
            new Data.Data.RecordStructure.Attribute("type", dispatch.Type.ToString()),
            new Data.Data.RecordStructure.Attribute("map_guid", Instance.ActiveMap.GUID)
        }, true);
        Instance.Message("Created new dispatch.");

        parent.SetActive(false);
    }
}
