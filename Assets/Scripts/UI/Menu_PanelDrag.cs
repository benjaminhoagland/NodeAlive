using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Menu_PanelDrag : EventTrigger
{
    private bool drag;
    RectTransform rectTransform;
	Vector2 origin;
    Vector2 mouseOrigin;
	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		origin = rectTransform.anchoredPosition;
	}
	public void Update() 
    {
        if(drag)
        {
            var n = new Vector2(Input.mousePosition.x,Input.mousePosition.y) - mouseOrigin;
            rectTransform.anchoredPosition = origin + n;
            Debug.Log("dragging");
        }
    }
    public override void OnPointerDown(PointerEventData eventData) 
    {
        drag = true;
        mouseOrigin = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
    }
    public override void OnPointerUp(PointerEventData eventData) 
    {
        drag = false;
    }
	private void OnEnable()
	{
		rectTransform.anchoredPosition = origin;
	}
}
