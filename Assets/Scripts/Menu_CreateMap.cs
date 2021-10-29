using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class Menu_CreateMap : MonoBehaviour
{
    Button button;
    void Start()
    {
       button = GetComponent<Button>();
       button.onClick.AddListener(() => Clicked());
    }
    void Clicked()
    {
        // parse input
        // send input to db
        // set active map
        // Instance.SetActiveMap(someid)

    }
}
