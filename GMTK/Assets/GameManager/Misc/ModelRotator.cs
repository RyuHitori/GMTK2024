using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    public Window window;

    public float rotationSpeed = 100f; // Speed of rotation
    public Transform target; // The model to rotate
    public Transform cameraTransform; // Reference to the camera's transform

    public bool rotating;

    void Update()
    {
        // Check if window is focused
        if (window != null)
        {
            rotating = window.focusing;
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Insta3D") != null)
            {
                window = GameObject.FindGameObjectWithTag("Insta3D").GetComponent<Window>();
            }
        }

        if (rotating)
        {
            if (Input.GetMouseButton(1))
            {
                // Get mouse input
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                // Rotate the object relative to the camera's local axes
                target.Rotate(cameraTransform.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
                target.Rotate(cameraTransform.right, mouseY * rotationSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}
