using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory")]
    public List<InventoryItem> inventory;

    [Header("Wallet")]
    public float money;

    private void Update()
    {

    }
    public void AddItem(ObjectData data)
    {
        foreach (InventoryItem item in inventory)
        {
            if (item.data == data) item.count++;
            return;
        }

        InventoryItem newItem = new InventoryItem(); 
        newItem.data = data; 
        newItem.count = 1;

        inventory.Add(newItem);

    }

    public void RemoveItem(ObjectData data)
    {
        foreach (InventoryItem item in inventory)
        {
            if (item.data == data) item.count--;
            return;
        }
    }

    public void AdjustMoney(float amount)
    {
        money += amount;
    }


    public int GetItemCount(ObjectData data)
    {
        int count = 0;
        foreach(InventoryItem item in inventory)
        {
            if(item.data == data) count = item.count;
        }

        return count;
    }
}

[Serializable]
public class InventoryItem
{
    public ObjectData data;
    public int count;
}
