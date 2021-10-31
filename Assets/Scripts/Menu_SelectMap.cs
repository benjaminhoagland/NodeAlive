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
        Instance.SetActiveMap(Instance.SelectedMapGUID);

       
    }
}
