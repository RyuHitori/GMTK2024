using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shopi : MonoBehaviour
{
    public ObjectData currentObject;
    public TextMeshProUGUI title, description, buyP, sellP, scale, remaining, available;
    public GameObject itemPF;
    public Transform itemHolder;

    ObjectData currentDisplaying;
    ObjectSystem objectSystem;
    InventorySystem inventory;
    ModelRotator modelRotator;

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");

        objectSystem = gameManager.GetComponent<ObjectSystem>();
        inventory = gameManager.GetComponent<InventorySystem>();
        modelRotator = GameObject.FindGameObjectWithTag("Insta3DManager").GetComponent<ModelRotator>();

        foreach(InventoryItem item in objectSystem.inventory)
        {
            
            CreateNewItem(item.data);
        }
    }

    private void Update()
    {
        if(currentDisplaying != currentObject)
        {
            currentDisplaying = currentObject;
            UpdateInfo();
        }
    }

    void UpdateInfo()
    {
        title.text = currentObject.title;
        description.text = currentObject.description;
        buyP.text = "• " + FormatNumber(currentObject.value) + " •";
        sellP.text = "• " + FormatNumber(currentObject.value * 0.7f) + " •";
        scale.text = "Scale " + FormatNumber(currentObject.scale);
        remaining.text = objectSystem.GetItemCount(currentObject) + " remaining";
        available.text = inventory.GetItemCount(currentObject) + " available";
        modelRotator.data = currentObject;
        
    }

    void CreateNewItem(ObjectData data)
    {
        GameObject newItem = Instantiate(itemPF, itemHolder);
        ShopiItem item = newItem.GetComponent<ShopiItem>();

        item.data = data;

        newItem.name = item.data.title;
        
        item.shopi = this;
    }

    public void Buy()
    {
        if(objectSystem.GetItemCount(currentObject) > 0)
        {
            if (inventory.money >= currentDisplaying.value)
            {
                inventory.AddItem(currentObject);
                inventory.AdjustMoney(-currentObject.value);
                objectSystem.RemoveItem(currentObject);
                UpdateInfo();
            }
        }

    }

    public void Sell()
    {
        if(inventory.GetItemCount(currentObject) > 0)
        {
            inventory.RemoveItem(currentObject);
            inventory.AdjustMoney(currentObject.value * 0.7f);
            objectSystem.AddItem(currentObject);
            UpdateInfo();
        }
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
