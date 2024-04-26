using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMouseControl : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the character
    public float rotationSpeed = 100f; // Rotation speed of the character
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void FixedUpdate()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");

        // Rotate the character based on mouse input
        Vector3 rotation = new Vector3(0f, mouseX * rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        // Get movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the character in the calculated direction
        rb.MovePosition(rb.position + transform.TransformDirection(movement) * moveSpeed * Time.fixedDeltaTime);
    }
}