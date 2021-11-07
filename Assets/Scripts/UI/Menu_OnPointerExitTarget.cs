using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Menu_OnPointerExitTarget : MonoBehaviour, IPointerExitHandler
{
    [SerializeField]GameObject target;
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // if(!Instance.UIEngaged) 
        target.gameObject.SetActive(false);
    }
}
