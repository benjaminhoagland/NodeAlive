using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_SelectMap : MonoBehaviour
{
    [SerializeField]GameObject mapLable;
    Button button;
    GameObject parent;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Clicked());
        parent = gameObject.transform.parent.gameObject;
    }
    void Clicked()
    {
        // do stuff

        Instance.Message("Map created.");
        Instance.Message("Setting active map...");
        if(Instance.SelectedMapGUID == null)
		{
			Log.WriteError("Instance.SelectedMapGUID is null"); 
		}
        Instance.SetActiveMap(Instance.SelectedMapGUID);
        parent.SetActive(false);
    }
}
