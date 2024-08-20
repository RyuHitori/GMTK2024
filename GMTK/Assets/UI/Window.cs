using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Window : MonoBehaviour, IPointerDownHandler
{
    [Header("Attributes")]
    
    public Sprite ButtonIcon;
    public int id;
    public Vector2 maxSize, minSize;
    public Vector2 preferedPos, preferedSize;
    public bool focusing;

    RectTransform rectTransform;

    bool vRezing, hRezing, cRezing, moving;
    float targetScale;
    Vector2 cursorAnchor, windowPosition;
    float hAnchor, vAnchor;

    Cursor cursor;

    public Color inactive;
    public Color active;

    Image background;

    public TaskbarButton button { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.zero;

    }

    private void Start()
    {
        inactive = Color.white;
        active = new Color(19, 255, 0);

        background = transform.Find("Background").GetComponent<Image>();

        rectTransform.sizeDelta = preferedSize;
        rectTransform.anchoredPosition = preferedPos;


        cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>();
        targetScale = 1.0f;
    }

    private void Update()
    {
        background.color = focusing ? active : inactive;

        focusing = transform.GetSiblingIndex() == transform.parent.childCount - 1;

        rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, Vector3.one * targetScale, 30 * Time.deltaTime);

        Vector2 clampedCursorPosition = new Vector2(
                Mathf.Clamp(cursor.position.x, cursor.bound1.x, cursor.bound2.x),
                Mathf.Clamp(cursor.position.y, cursor.bound1.y, cursor.bound2.y)
            );

        Vector2 cursorDelta = clampedCursorPosition - cursorAnchor;

        if(Input.GetMouseButtonUp(0))
        {

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
