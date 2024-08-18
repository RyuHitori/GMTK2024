using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mover : MonoBehaviour, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler, IPointerDownHandler
{
    public Window target;

    Cursor cursor;

    private void Start()
    {
        cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        target.StartMove();
        
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(0))
        {
            cursor.cursorType = CursorType.Hand;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!Input.GetMouseButton(0))
        {
            cursor.cursorType = CursorType.Default;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        cursor.cursorType = CursorType.Hand;
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        cursor.cursorType = CursorType.Grab;
        target.transform.SetAsLastSibling();
    }
}
