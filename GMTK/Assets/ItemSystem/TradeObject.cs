using UnityEngine;

public class TradeObject : MonoBehaviour
{
    [Header("Data")]
    public ObjectData data;

    public string title => data.title;
    public string description => data.description;
    public long value => data.value;
    public long unit => data.scale;

    private void Start()
    {
        transform.name = title;
    }


}