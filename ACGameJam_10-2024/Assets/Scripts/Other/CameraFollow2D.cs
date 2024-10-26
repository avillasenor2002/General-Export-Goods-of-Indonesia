using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private Transform target; // The GameObject to keep on screen
    [SerializeField] private Transform leftBound; // The left boundary point
    [SerializeField] private Transform rightBound; // The right boundary point
    [SerializeField] private Transform topBound; // The top boundary point
    [SerializeField] private Transform bottomBound; // The bottom boundary point

    [SerializeField] private float pixelsPerStep = 1f; // Number of pixels to move each frame
    [SerializeField] private float edgeBuffer = 0.1f; // Buffer percentage of the camera's view near the edges

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (target != null)
        {
            // Get the target's position in the viewport
            Vector3 viewportPosition = cam.WorldToViewportPoint(target.position);

            // Calculate the desired position based on the target's position
            Vector3 desiredPosition = transform.position;

            // Check if the target is near the edges of the camera's view and adjust the desired position
            if (viewportPosition.x < edgeBuffer) // Left edge
            {
                desiredPosition.x = target.position.x - (cam.orthographicSize * cam.aspect);
            }
            else if (viewportPosition.x > 1f - edgeBuffer) // Right edge
            {
                desiredPosition.x = target.position.x + (cam.orthographicSize * cam.aspect);
            }

            if (viewportPosition.y < edgeBuffer) // Bottom edge
            {
                desiredPosition.y = target.position.y - cam.orthographicSize;
            }
            else if (viewportPosition.y > 1f - edgeBuffer) // Top edge
            {
                desiredPosition.y = target.position.y + cam.orthographicSize;
            }

            // Clamp the desired position within the bounds
            float cameraHalfWidth = cam.orthographicSize * cam.aspect;
            float cameraHalfHeight = cam.orthographicSize;

            float clampedX = Mathf.Clamp(desiredPosition.x, leftBound.position.x + cameraHalfWidth, rightBound.position.x - cameraHalfWidth);
            float clampedY = Mathf.Clamp(desiredPosition.y, bottomBound.position.y + cameraHalfHeight, topBound.position.y - cameraHalfHeight);

            Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);

            // Move the camera pixel by pixel using Mathf.MoveTowards
            float step = pixelsPerStep * Time.deltaTime;
            float newX = Mathf.MoveTowards(transform.position.x, clampedPosition.x, step);
            float newY = Mathf.MoveTowards(transform.position.y, clampedPosition.y, step);

            // Update the camera's position with pixel-by-pixel movement
            transform.position = new Vector3(newX, newY, transform.position.z);
        }
    }
}
