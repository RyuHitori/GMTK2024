using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CursorType
{
    Default,
    Hand,
    Grab,
    HRez,
    VRez,
    CRez,
}

public class Cursor : MonoBehaviour
{
    public RectTransform cursorImage; // Reference to the UI Image's RectTransform
    public Image cursor;
    public Sprite cursorDefault, cursorHand, cursorGrab, cursorHRez, cursorVRez, cursorCRez;

    public CursorType cursorType;
    public Vector2 position { get; private set; }

    public Vector2 bound1, bound2;


    void Start()
    {
        UnityEngine.Cursor.visible = false;

    }

    void Update()
    {
        // Convert mouse position to screen point in UI space
        Vector2 cursorPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            cursorImage.parent as RectTransform,
            Input.mousePosition,
            Camera.main,
            out cursorPos
        );

        position = cursorPos;

        // Interpolate cursor image position towards the target position
        Vector2 cursorPosition = Vector3.Lerp(cursorImage.localPosition, cursorPos, 90 * Time.deltaTime);

        // Update the cursor image's local position
        cursorImage.localPosition = cursorPosition;

        switch (cursorType)
        {
            case CursorType.Default:
                cursor.sprite = cursorDefault; 
                break;
            case CursorType.Hand:
                cursor.sprite = cursorHand;
                break;
            case CursorType.Grab:
                cursor.sprite = cursorGrab;
                break;
            case CursorType.HRez:
                cursor.sprite = cursorHRez;
                break;
            case CursorType.VRez:
                cursor.sprite = cursorVRez;
                break;
            case CursorType.CRez:
                cursor.sprite = cursorCRez;
                break;
        }
    }



}
