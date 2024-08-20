using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    InventorySystem inventory;
    public TextMeshProUGUI moneyText;

    float currentNumber;


    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventorySystem>();

        currentNumber = inventory.money;
    }

    private void Update()
    {

        // Update the text component to display the formatted money value
        float targetNumber = inventory.money;

        currentNumber = Mathf.Lerp(currentNumber, targetNumber, 30 * Time.deltaTime);

        moneyText.text = "• " + currentNumber.ToString("N2") + " •";
    }
}
