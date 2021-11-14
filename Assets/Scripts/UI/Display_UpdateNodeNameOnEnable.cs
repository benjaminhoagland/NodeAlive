using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Display_UpdateNodeNameOnEnable : MonoBehaviour
{
    [SerializeField] GameObject nodeNameGameObject;
	TMPro.TMP_Text nodeName;
	[SerializeField] GameObject scriptNameGameObject;
	TMPro.TMP_Text scriptName;
	[SerializeField] GameObject identifierTarget;
	[SerializeField] GameObject timeoutGameObject;
	TMPro.TMP_Text timeout;
	private void Awake()
	{
		nodeName = nodeNameGameObject.GetComponent<TMPro.TMP_Text>();
		nodeName.text = "Name: parsing...";
		scriptName = scriptNameGameObject.GetComponent<TMPro.TMP_Text>();
		scriptName.text = "Script: parsing...";
		timeout = timeoutGameObject.GetComponent<TMPro.TMP_Text>();
		timeout.text = "Timeout: parsing...";
	}
	private void OnEnable()
	{
		var guid = identifierTarget.GetComponent<Identifier>().GUID;
		var node = (from n in Data.Data.Select.Node()
						 where n.GUID == guid
						 select n).FirstOrDefault();
		if(node == null)
		{ 
			// Debug.Log("no node returned by LINQ at display_updatenodenameonenable"); 
			return; 
		}
		nodeName.text = node.Name;

		scriptName.text = "Script: " + (from s in Data.Data.Select.Script()
						   where s.NodeGUID == guid
					       select s.Name).FirstOrDefault();

		timeout.text = "Timeout: " + node.Timeout;
	}
}
