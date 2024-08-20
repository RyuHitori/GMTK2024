using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ObjectSystem : MonoBehaviour
{
    [Header("Database")]
    public List<InventoryItem> inventory;

    [Header("Summary")]
    public long value;
    public long unit;


    int listLastCount = 0;

    private void Start()
    {
        listLastCount = inventory.Count;

        //UpdateData();
    }
    private void Update()
    {
        //UpdateData();
    }

    public void RemoveItem(ObjectData data)
    {
        foreach (InventoryItem item in inventory)
        {
            
            if (item.data == data)
            {
                
                item.count--;
                return;
            }
                
            
        }
    }

    public void AddItem(ObjectData data)
    {
        foreach (InventoryItem item in inventory)
        {
            if (item.data == data)
            {
                item.count++;
                return;
            }
                
        }

        InventoryItem newItem = new InventoryItem();
        newItem.data = data;
        newItem.count = 0;

        inventory.Add(newItem);
    }

    public int GetItemCount(ObjectData data)
    {
        int count = 0;

        foreach (InventoryItem item in inventory)
        {
            if(item.data == data) count = item.count;
        }

        return count;
    }

}
