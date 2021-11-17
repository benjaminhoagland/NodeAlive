using System.Collections;
using UnityEngine;

public class Status_Collector : MonoBehaviour
{
    TMPro.TMP_Text text;
    bool pollIsRunning = false;
    void Awake()
    {
        text = GetComponentInChildren<TMPro.TMP_Text>();
    }
    void Update()
    {
        if( Instance.MessageQueue.Count > 0)
        {
            var item = Instance.MessageQueue.Dequeue();
            text.text = item.message;
            if(!pollIsRunning) StartCoroutine(PollStatus(item.showForDelay));
        }
    }
	IEnumerator PollStatus(float delay)
	{
        pollIsRunning = true;
        // Debug.Log(delay.ToString());
        yield return new WaitForSeconds(1);
        gameObject.SetActive(Instance.MessageQueue.Count > 0 ? true : false);
        pollIsRunning = false;
	}
}
