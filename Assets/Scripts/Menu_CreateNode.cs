using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public class Menu_CreateNode : SerializedMonoBehaviour
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
        entity.LocationGUID = Instance.SelectedLocationGUID;
        entity.GUID = location.ChildGUID;
        entity.ChildGUID = Guid.NewGuid().ToString();
        entity.Type = 0;
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

        Instance.Message("Creating new node...");
        Data.Data.Schema.Table.Node node = new Data.Data.Schema.Table.Node();
        
        // this linq is very brittle, and will need heavy error handling
        // we should also disable inputs to the other ui elements to prevent other locations from becoming the "selected location"

        foreach(var e in Data.Data.Select.Entity())
		{
            Debug.Log(e.ToString());
		}
        node.EntityGUID = (from e in Data.Data.Select.Entity()
                           where e.LocationGUID == Instance.SelectedLocationGUID
                           select e.GUID).FirstOrDefault();
        if(node.EntityGUID == null)
        {
            throw new NullReferenceException("LINQ failure at node.EntityGUID assignment in Menu_CreateNode.cs");
        }
        node.Name = InputIndicatorTuple[0].input.text;
        
		node.Alive = true;
        node.GUID = entity.ChildGUID;
            
        int timeout = 90; 
        try
		{
            Int32.TryParse(InputIndicatorTuple[1].input.text, out timeout);
		}
        catch
		{
            Log.WriteError("Error parsing int input at Create New Node menu");
		}
        node.Timeout = timeout;

        Data.Data.Insert("node", new List<Data.Data.RecordStructure.Attribute>()
        {
            new Data.Data.RecordStructure.Attribute("entity_guid", node.EntityGUID),
            new Data.Data.RecordStructure.Attribute("name", node.Name),
            new Data.Data.RecordStructure.Attribute("guid", node.GUID),
            new Data.Data.RecordStructure.Attribute("date_created", DateTime.Now.ToString(Data.Data.timeformat)),
            new Data.Data.RecordStructure.Attribute("type", node.Type.ToString()),
            new Data.Data.RecordStructure.Attribute("map_guid", Instance.ActiveMap.GUID),
            new Data.Data.RecordStructure.Attribute("cluster_guid", "unassigned"),
            new Data.Data.RecordStructure.Attribute("timeout", node.Timeout.ToString()),
            new Data.Data.RecordStructure.Attribute("alive", node.Alive.ToString())
        }, true);
        Instance.Message("Created new node.");

        parent.SetActive(false);
    }
}
