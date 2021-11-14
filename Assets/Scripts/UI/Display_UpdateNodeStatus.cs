using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Linq;
using Utility;
public class Display_UpdateNodeStatus : SerializedMonoBehaviour
{
    [SerializeField] public List<Image> imagesToUpdate;
	[SerializeField] public GameObject lastResponseGameObject;
	TMPro.TMP_Text lastResponse;
	private string guid;
	private bool knownState = true;
	private Color aliveColor = new Color(0f, 1f, 0f, 1f);
	private Color notAliveColor = new Color(1f, 0f, 0f, 1f);
	private void OnEnable()
	{
		guid = GetComponent<Identifier>().GUID;
		lastResponse = lastResponseGameObject.GetComponent<TMPro.TMP_Text>();
	}
	private void Update()
	{
		lastResponse.text = "Last: 0s";
		lastResponse.text = "Last: " + (from n in Instance.Nodes
										where n.GUID == guid
										select n.LastResponse).FirstOrDefault().DisplayFormat();
		var alive = (from n in Instance.Nodes
						 where n.GUID == guid
						 select n.Alive).FirstOrDefault();
		// Debug.Log("node is " + alive);
		if(alive == knownState)
		{
			return;
		}
		else if(alive)
		{
			foreach(var image in imagesToUpdate)
			{
				image.color = aliveColor;
			}
		}
		else
		{
			foreach(var image in imagesToUpdate)
			{
				image.color = notAliveColor;
			}
		}
		
		knownState = alive;
	}
}
