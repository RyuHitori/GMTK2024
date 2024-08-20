using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TradeItemOrigin
{
    Inventory,
    Stock,
}

public class TradeItem : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler
{
    public TradeItemOrigin origin;

    public InventoryItem item;
    public UTrade UTrade;

    public Image icon, background;
    public Color highlight, inactive, greenActive, yellowActive;

    bool active;

    public TextMeshProUGUI title, scale, count;

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButtonDown(0))
        {
            switch (origin)
            {
                case TradeItemOrigin.Inventory:
                    UTrade.AddToInventory(item.data);
                    break;
                case TradeItemOrigin.Stock:
                    UTrade.AddToSystem(item.data);
                    break;
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            switch (origin)
            {
                case TradeItemOrigin.Inventory:
                    UTrade.RemoveFromInventory(item.data);
                    break;
                case TradeItemOrigin.Stock:
                    UTrade.RemoveFromSystem(item.data);
                    break;
            }
        }

        UpdateInfo();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UTrade.currentObject = item.data;
        if(!active) background.color = highlight;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!active) background.color = inactive;
    }

    void UpdateInfo()
    {
        ObjectData data = item.data;

        int usedCount = 0;

        switch (origin)
        {
            case TradeItemOrigin.Inventory:
                foreach(InventoryItem item in UTrade.inventorySelected)
                {
                    if(item.data == data)
                    {
                        usedCount = item.count;
                        break;
                    }
                }
                break;
            case TradeItemOrigin.Stock:
                foreach (InventoryItem item in UTrade.systemSelected)
                {
                    if (item.data == data)
                    {
                        usedCount = item.count;
                        break;
                    }
                }
                break;
        }

        icon.sprite = data.icon;
        title.text = data.title;
        scale.text = FormatNumber(data.scale);
        count.text = "(" + (item.count - usedCount) + ")";

        if(usedCount > 0)
        {
            active = true;
            switch (origin)
            {
                case TradeItemOrigin.Inventory:
                    background.color = greenActive;
                    break;
                case TradeItemOrigin.Stock:
                    background.color = yellowActive;
                    break;
            }
        }
        else
        {
            active = false;
            background.color = inactive;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateInfo();

        background.color = inactive;
    }

    // Update is called once per frame
    void Update()
    {

    }

    string FormatNumber(float value)
    {
        if (value >= 1_000_000_000) // 1 billion
            return (value / 1_000_000_000f).ToString("0.##") + "B";
        if (value >= 1_000_000) // 1 million
            return (value / 1_000_000f).ToString("0.##") + "M";
        if (value >= 1_000) // 1 thousand
            return (value / 1_000f).ToString("0.##") + "K";
        return value.ToString("0.##"); // Less than 1 thousand
    }

}
