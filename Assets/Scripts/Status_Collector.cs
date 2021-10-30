using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status_Collector : MonoBehaviour
{
    TMPro.TMP_Text text;
    [SerializeField] float duration = 1f;
    WaitForSeconds wait;
    bool empty = false;
    void Awake()
    {
        wait = new WaitForSeconds(duration);
        text = GetComponentInChildren<TMPro.TMP_Text>();
    }
    
    void Update()
    {
        if(Instance.MessageQueue.Count > 0) text.text = Instance.MessageQueue[0];
        StartCoroutine("PollStatus");
    }

    IEnumerator PollStatus()
	{
        yield return wait;
        if(empty) gameObject.SetActive(false);
        if(Instance.MessageQueue.Count > 0)
        {
            empty = false;
            Instance.MessageQueue.RemoveAt(0);
        }
		else
		{
            empty = true;
		}
	}
}
