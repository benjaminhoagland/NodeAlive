using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu_LockUnlockUpdateMenuButton : MonoBehaviour
{
	[SerializeField] TMPro.TMP_Text text;
	private void Awake()
	{
		text = GetComponent<TMPro.TMP_Text>();
		
	}
	private void Update()
	{
		text.text = Instance.AdminLocked ? "Unlock" : "Lock";
	}
}
