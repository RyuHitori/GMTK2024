using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    public float baseRotationSpeed = 100f;
    public Camera cam;
    public float baseZoomSpeed = 10f;
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

    float targetFOV;

    private void Start()
    {
        targetFOV = cam.fieldOfView;
    }

    void Update()
    {
        HandleRotation();
        HandleZoom();
        HandlePositionChange();
    }

    private void HandleRotation()
    {
        // Get camera's current FOV
        float fov = cam.fieldOfView;

        // Adjust rotation speed based on FOV
        float rotationSpeed = baseRotationSpeed * (fov / 60f); // Assuming 60 is the default FOV for your camera

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

        Quaternion targetRotation = Quaternion.Euler(currentVerticalRotation, currentHorizontalRotation, 0f);

        // Apply the rotations -> Smooth
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 20 * Time.deltaTime);
    }

    private void HandleZoom()
    {

        // Calculate zoom speed based on the current FOV
        float fov = cam.fieldOfView;
        float zoomSpeed = baseZoomSpeed * (fov / 60f); // Assuming 60 is the default FOV

        // Zoom using 'Q' and 'E' keys
        if (Input.GetKey(KeyCode.E))
        {
            targetFOV -= zoomSpeed * Time.deltaTime;
            targetFOV = Mathf.Clamp(targetFOV, minFieldOfView, maxFieldOfView);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            targetFOV += zoomSpeed * Time.deltaTime;
            targetFOV = Mathf.Clamp(targetFOV, minFieldOfView, maxFieldOfView);
        }

        // Smoothly interpolate camera FOV
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, 10 * Time.deltaTime);
    }

    private void HandlePositionChange()
    {
        float verticalMovement = 0f;

        // Adjust camera position on the y-axis using the up and down arrow keys ->  shift + control
        if (Input.GetKey(KeyCode.LeftShift))
        {
            verticalMovement = moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            verticalMovement = -moveSpeed * Time.deltaTime;
        }

        // Update and clamp the camera's y position
        Vector3 newPosition = cam.transform.position + new Vector3(0, verticalMovement, 0);
        newPosition.y = Mathf.Clamp(newPosition.y, minYPosition, maxYPosition);
        cam.transform.position = newPosition;
    }
}
