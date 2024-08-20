using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ObjectSystem : MonoBehaviour
{
    [Header("Database")]
    public List<TradeObject> objects;

    [Header("Summary")]
    public long value;
    public long unit;


    int listLastCount = 0;

    private void Start()
    {
        listLastCount = objects.Count;

        UpdateData();
    }
    private void Update()
    {
        UpdateData();
    }

    static long GetTotalUnit(List<TradeObject> data)
    {
        long unit = 0;

        foreach(TradeObject obj in data)
        {
            unit += obj.unit;
        }

        return unit;
    }

    static long GetTotalValue(List<TradeObject> data)
    {
        long value = 0;

        foreach(TradeObject obj in data)
        {
            value += obj.value;
        }

        return value;
    }

    public void UpdateData()
    {
        value = GetTotalValue(objects);
        unit = GetTotalUnit(objects);
    }
}