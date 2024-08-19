using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCameraController : MonoBehaviour
{
    public Transform target; 
    public float rotationSpeed = 5.0f; 
    public float zoomSpeed = 4.0f; 
    public float minZoom = 5.0f; 
    public float maxZoom = 20.0f; 

    public float currentZoom = 10.0f;
    private float yaw = 0.0f; 
    private float pitch = 0.0f;



    void Update()
    {
        

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scrollInput * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        
        if (Input.GetMouseButton(0)) 
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            pitch = Mathf.Clamp(pitch, -89f, 89f); 
        }

        
        Vector3 direction = new Vector3(0, 0, -currentZoom);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position + rotation * direction;

        
        transform.LookAt(target.position);
    }
}
