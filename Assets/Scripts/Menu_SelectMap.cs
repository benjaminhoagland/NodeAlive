using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_SelectMap : MonoBehaviour
{
    [SerializeField]GameObject mapLable;
    Button button;
    void Awake()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }
    void Clicked()
    {
        // do stuff
        foreach(var mapRecord in Data.SelectStar("map", true))
        {
             foreach(var pair in mapRecord.columnValuePairs)
			 {
                Debug.Log(pair.Column + " : " + pair.Value);
			 }
        }

       
    }
}
