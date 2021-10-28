using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_ResetNewMapValues : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> textObjectsToClear;
	void OnEnable()
    {
        foreach(var textObject in textObjectsToClear)
		{
            TMPro.TMP_InputField input = textObject.GetComponent<TMPro.TMP_InputField>();
            
			//TMPro.TMP_Text textComponent = textObject.GetComponent<TMPro.TextMeshProUGUI>();
            input.text = "";
		}
    }
}
