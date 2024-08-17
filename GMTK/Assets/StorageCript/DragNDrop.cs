using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    private Vector3 offset;
    private Rigidbody rb;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    Vector3 GetMousePos()
    {
        
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z; 
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDown()
    {
        
        offset = transform.position - GetMousePos();
        isDragging = true;
        rb.isKinematic = true; 
    }

    void OnMouseDrag()
    {
        
        Vector3 newPosition = GetMousePos() + offset;

        
        rb.MovePosition(newPosition);
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false; 
    }
}
