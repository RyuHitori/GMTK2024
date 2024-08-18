using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public Transform dropPoint; 
    public GameObject itemPrefab; 
    public float cooldownTime = 0.5f; 

    private float nextDropTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= nextDropTime)
        {
            DropItem();
            nextDropTime = Time.time + cooldownTime; 
        }
    }

    void DropItem()
    {
        
        GameObject item = Instantiate(itemPrefab, dropPoint.position, Quaternion.identity);
        Rigidbody rb = item.GetComponent<Rigidbody>();

        
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
