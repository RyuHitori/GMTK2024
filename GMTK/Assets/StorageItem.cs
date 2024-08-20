using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItem : MonoBehaviour
{
    public ObjectData data;
    public MeshFilter meshFilter;

    private void Start()
    {
        meshFilter.mesh = data.mesh;
    }

    private void Update()
    {
        transform.localScale = data.storageScale;
        transform.position = transform.parent.position + data.storageOffset;
        transform.rotation = data.storageRotation;
    }
}
