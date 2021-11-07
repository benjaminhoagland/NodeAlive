using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Initialization : MonoBehaviour
{
    public GameObject statusTextGameObject;
    // Start is called before the first frame update
    void Start()
    {
        TMPro.TMP_Text text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // flag todo report status of initialization
    }
}
