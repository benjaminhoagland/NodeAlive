using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[RequireComponent(typeof(Button))]
public class Menu_CreateMap : MonoBehaviour
{
    [SerializeField]List<(GameObject input, GameObject warning)> inputsToValidate;

    Button button;
    TMPro.TMP_Text statusText;
    void Awake()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }
    void Clicked()
    {
        var inputError = false;
		try
		{
            foreach(var item in inputsToValidate)
		    {
                var input = item.input.GetComponent<TMPro.TMP_InputField>().text;
                if(input.Length == 0)
			    {
                    item.warning.SetActive(true);
                    inputError = true;
			    }
			    else
			    {
                    item.warning.SetActive(false);
			    }
		    }
		}
        catch
		{
            Log.WriteError("Input validation failure at " + MethodBase.GetCurrentMethod().Name);
		}
        if(inputError)
        {
            Instance.Message("Map parameter error;" + 
                System.Environment.NewLine +
                "please check input.");
            return;
        }
        Instance.Message("Populating GUID...");
        var guid = System.Guid.NewGuid().ToString();
        Instance.Message("Creating new map...");
        Data.Insert("map", new List<Data.Record.ColumnValuePair>()
        {
            new Data.Record.ColumnValuePair("name", inputsToValidate[0].input.GetComponent<TMPro.TMP_InputField>().text),
            new Data.Record.ColumnValuePair("location", inputsToValidate[1].input.GetComponent<TMPro.TMP_InputField>().text),
            new Data.Record.ColumnValuePair("latitude", inputsToValidate[2].input.GetComponent<TMPro.TMP_InputField>().text),
            new Data.Record.ColumnValuePair("longitude", inputsToValidate[3].input.GetComponent<TMPro.TMP_InputField>().text),
            new Data.Record.ColumnValuePair("zoom", inputsToValidate[4].input.GetComponent<TMPro.TMP_InputField>().text),
            new Data.Record.ColumnValuePair("guid", guid)
        });
        Instance.Message("Setting active map...");
        Instance.SetActiveMap(guid);

    }
}
