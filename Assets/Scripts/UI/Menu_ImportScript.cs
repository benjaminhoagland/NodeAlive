using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;
using System.IO;

public class Menu_ImportScript : SerializedMonoBehaviour
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
            throw new NullReferenceException("Input fields are null at Menu_ImportScript.cs");
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
        Instance.Message("Validating script...");
        var fileInfo = new FileInfo(InputIndicatorTuple[1].input.text);
        Debug.Log(fileInfo.FullName);
        if(!fileInfo.Exists)
        {
            InputIndicatorTuple[1].indicator.SetActive(true);
            var msg = "File not found or problem with file fullname.";
            Instance.Message(msg);
            throw new FileNotFoundException(msg);
        }
        Instance.Message(fileInfo.FullName + " script validated.");
        Instance.Message("Importing script...");
        var script = new Data.Data.Schema.Table.Script();
        script.GUID = Guid.NewGuid().ToString();
        script.Name = InputIndicatorTuple[0].input.text;
        script.NodeGUID = Instance.ActiveNode.GUID;
        script.Path = InputIndicatorTuple[1].input.text;
        script.DateCreated = DateTime.Now;
        script.Contents = File.ReadAllText(script.Path);
        
        Instance.Message("Attaching script...");
        Data.Data.Insert("script", new List<Data.Data.RecordStructure.Attribute>()
		{
            new Data.Data.RecordStructure.Attribute("guid", script.GUID),
            new Data.Data.RecordStructure.Attribute("name", script.Name),
            new Data.Data.RecordStructure.Attribute("node_guid", script.NodeGUID),
            new Data.Data.RecordStructure.Attribute("path", script.Path),
            new Data.Data.RecordStructure.Attribute("date_created", script.DateCreated.ToString(Data.Data.timeformat)),
            new Data.Data.RecordStructure.Attribute("contents", script.Contents)
		});
        Instance.Message("Script imported and attached.");

        parent.SetActive(false);
    }
}
