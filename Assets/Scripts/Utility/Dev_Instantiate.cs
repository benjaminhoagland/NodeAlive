using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dev_Instantiate : MonoBehaviour
{
	private void Awake()
	{
		var guid = (from m in Data.Data.Select.Map() select m.GUID).ToList().FirstOrDefault();
		Instance.SetActiveMap(guid);
	}
}
