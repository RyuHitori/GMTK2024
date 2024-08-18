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
        public Vector3 position;
        public Vector3 scale = Vector3.one;
        public bool CusPos = false;
        public bool CusScale = false;
        public Material material;
        public Texture texture;
    }

    public ItemData[] items;
    public Transform box;
    public Vector3 cornerOffset = new Vector3(0f, 0f, 0f);

    private int currentItemIndex = 0;
    private Vector3 currentSpawnPosition;

    void Start()
    {
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
        Vector3 spawnPosition;

        if (itemData.CusPos)
        {
            spawnPosition = itemData.position;
        }
        else
        {
            spawnPosition = currentSpawnPosition;
        }

        GameObject item = Instantiate(itemData.prefab, spawnPosition, Quaternion.identity);
        if (itemData.CusScale)
        {
            item.transform.localScale = itemData.scale;
        }
        Collider collider = item.GetComponent<Collider>();

        MeshRenderer renderer = item.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = itemData.material;
            renderer.material.mainTexture = itemData.texture;
        }
        if (!itemData.CusPos && currentItemIndex + 1 < items.Length)
        {
            Bounds bounds = collider.bounds;
            float X = bounds.size.x;
            float Z = bounds.size.z;
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
}
