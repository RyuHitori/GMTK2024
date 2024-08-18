using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        public GameObject prefab;
        public MeshRenderer meshRenderer;
        public Material material;
        public Texture texture;
        public float size;
    }

    public ItemData[] items; 
    public Transform box;
    public Vector3 cornerOffset = new Vector3(0.5f, 0.5f, 0.5f);
    public float spacing = 0.5f;

    private int currentItemIndex = 0;
    private Vector3 currentSpawnPosition;

    void Start()
    {
        // Calculate the size of each item based on its volume
        foreach (var item in items)
        {
            item.size = CalculateItemSize(item.prefab);
        }

        // Sort items based on size (smallest first)
        System.Array.Sort(items, (a, b) => a.size.CompareTo(b.size));

        // Start spawning from the top corner of the box
        currentSpawnPosition = GetBoxTopCorner(box) + cornerOffset;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentItemIndex < items.Length)
        {
            SpawnItem(items[currentItemIndex]);
            currentItemIndex++;
        }
    }

    void SpawnItem(ItemData itemData)
    {
        GameObject item = Instantiate(itemData.prefab, currentSpawnPosition, Quaternion.identity);

        MeshRenderer renderer = item.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = itemData.material;
            renderer.material.mainTexture = itemData.texture;
        }

        currentSpawnPosition += new Vector3(spacing, 0, spacing);
    }

    Vector3 GetBoxTopCorner(Transform boxTransform)
    {
        Renderer renderer = boxTransform.GetComponent<Renderer>();
        if (renderer != null)
        {
            Bounds bounds = renderer.bounds;
            Vector3 topCorner = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
            return topCorner;
        }
        else
        {
            Vector3 scale = boxTransform.localScale;
            Vector3 topCorner = new Vector3(boxTransform.position.x - (scale.x / 2f),
                                            boxTransform.position.y + (scale.y / 2f),
                                            boxTransform.position.z - (scale.z / 2f));
            return topCorner;
        }
    }

    float CalculateItemSize(GameObject item)
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
