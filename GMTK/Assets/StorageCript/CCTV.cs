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
    public float moveSpeed = 5f;

    void Start()
    {

    }

    void Update()
    {
        HandleRotation();
        HandleZoom();
        HandlePositionChange();
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
        // Zoom using 'I' and 'O' keys
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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        camera.fieldOfView -= scroll * zoomSpeed * 1/2;
        camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minFieldOfView, maxFieldOfView);
        
    }

    private void HandlePositionChange()
    {
        float verticalMovement = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalMovement = moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalMovement = -moveSpeed * Time.deltaTime;
        }

        camera.transform.position += new Vector3(0, verticalMovement, 0);
    }
}
