using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Button))]

public class Menu_CreateMap : SerializedMonoBehaviour
{
    [ShowInInspector][SerializeField]
    List<(GameObject input, GameObject warning)> inputsToValidate;

    [SerializeField]GameObject parent;
    [SerializeField]GameObject mapLable;
    Button button;
    void Awake()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }
    void Clicked()
    {
        var inputError = false;
		try
		{
            foreach(var item in inputsToValidate)
		    {
                var input = item.input.GetComponent<TMPro.TMP_InputField>().text;
                if(input.Length == 0)
			    {
                    item.warning.SetActive(true);
                    inputError = true;
			    }
			    else
			    {
                    item.warning.SetActive(false);
			    }
		    }
		}
        catch
		{
            Log.WriteError("Input validation failure at " + MethodBase.GetCurrentMethod().Name);
		}
        if(inputError)
        {
            Instance.Message("Map parameter error;" + 
                System.Environment.NewLine +
                "please check input.");
            return;
        }
        Instance.Message("Populating GUID...");
        var guid = System.Guid.NewGuid().ToString();
        Instance.Message("Creating new map...");
        Data.Insert("map", new List<Data.RecordStructure.Attribute>()
        {
            new Data.RecordStructure.Attribute("name", inputsToValidate[0].input.GetComponent<TMPro.TMP_InputField>().text),
            new Data.RecordStructure.Attribute("location", inputsToValidate[1].input.GetComponent<TMPro.TMP_InputField>().text),
            new Data.RecordStructure.Attribute("latitude", inputsToValidate[2].input.GetComponent<TMPro.TMP_InputField>().text),
            new Data.RecordStructure.Attribute("longitude", inputsToValidate[3].input.GetComponent<TMPro.TMP_InputField>().text),
            new Data.RecordStructure.Attribute("zoom", inputsToValidate[4].input.GetComponent<TMPro.TMP_InputField>().text),
            new Data.RecordStructure.Attribute("guid", guid),
            new Data.RecordStructure.Attribute("date_created", DateTime.Now.ToString(Data.timeformat)),
            new Data.RecordStructure.Attribute("date_activated", DateTime.Now.ToString(Data.timeformat))
        }, true);
        Instance.Message("Map created.");
        Instance.Message("Setting active map...");
        Instance.SetActiveMap(guid);
        parent.SetActive(false);
    }
}
