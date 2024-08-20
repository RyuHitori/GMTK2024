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
        public bool CusRot = false;
        public Vector3 rotation = Vector3.zero;
        public Material material;
        public Texture texture;
    }

    public ItemData[] items;
    public Transform box;
    public Vector3 cornerOffset = new Vector3(0f, 0f, 0f);

    private Vector3 currentSpawnPosition;
    private GameObject[] spawnedItems;

    void Start()
    {
        currentSpawnPosition = GetBoxTopCorner(box) + cornerOffset;
        spawnedItems = new GameObject[items.Length];
    }

    void Update()
    {
        for (int i = 0; i < items.Length; i++)
        {
            KeyCode alphaKey = KeyCode.Alpha1 + i;
            KeyCode keypadKey = KeyCode.Keypad1 + i;

            if (Input.GetKeyDown(alphaKey) || Input.GetKeyDown(keypadKey))
            {
                if (spawnedItems[i] == null)
                {
                    SpawnItem(items[i], i);
                }
                else
                {
                    DeleteItem(i);
                }
            }
        }
    }

    void SpawnItem(ItemData itemData, int index)
    {
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        if (itemData.CusPos)
        {
            spawnPosition = itemData.position;
        }
        else
        {
            spawnPosition = currentSpawnPosition;
        }

        if (itemData.CusRot)
        {
            spawnRotation = Quaternion.Euler(itemData.rotation);
        }
        else
        {
            spawnRotation = Quaternion.identity;
        }

        GameObject item = Instantiate(itemData.prefab, spawnPosition, spawnRotation);

        if (itemData.CusScale)
        {
            item.transform.localScale = itemData.scale;
        }

        //Collider collider = item.GetComponent<Collider>();

        MeshRenderer renderer = item.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = itemData.material;
            renderer.material.mainTexture = itemData.texture;
        }

        //if (!itemData.CusPos && index + 1 < items.Length)
        //{
        //    Bounds bounds = collider.bounds;
        //    float X = bounds.size.x;
        //    float Z = bounds.size.z;
        //    currentSpawnPosition += new Vector3(X, 0, Z);
        //}

        spawnedItems[index] = item;
    }

    void DeleteItem(int index)
    {
        if (spawnedItems[index] != null)
        {
            Destroy(spawnedItems[index]);
            spawnedItems[index] = null;
            Debug.Log("deleted");
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
