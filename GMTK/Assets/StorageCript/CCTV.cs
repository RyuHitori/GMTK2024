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

    // Rotation limits
    public float minVerticalRotation = -30f;
    public float maxVerticalRotation = 30f;
    public float minHorizontalRotation = -60f;
    public float maxHorizontalRotation = 60f;

    // Position limits
    public float minYPosition = 1f;
    public float maxYPosition = 10f;

    private float currentVerticalRotation = 0f;
    private float currentHorizontalRotation = 0f;

    void Update()
    {
        HandleRotation();
        HandleZoom();
        HandlePositionChange();
    }

    private void HandleRotation()
    {
        float horizontalRotation = 0f;
        float verticalRotation = 0f;

        // Handle horizontal rotation with 'A' and 'D' keys
        if (Input.GetKey(KeyCode.A))
        {
            horizontalRotation = -rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalRotation = rotationSpeed * Time.deltaTime;
        }

        // Handle vertical rotation with 'W' and 'S' keys
        if (Input.GetKey(KeyCode.W))
        {
            verticalRotation = -rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verticalRotation = rotationSpeed * Time.deltaTime;
        }

        // Update current rotations
        currentHorizontalRotation += horizontalRotation;
        currentVerticalRotation += verticalRotation;

        // Clamp the rotations within limits
        currentHorizontalRotation = Mathf.Clamp(currentHorizontalRotation, minHorizontalRotation, maxHorizontalRotation);
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, minVerticalRotation, maxVerticalRotation);

        // Apply the rotations
        transform.rotation = Quaternion.Euler(currentVerticalRotation, currentHorizontalRotation, 0f);
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

        // Zoom using the mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        camera.fieldOfView -= scroll * zoomSpeed * 0.5f;
        camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minFieldOfView, maxFieldOfView);
    }

    private void HandlePositionChange()
    {
        float verticalMovement = 0f;

        // Adjust camera position on the y-axis using the up and down arrow keys
        if (Input.GetKey(KeyCode.UpArrow))
        {
            verticalMovement = moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            verticalMovement = -moveSpeed * Time.deltaTime;
        }

        // Update and clamp the camera's y position
        Vector3 newPosition = camera.transform.position + new Vector3(0, verticalMovement, 0);
        newPosition.y = Mathf.Clamp(newPosition.y, minYPosition, maxYPosition);
        camera.transform.position = newPosition;
    }
}
