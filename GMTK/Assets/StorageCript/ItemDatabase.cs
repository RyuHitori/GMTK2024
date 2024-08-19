using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public MeshRenderer[] meshRenderers; // Array to hold different mesh renderers
    private int currentIndex = 0; // To track the current mesh renderer

    void Start()
    {
        // Ensure the first mesh renderer is active and others are inactive
        UpdateMeshRenderer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeMeshRenderer(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeMeshRenderer(1);
        }
    }

    private void ChangeMeshRenderer(int direction)
    {
        // Update the index based on direction (1 for next, -1 for previous)
        currentIndex += direction;

        // Wrap the index if it goes out of bounds
        if (currentIndex < 0)
        {
            currentIndex = meshRenderers.Length - 1;
        }
        else if (currentIndex >= meshRenderers.Length)
        {
            currentIndex = 0;
        }

        UpdateMeshRenderer();
    }

    private void UpdateMeshRenderer()
    {
        // Deactivate all mesh renderers first
        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = false;
        }

        // Activate the current mesh renderer
        meshRenderers[currentIndex].enabled = true;
    }
}