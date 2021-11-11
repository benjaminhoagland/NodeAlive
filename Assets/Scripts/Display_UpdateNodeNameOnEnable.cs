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
	private void Awake()
	{
		nodeName = nodeNameGameObject.GetComponent<TMPro.TMP_Text>();
		nodeName.text = "New Node";
		scriptName = scriptNameGameObject.GetComponent<TMPro.TMP_Text>();
		scriptName.text = "Script: unassigned";
	}
	private void OnEnable()
	{
		var guid = GetComponent<Identifier>().GUID;
		nodeName.text = (from n in Data.Data.Select.Node()
						 where n.GUID == guid
						 select n.Name).FirstOrDefault();

		// flag:todo set this text to a script with a node guid that matches the node guid
	}
}
