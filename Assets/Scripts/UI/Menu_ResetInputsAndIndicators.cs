using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_ResetInputsAndIndicators : MonoBehaviour
{
    
    [SerializeField] List<GameObject> inputObjectsToClear;
    [SerializeField] List<GameObject> indicatorsToClear;

	void OnEnable()
    {
        foreach(var inputObject in inputObjectsToClear)
		{
            TMPro.TMP_InputField input = inputObject.GetComponent<TMPro.TMP_InputField>();
            input.text = "";
		}
        foreach(var indicator in indicatorsToClear)
		{
            indicator.SetActive(false);
		}
    }
}
