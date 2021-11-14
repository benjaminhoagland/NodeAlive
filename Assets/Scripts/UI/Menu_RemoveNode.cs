using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class Menu_RemoveNode : MonoBehaviour
{
    Button button;
    void Awake()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }

    void Clicked()
    {
        var nodeGUID = Instance.ActiveNode.GUID;
        Data.Data.Delete.Node(nodeGUID);
        Instance.Message("Deleting node...");
        Instance.ClearActiveNode();
        var entityGUID = (from e in Data.Data.Select.Entity()
                          where e.ChildGUID == nodeGUID
                          select e.GUID).FirstOrDefault();
        Instance.Message("Deleting entity...");
        Data.Data.Delete.Entity(entityGUID);
        var locationGUID = (from l in Data.Data.Select.Location()
                            where l.ChildGUID == entityGUID
                            select l.GUID).FirstOrDefault();
        Instance.Message("Updating location references...");
        Data.Data.Update.RemoveLocationChildReference(locationGUID);
        var scriptGUID = (from s in Data.Data.Select.Script()
                          where s.NodeGUID == nodeGUID
                          select s.GUID).FirstOrDefault();
        Instance.Message("Removing script references...");
        Data.Data.Delete.Script(scriptGUID);
        Instance.Message("Complete");
    }

}
