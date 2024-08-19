using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public Camera camera;
    public float zoomSpeed = 10f;
    public float minFieldOfView = 15f;
    public float maxFieldOfView = 90f;

    void Start()
    {

    }

    void Update()
    {
        HandleRotation();
        HandleZoom();
    }

    private void HandleRotation()
    {
        float horizontalRotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float verticalRotation = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, horizontalRotation, Space.World);
        transform.Rotate(Vector3.right, -verticalRotation, Space.Self);
    }

    private void HandleZoom()
    {
        if (Input.GetKey(KeyCode.I))
        {
            camera.fieldOfView -= zoomSpeed * Time.deltaTime;
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minFieldOfView, maxFieldOfView);
        }
        if (Input.GetKey(KeyCode.O))
        {
            camera.fieldOfView += zoomSpeed * Time.deltaTime;
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minFieldOfView, maxFieldOfView);
        }
    }
}
