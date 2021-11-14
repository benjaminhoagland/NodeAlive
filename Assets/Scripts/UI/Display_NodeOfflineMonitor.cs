using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_NodeOfflineMonitor : MonoBehaviour
{
    [SerializeField] GameObject vignette;
	private void Awake()
	{
		
	}
	private void Update()
	{
		if(Instance.NODEOFFLINE)
		{ vignette.SetActive(true);}
		else
		{
			vignette.SetActive(false);
		}
	}
}
