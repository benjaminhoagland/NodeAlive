using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_ResetNewMapValues : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> textObjectsToClear;
    [SerializeField] List<GameObject> indicatorsToClear;
	void OnEnable()
    {
        foreach(var textObject in textObjectsToClear)
		{
            TMPro.TMP_InputField input = textObject.GetComponent<TMPro.TMP_InputField>();
            
			//TMPro.TMP_Text textComponent = textObject.GetComponent<TMPro.TextMeshProUGUI>();
            input.text = "";
		}
        foreach(var i in indicatorsToClear)
		{
            i.SetActive(false);
		}
    }
}
