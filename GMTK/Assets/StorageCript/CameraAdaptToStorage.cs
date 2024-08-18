using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdaptToStorage : MonoBehaviour
{
    public Camera mainCamera; 
    public Transform storageObject; 
    public float padding = 1.2f; 
    public float heightAboveStorage = 10.0f; 

    void Start()
    {
        AdaptCameraToStorage();
    }

    void AdaptCameraToStorage()
    {
        
        Bounds bounds = CalculateBounds(storageObject);

       
        Vector3 center = bounds.center;
        float maxSize = Mathf.Max(bounds.size.x, bounds.size.z);

        
        Vector3 newCameraPosition = new Vector3(center.x, heightAboveStorage, center.z);
        mainCamera.transform.position = newCameraPosition;

        
        if (mainCamera.orthographic)
        {
            mainCamera.orthographicSize = maxSize / 2 * padding;
        }
        else 
        {
            float distance = heightAboveStorage;
            float fov = mainCamera.fieldOfView;
            float aspectRatio = mainCamera.aspect;

            
            float requiredFOV = Mathf.Atan2(maxSize * padding, distance) * Mathf.Rad2Deg;
            mainCamera.fieldOfView = Mathf.Clamp(requiredFOV, 1, 179);
        }

       
        mainCamera.transform.LookAt(center);
    }

    Bounds CalculateBounds(Transform target)
    {
        Bounds bounds = new Bounds(target.position, Vector3.zero);

        Renderer[] renderers = target.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }
}
