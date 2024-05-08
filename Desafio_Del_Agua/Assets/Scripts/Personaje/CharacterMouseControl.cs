using UnityEngine;

public class CharacterMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Mouse sensitivity for character rotation
    public Transform playerBody; // Reference to the player's body (or root) transform
    private float rotationY = 0f; // Rotation around Y-axis

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // Rotate the player's body horizontally based on mouse input
        rotationY += mouseX;
        rotationY %= 360f;
        playerBody.localRotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
