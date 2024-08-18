using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Test : MonoBehaviour
{
    public GameObject floor;
    
    void Update()
    {
        Collider collider = floor.GetComponent<Collider>();
        Bounds bounds = collider.bounds;
        Debug.Log(bounds.size.x);
        Debug.Log(GetItemVolume(floor));
    }

    float GetItemVolume(GameObject item)
    {
        Collider collider = item.GetComponent<Collider>();
        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            return bounds.size.x * bounds.size.y * bounds.size.z;
        }
        else
        {
            Vector3 scale = item.transform.localScale;
            return scale.x * scale.y * scale.z;
        }
    }
}
