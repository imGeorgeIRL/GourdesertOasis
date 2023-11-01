using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed;
    public float boundaryDistance = 20.0f;
    public float smoothFactor = 2.0f; // Adjust this for smoother movement.

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Check if the mouse is near the screen edges within the specified boundary.
        if (
            mousePosition.x < boundaryDistance ||
            mousePosition.x > Screen.width - boundaryDistance ||
            mousePosition.y < boundaryDistance ||
            mousePosition.y > Screen.height - boundaryDistance
        )
        {
            // Calculate normalized direction towards the mouse position.
            Vector3 direction = new Vector3(
                Mathf.Clamp((mousePosition.x - Screen.width / 2) / (Screen.width / 2), -1, 1),
                Mathf.Clamp((mousePosition.y - Screen.height / 2) / (Screen.height / 2), -1, 1),
                0
            );

            // Calculate the desired camera position within the boundary.
            Vector3 targetPosition = transform.position + direction * cameraSpeed * Time.deltaTime;

            // Clamp the camera position to stay within the boundary.
            targetPosition.x = Mathf.Clamp(targetPosition.x, -boundaryDistance, boundaryDistance);
            targetPosition.y = Mathf.Clamp(targetPosition.y, -boundaryDistance, boundaryDistance);

            // Smoothly move the camera towards the target position.
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);
        }
    }

}
