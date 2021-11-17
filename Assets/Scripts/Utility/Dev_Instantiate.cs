using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dev_Instantiate : MonoBehaviour
{
	private void Awake()
	{
		Data.Data.Initialize();
		
		// only use this line if actively writing and cycling unity playmodes with a need to autoreset the db
		// Data.Data.Reset();
		
		var guid = (from m in Data.Data.Select.Map() select m.GUID).ToList().FirstOrDefault();
		Instance.SetActiveMap(guid);
	}
}
