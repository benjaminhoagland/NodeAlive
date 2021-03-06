using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class Menu_PopulateDB : MonoBehaviour
{
    Button button;
    void Start()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }
    void Clicked()
    {
        Instance.Message("Populating data...");
        Data.Data.Populate();
        Instance.Message("Data populated.");
    }
}
