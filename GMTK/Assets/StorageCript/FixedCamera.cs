using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    public Transform cube; 
    public Vector3 cornerOffset; 
    public Vector3 rotation;

    void Start()
    {
        transform.rotation = Quaternion.Euler(rotation);
        if (cube != null)
        {
            
            Vector3 cubeCorner = GetCubeCorner(cube);
            transform.position = cubeCorner + cornerOffset;
            //transform.LookAt(cube.position);
        }
    }

    Vector3 GetCubeCorner(Transform cubeTransform)
    {
        Renderer renderer = cubeTransform.GetComponent<Renderer>();
        if (renderer != null)
        {
            Bounds bounds = renderer.bounds;
            Vector3 corner = bounds.center - bounds.extents;
            return corner;
        }
        else
        {
            Vector3 scale = cubeTransform.localScale;
            Vector3 corner = cubeTransform.position - (scale / 2f);
            return corner;
        }
    }
}
