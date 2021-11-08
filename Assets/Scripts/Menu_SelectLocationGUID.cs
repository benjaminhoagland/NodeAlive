using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_SelectLocationGUID : MonoBehaviour
{    
    Button button;
    void Awake()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }

    void Clicked()
    {
        var guid = GetComponent<Identifier>().GUID;
        Instance.SelectLocationGUID(guid);
    }
}
