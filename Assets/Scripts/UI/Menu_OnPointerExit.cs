using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Menu_OnPointerExit : MonoBehaviour, IPointerExitHandler
{
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // if(!Instance.UIEngaged) 
        this.gameObject.SetActive(false);
    }
}
