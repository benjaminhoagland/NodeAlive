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

        // parent.SetActive(false);
    }
}
