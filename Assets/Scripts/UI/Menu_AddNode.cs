using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_AddNode : MonoBehaviour
{
    Button button;
    GameObject target;
    void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Clicked());
        target = GameObject.Find("NewNodePanel Host").transform.GetChild(0).gameObject;
    }

    void Clicked()
    {
            target.SetActive(true);
    }
}
