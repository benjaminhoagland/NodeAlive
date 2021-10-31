using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Selection : MonoBehaviour
{
    Button button;
	void Awake()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }
    void Clicked()
    {
        // do stuff
        Instance.SelectGUID(gameObject.transform
            .GetChild(3)
            .GetChild(0)
            .GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>().text);
        // Instance.Message(gameObject.transform
        //    .GetChild(3)
         //   .GetChild(0)
        //    .GetChild(0).gameObject.GetComponent<TMPro.TMP_Text>().text);
       
    }
}
