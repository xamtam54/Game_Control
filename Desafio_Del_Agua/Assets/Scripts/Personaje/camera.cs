using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The target to follow (your player)
    public float distance = 5f; // Distance from the target
    public float height = 2f; // Height above the target
    public float rotationDamping = 10f; // Damping for camera rotation

    private float currentRotationAngle; // Current rotation angle of the camera

    void LateUpdate()
    {
        // Calculate desired rotation angle based on player's current rotation
        float desiredRotationAngle = target.eulerAngles.y;
        float desiredHeight = target.position.y + height;

        // Calculate current rotation angle and height using damping
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, desiredRotationAngle, rotationDamping * Time.deltaTime);
        Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

        // Calculate desired position based on rotation and distance
        Vector3 desiredPosition = target.position - currentRotation * Vector3.forward * distance;
        desiredPosition.y = desiredHeight;

        // Update camera position and rotation
        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
