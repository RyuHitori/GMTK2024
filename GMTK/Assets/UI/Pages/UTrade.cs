using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UTrade : MonoBehaviour
{
    InventorySystem inventory;
    ObjectSystem objectSystem;

    public List<InventoryItem> inventorySelected;
    public List<InventoryItem> systemSelected;

    ModelRotator modelRotator;

    public ObjectData currentObject;
    public GameObject tradeItemPF;
    public Transform inventoryHolder, stockHolder;

    ObjectData currentDisplaying;

    bool canTrade;

    int invCount;

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");

        inventory = gameManager.GetComponent<InventorySystem>();
        objectSystem = gameManager.GetComponent<ObjectSystem>();

        modelRotator = GameObject.FindGameObjectWithTag("Insta3DManager").GetComponent<ModelRotator>();

        Refresh();
        UpdateInfo();
    }

    private void Update()
    {
        if(currentDisplaying != currentObject)
        {
            currentDisplaying = currentObject;

            modelRotator.data = currentDisplaying;

            UpdateInfo();
        }

        if(invCount != inventory.inventory.Count)
        {
            Refresh();
        }
    }

    void Refresh()
    {
        foreach (Transform child in inventoryHolder)
        {
            // Destroy each child
            Destroy(child.gameObject);
        }
        foreach (Transform child in stockHolder)
        {
            // Destroy each child
            Destroy(child.gameObject);
        }

        foreach (InventoryItem item in inventory.inventory) 
        {
            CreateNewTradeItem(item, inventoryHolder, TradeItemOrigin.Inventory);
        }
        foreach(InventoryItem item in objectSystem.inventory)
        {
            CreateNewTradeItem(item, stockHolder, TradeItemOrigin.Stock);
        }

        
    }

    void CreateNewTradeItem(InventoryItem item, Transform holder, TradeItemOrigin origin)
    {
        GameObject newTradeItem = Instantiate(tradeItemPF, holder);
        TradeItem tradeItem = newTradeItem.GetComponent<TradeItem>();

        tradeItem.origin = origin;
        tradeItem.item = item;
        tradeItem.UTrade = this;
    }

    public void AddToInventory(ObjectData data)
    {
        bool found = false;
        foreach(InventoryItem item in inventorySelected)
        {
            if(item.data == data)
            {
                found = true;
                InventoryItem target = null;
                foreach(InventoryItem invItem in inventory.inventory)
                {
                    if(invItem.data == data)
                    {
                        target = invItem;
                        
                    }
                }

                if(target != null)
                {
                    if(target.count > item.count)
                    {
                        item.count++;
                        UpdateInfo();
                        return;
                    }
                }
                
                
            }
        }
        if(!found)
        {
            InventoryItem newItem = new InventoryItem();
            newItem.data = data;
            newItem.count = 1;

            inventorySelected.Add(newItem);
        }

        UpdateInfo();

    }

    public void RemoveFromInventory(ObjectData data)
    {
        foreach (InventoryItem item in inventorySelected)
        {
            Debug.Log("a");
            if (item.data == data)
            {
                if(item.count > 0 )
                {
                    item.count--;
                    UpdateInfo();
                    return;
                }
                
            }
        }

        UpdateInfo();
    }

    public void AddToSystem(ObjectData data)
    {
        bool found = false;
        foreach (InventoryItem item in systemSelected)
        {
            
            if (item.data == data)
            {
                found = true;

                InventoryItem target = null;

                foreach (InventoryItem invItem in objectSystem.inventory)
                {
                    if (invItem.data == data)
                    {
                        target = invItem;
                        
                    }
                }

                if (target != null)
                {
                    if (target.count > item.count)
                    {
                        item.count++;
                        UpdateInfo();
                        return;
                    }
                }

                
            }
        }
        if(!found)
        {
            InventoryItem newItem = new InventoryItem();
            newItem.data = data;
            newItem.count = 1;

            systemSelected.Add(newItem);
        }

        UpdateInfo();

    }

    public void RemoveFromSystem(ObjectData data)
    {
        foreach (InventoryItem item in systemSelected)
        {

            
            if (item.data == data)
            {
                if (item.count > 0)
                {
                    item.count--;
                    UpdateInfo();
                    return;
                }

            }
        }

        UpdateInfo();
    }

    public TextMeshProUGUI title, description, buyP, sellP, scale, inventoryTotal, stockTotal;

    void UpdateInfo()
    {
        try
        {
            title.text = currentObject.title;
            description.text = currentObject.description;
            buyP.text = "• " + FormatNumber(currentObject.value) + " •";
            sellP.text = "• " + FormatNumber(currentObject.value * 0.7f) + " •";
            scale.text = "Scale " + FormatNumber(currentObject.scale);
            modelRotator.data = currentObject;

            inventoryTotal.text = FormatNumber(GetTotalScale(inventorySelected));
            stockTotal.text = FormatNumber(GetTotalScale(systemSelected));

            canTrade = GetTotalScale(inventorySelected) > GetTotalScale(systemSelected);

            if (canTrade)
            {
                inventoryTotal.color = Color.green;
            }
            else
            {
                inventoryTotal.color = Color.red;
            }
        }
        catch
        {
            // Its ok bri
        }

    }

    float GetTotalScale(List<InventoryItem> list)
    {
        float value = 0;

        foreach (InventoryItem item in list)
        {
            value += item.data.scale * item.count;
        }

        return value;
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

    void TradeItems(List<InventoryItem> source, List<InventoryItem> destination)
    {
        List<InventoryItem> itemsToRemove = new List<InventoryItem>();

        foreach (InventoryItem item in source)
        {
            bool found = false;

            // Check if the item exists in the destination inventory
            foreach (InventoryItem checkingItem in destination)
            {
                if (checkingItem.data == item.data)
                {
                    if(destination == inventory.inventory)
                    {
                        foreach(InventoryItem item2 in objectSystem.inventory)
                        {
                            if(item2.data == checkingItem.data)
                            {
                                item2.count -= item.count;
                            }
                        }
                    }
                    else
                    {
                        foreach (InventoryItem item2 in inventory.inventory)
                        {
                            if (item2.data == checkingItem.data)
                            {
                                item2.count -= item.count;
                            }
                        }
                    }

                    found = true;
                    checkingItem.count += item.count;  // Add count to destination
                    itemsToRemove.Add(item);  // Mark for removal from source
                    break;
                }
            }

            // If the item was not found, add it to the destination inventory
            if (!found)
            {
                InventoryItem newItem = new InventoryItem
                {
                    data = item.data,
                    count = item.count
                };
                destination.Add(newItem);
                if (destination == inventory.inventory)
                {
                    foreach (InventoryItem item2 in objectSystem.inventory)
                    {
                        if (item2.data == newItem.data)
                        {
                            item2.count -= item.count;
                        }
                    }
                }
                else
                {
                    foreach (InventoryItem item2 in inventory.inventory)
                    {
                        if (item2.data == newItem.data)
                        {
                            item2.count -= item.count;
                        }
                    }
                }
                itemsToRemove.Add(item);  // Mark for removal from source
            }
        }

        // Remove items from source inventory that have been transferred
        foreach (InventoryItem item in itemsToRemove)
        {
            source.Remove(item);
        }
    }

    public void ExecuteTrade()
    {
        if(canTrade)
        {
            // Transfer items from inventorySelected to objectSystem.inventory
            TradeItems(inventorySelected, objectSystem.inventory);

            // Transfer items from systemSelected to inventory.inventory
            TradeItems(systemSelected, inventory.inventory);
        }
        

        Refresh();
    }

}