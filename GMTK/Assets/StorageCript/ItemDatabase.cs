using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [System.Serializable]
    public class ObjectData
    {
        public GameObject model;        // The 3D model prefab
        public float value;             // Custom value associated with the object
        public Vector3 scale = Vector3.one; // Scale of the object
        public Color colorCode = Color.white; // Color code for the object
    }

    public ObjectData[] objects; // Array of ObjectData

    private int currentIndex = 0; // Current object index
    private GameObject currentObject; // The currently displayed object

    void Start()
    {
        if (objects.Length > 0)
        {
            ShowObject(0); // Show the first object at the start
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowPreviousObject();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ShowNextObject();
        }
    }

    void ShowObject(int index)
    {
        if (currentObject != null)
        {
            Destroy(currentObject); // Destroy the current object before showing the new one
        }

        ObjectData objectData = objects[index];
        currentObject = Instantiate(objectData.model, transform.position, Quaternion.identity);
        currentObject.transform.localScale = objectData.scale;

        Renderer renderer = currentObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = objectData.colorCode; // Apply the color code
        }
        Rigidbody rb = currentObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Set isKinematic to true if Rigidbody is present
        }
    }

    void ShowPreviousObject()
    {
        currentIndex = (currentIndex - 1 + objects.Length) % objects.Length;
        ShowObject(currentIndex);
    }

    void ShowNextObject()
    {
        currentIndex = (currentIndex + 1) % objects.Length;
        ShowObject(currentIndex);
    }
}