using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Display_UpdateDispatchNameOnEnable : MonoBehaviour
{
    [SerializeField] GameObject dispatchNameGameObject;
	TMPro.TMP_Text dispatchName;
	[SerializeField] GameObject identifierTarget;
	private void Awake()
	{
		dispatchName = dispatchNameGameObject.GetComponent<TMPro.TMP_Text>();
		dispatchName.text = "Name: parsing...";
	}
	private void OnEnable()
	{
		var guid = identifierTarget.GetComponent<Identifier>().GUID;
		var dispatch = (from d in Data.Data.Select.Dispatch()
						 where d.GUID == guid
						 select d).FirstOrDefault();
		if(dispatch == null)
		{ 
			// Debug.Log("no node returned by LINQ at display_updatenodenameonenable"); 
			return; 
		}
		dispatchName.text = dispatch.Name;

	}
}
