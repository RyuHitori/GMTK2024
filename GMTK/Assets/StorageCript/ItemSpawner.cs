using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

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
    public Vector3 cornerOffset = new Vector3(0f, 0f, 0f);
    //public float spacing = 0f;

    private int currentItemIndex = 0;
    private Vector3 currentSpawnPosition;

    void Start()
    {
        foreach (var item in items)
        {
            item.size = CalculateItemSize(item.prefab);
        }
        System.Array.Sort(items, (a, b) => a.size.CompareTo(b.size));
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
        Collider collider = item.GetComponent<Collider>();
        Debug.Log(currentItemIndex);
        Bounds bounds = collider.bounds;
        Debug.Log(bounds.size.x);
        Debug.Log(bounds.size.z);
        float X = bounds.size.x * 2;
        float Z = bounds.size.z * 2;
        Debug.Log(X);
        Debug.Log(Z);
        MeshRenderer renderer = item.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = itemData.material;
            renderer.material.mainTexture = itemData.texture;
        }

        //currentSpawnPosition += new Vector3(spacing, 0, spacing);
        if (currentItemIndex + 1 < items.Length)
        {
            currentSpawnPosition += new Vector3(X, 0, Z);
        }
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
