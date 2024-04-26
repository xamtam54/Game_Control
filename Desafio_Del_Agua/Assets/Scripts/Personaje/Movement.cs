using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f; // Walking speed of the player
    public float runSpeed = 10f; // Running speed of the player
    private float currentSpeed; // Current speed of the player

    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Determine whether the player is running or walking
        if (Input.GetKey(KeyCode.LeftShift) && verticalInput > 0) // Only run if moving forward
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        // Calculate the movement direction based on input
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the player in the calculated direction with the current speed
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime);
    }
}