using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopiItem : MonoBehaviour
{
    public ObjectData data;


    public Image icon;
    public TextMeshProUGUI buy, sell, title;

    public Shopi shopi;

    private void Start()
    {
        icon.sprite = data.icon;
        buy.text = FormatNumber(data.value);
        sell.text = FormatNumber(data.value * 0.7f);
        title.text = data.title;
    }

    public void OnClick()
    {
        shopi.currentObject = data;
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
