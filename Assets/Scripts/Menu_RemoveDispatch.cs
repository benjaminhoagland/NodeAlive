using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Sirenix.OdinInspector;
public class Menu_RemoveDispatch : MonoBehaviour
{
    Button button;
    [SerializeField] public GameObject identifierTarget;
    void Awake()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }

    void Clicked()
    {
        
        
        var dispatchGUID = identifierTarget.GetComponent<Identifier>().GUID;
        Data.Data.Delete.Dispatch(dispatchGUID);
        Instance.Message("Deleting dispatch...");
        var entityGUID = (from e in Data.Data.Select.Entity()
                          where e.ChildGUID == dispatchGUID
                          select e.GUID).FirstOrDefault();
        Instance.Message("Deleting entity...");
        Data.Data.Delete.Entity(entityGUID);
        var locationGUID = (from l in Data.Data.Select.Location()
                            where l.ChildGUID == entityGUID
                            select l.GUID).FirstOrDefault();
        Instance.Message("Updating location references...");
        Data.Data.Update.RemoveLocationChildReference(locationGUID);

        Instance.Message("Complete");
    }

}
