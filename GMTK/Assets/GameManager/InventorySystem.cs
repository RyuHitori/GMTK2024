using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Header("Inventory")]
    public List<TradeObject> inventory;

    [Header("Wallet")]
    public long money;

}
