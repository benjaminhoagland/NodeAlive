using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    Button button;
    void Start()
    {
       button = GetComponent<Button>();

       button.onClick.AddListener(() => Clicked());
    }

    void Clicked()
    {
        if(target.gameObject.activeInHierarchy);
        target.gameObject.SetActive();
        //flag:workinghere
    }
}
