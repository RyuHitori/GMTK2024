using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    InventorySystem inventory;
    public TextMeshProUGUI moneyText;


    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventorySystem>();
    }

    private void Update()
    {
        // Convert inventory amounts to formatted strings
        string big = inventory.big.ToString("N0"); // Format big part with thousands separators
        string small = inventory.small.ToString("D2"); // Ensure small part has two digits

        // Combine formatted big and small parts into a single money string
        string money = big + "." + small;

        // Update the text component to display the formatted money value
        moneyText.text = "• " + money + " •";
    }
}
