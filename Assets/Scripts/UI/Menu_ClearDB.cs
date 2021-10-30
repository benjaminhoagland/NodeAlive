using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class Menu_ClearDB : MonoBehaviour
{
    Button button;
    void Start()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }
    void Clicked()
    {
        Instance.Message("Clearing data...");
        Data.Clear();
        Instance.Message("Data cleared.");
    }
}
