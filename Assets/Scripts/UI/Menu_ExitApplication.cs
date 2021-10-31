using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_ExitApplication : MonoBehaviour
{    // Update is called once per frame
    Button button;
    void Start()
    {
       button = GetComponent<Button>();

       button.onClick.AddListener(() => Clicked());
    }

    void Clicked()
    {
        Application.Quit();
    }
}
