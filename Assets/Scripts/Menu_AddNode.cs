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
        target = GameObject.Find("NewNodePanel");
        Debug.Log(target.name);
    }

    void Clicked()
    {
        if(target.gameObject.activeInHierarchy)
		{
            target.gameObject.SetActive(false);
		}
        else
		{
            target.gameObject.SetActive(true);
		}
    }
}
