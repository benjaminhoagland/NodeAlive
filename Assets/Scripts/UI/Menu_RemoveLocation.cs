using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_RemoveLocation : MonoBehaviour
{
    [SerializeField] public GameObject identifierTarget;
	private string guid;
    private Button button;
	private void Awake()
	{
		button = GetComponent<Button>();
        button.onClick.AddListener(() => Clicked());
    }

    void Clicked()
    {
        guid = identifierTarget.GetComponent<Identifier>().GUID;
        Instance.Message("Removing location..");
        Data.Data.Delete.Location(guid);
        Instance.Message("Location " + guid + " removed.");
    }
}
