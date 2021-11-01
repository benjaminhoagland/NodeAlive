using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusHost : MonoBehaviour
{
    [SerializeField] GameObject statusPanel;
    void Update()
    {
        if(Instance.MessageQueue.Count > 0)
            statusPanel.SetActive(true);
    }
}
