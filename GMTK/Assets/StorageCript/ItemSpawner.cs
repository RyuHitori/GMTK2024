using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Array of item prefabs to be spawned
    public Transform box; // The box on which items will be spawned
    public Vector3 cornerOffset = new Vector3(0.5f, 0.5f, 0.5f); // Offset to ensure items are placed inside the box
    public float spacing = 0.5f; // Space between items along the X-axis

    private int currentItemIndex = 0; // Tracks the index of the next item to be spawned
    private Vector3 currentSpawnPosition;

    void Start()
    {
        // Start from the corner of the box
        currentSpawnPosition = GetBoxCorner(box) + cornerOffset;

        // Sort items based on their size (volume)
        System.Array.Sort(itemPrefabs, (a, b) =>
        {
            float volumeA = GetItemVolume(a);
            float volumeB = GetItemVolume(b);
            return volumeA.CompareTo(volumeB);
        });
    }

    void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E) && currentItemIndex < itemPrefabs.Length)
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        // Instantiate the item at the current spawn position
        GameObject item = Instantiate(itemPrefabs[currentItemIndex], currentSpawnPosition, Quaternion.identity);

        // Update the spawn position for the next item
        currentSpawnPosition += new Vector3(spacing, 0, spacing);

        // Move to the next item in the array
        currentItemIndex++;
    }

    Vector3 GetBoxCorner(Transform boxTransform)
    {
        // Calculate the corner position of the box
        Renderer renderer = boxTransform.GetComponent<Renderer>();
        if (renderer != null)
        {
            Bounds bounds = renderer.bounds;
            Vector3 corner = bounds.center - bounds.extents;
            return corner;
        }
        else
        {
            // Fallback to using scale if no Renderer is found
            Vector3 scale = boxTransform.localScale;
            Vector3 corner = boxTransform.position - (scale / 2f);
            return corner;
        }
    }

    float GetItemVolume(GameObject item)
    {
        // Calculate the volume based on the item's bounds or scale
        Collider collider = item.GetComponent<Collider>();
        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            return bounds.size.x * bounds.size.y * bounds.size.z;
        }
        else
        {
            // Fallback to scale if no collider is found
            Vector3 scale = item.transform.localScale;
            return scale.x * scale.y * scale.z;
        }
    }
}
