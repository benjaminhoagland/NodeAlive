using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class Menu_ResetDB : MonoBehaviour
{
    Button button;
    void Start()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }
    void Clicked()
    {
        Instance.Message("Resetting data...");
        Data.Data.Reset();
        Instance.Message("Data reset.");
    }
}
