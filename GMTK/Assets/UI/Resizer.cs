using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ResizeType
{
    Corner,
    Horizontal,
    Vertical,
}
public class Resizer : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Window target;
    public ResizeType resizeType;

    Cursor cursor;

    private void Start()
    {
        cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        switch (resizeType)
        {
            case ResizeType.Corner:
                target.StartCornerResize();
                break;
            case ResizeType.Horizontal:
                target.StartHorizontalResize();
                break;
            case ResizeType.Vertical:
                target.StartVerticalResize();
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!Input.GetMouseButton(0))
        {
            switch (resizeType)
            {
                case ResizeType.Corner:
                    cursor.cursorType = CursorType.CRez;
                    break;
                case ResizeType.Horizontal:
                    cursor.cursorType = CursorType.HRez;
                    break;
                case ResizeType.Vertical:
                    cursor.cursorType = CursorType.VRez;
                    break;
            }
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!Input.GetMouseButton(0))
        {
            cursor.cursorType = CursorType.Default;
        }
        
    }
}
