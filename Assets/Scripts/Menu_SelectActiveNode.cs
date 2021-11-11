using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Menu_SelectActiveNode : MonoBehaviour
{
    [SerializeField]public GameObject identifierTarget;
    Button button;
    void Awake()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }

    void Clicked()
    {
        var nGUID = identifierTarget.GetComponent<Identifier>().GUID;
        if(string.IsNullOrEmpty(nGUID))
		{
            throw new System.Exception("nGUID is null");
		}
        Instance.SetActiveNode(nGUID);
        var lGUID = (from l in Data.Data.Select.Location()
                     where l.ChildGUID == (from e in Data.Data.Select.Entity()
                                           where e.ChildGUID == nGUID
                                           select e.GUID).FirstOrDefault()
                     select l.GUID).FirstOrDefault();
        if(string.IsNullOrEmpty(lGUID))
		{
            throw new System.Exception("lGUID is null");
		}
        Instance.SetActiveLocation(lGUID);
    }
}
