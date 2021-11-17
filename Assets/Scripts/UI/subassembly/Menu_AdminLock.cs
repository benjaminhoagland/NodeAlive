using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public class Menu_AdminLock : SerializedMonoBehaviour
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
        Instance.Message("Validating username...");
        Instance.Message("Validating password...");
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
        
        var username = InputIndicatorTuple[0].input.text;
        var password = InputIndicatorTuple[1].input.text;
        
        var authenticated = false;
        if(username == "admin" && password == "admin") authenticated = true;

        if(authenticated)
        {
            if(Instance.AdminLocked) Instance.AdminUnlock();
            else Instance.AdminLock();

        var message = Instance.AdminLocked ? "Locked" : "Unlocked";
        Instance.Message(message);
        parent.SetActive(false);
        }
        else
		{
            Instance.Message("Not Authenticated", 1);
		}
    }
}
