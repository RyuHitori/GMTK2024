using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shopi : MonoBehaviour
{
    public ObjectData currentObject;
    public TextMeshProUGUI title, description, buyP, sellP, scale;

    ObjectData currentDisplaying;

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
        buyP.text = "• " + currentObject.value.ToString() + " •";
        sellP.text = "• " + (currentObject.value * 0.7f).ToString() + " •";
        scale.text = "Scale " + currentObject.scale.ToString();
    }
}
