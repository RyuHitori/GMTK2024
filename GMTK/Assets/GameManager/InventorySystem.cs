using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory")]
    public List<TradeObject> inventory;

    [Header("Wallet")]
    public long big;
    public int small;

    private void Update()
    {
        if(small > 100)
        {
            big++;
            small -= 100;
        }
    }

}
