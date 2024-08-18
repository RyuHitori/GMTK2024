using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Window : MonoBehaviour, IPointerDownHandler
{
    [Header("Attributes")]
    
    public Sprite ButtonIcon;
    public int id;
    public Vector2 maxSize, minSize;
    public Vector2 preferedPos, preferedSize;

    RectTransform rectTransform;

    bool vRezing, hRezing, cRezing, moving;
    float targetScale;
    Vector2 cursorAnchor, windowPosition;
    float hAnchor, vAnchor;

    Cursor cursor;

    public TaskbarButton button { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.zero;

    }

    private void Start()
    {
        rectTransform.sizeDelta = preferedSize;
        rectTransform.anchoredPosition = preferedPos;


        cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>();
        targetScale = 1.0f;
    }

    private void Update()
    {
        rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, Vector3.one * targetScale, 30 * Time.deltaTime);
        Vector2 cursorDelta = cursor.position - cursorAnchor;

        if(Input.GetMouseButtonUp(0))
        {
            if(rectTransform.rect.Overlaps(cursor.transform.GetComponent<RectTransform>().rect))
            {
                cursor.cursorType = CursorType.Default;
            }

            moving = vRezing = hRezing = cRezing = false;
            
            
        }

        if(cRezing)
        {
            Vector2 size = rectTransform.sizeDelta;

            size.y = vAnchor - cursorDelta.y;
            size.x = hAnchor + cursorDelta.x;

            rectTransform.sizeDelta = size;

            cursor.cursorType = CursorType.CRez;
        }
        if(hRezing)
        {
            Vector2 size = rectTransform.sizeDelta;

            size.x = hAnchor + cursorDelta.x;

            rectTransform.sizeDelta = size;

            cursor.cursorType = CursorType.HRez;
        }
        if(vRezing)
        {
            Vector2 size = rectTransform.sizeDelta;

            size.y = vAnchor - cursorDelta.y;

            rectTransform.sizeDelta = size;

            cursor.cursorType = CursorType.VRez;
        }
        if(moving)
        {
            rectTransform.anchoredPosition = windowPosition + cursorDelta;

            cursor.cursorType = CursorType.Grab;
        }

        rectTransform.sizeDelta = new Vector2(  Mathf.Clamp(rectTransform.sizeDelta.x, minSize.x, maxSize.x),
                                                Mathf.Clamp(rectTransform.sizeDelta.y, minSize.y, maxSize.y));


    }
    public void StartVerticalResize()
    {
        vRezing = true;
        cursorAnchor = cursor.position;

        vAnchor = rectTransform.rect.height;
    }

    public void StartHorizontalResize()
    {
        hRezing = true;
        cursorAnchor = cursor.position;

        hAnchor = rectTransform.rect.width;
    }

    public void StartCornerResize()
    {
        cRezing = true;
        cursorAnchor = cursor.position;

        vAnchor = rectTransform.rect.height;
        hAnchor = rectTransform.rect.width;
    }

    public void StartMove()
    {
        moving = true;
        cursorAnchor = cursor.position;
        windowPosition = rectTransform.anchoredPosition;
    }

    public void SetTargetScale(float value)
    {
        targetScale = value;
    }
    public void SetControlButton(TaskbarButton taskbarButton)
    {
        button = taskbarButton;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    public void OnCloseButton()
    {
        button.Close();
    }

    public void OnMinimizeButton()
    {
        button.Minimize();
    }
}
