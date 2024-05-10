using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float walkSpeed = 5f; // Walking speed of the character
    public float runSpeed = 10f; // Running speed of the character

    private Animator animator; // Reference to the Animator component
    private bool run = false; // Flag to track whether the character is running
    private bool quieto;

    void Start()
    {
        // Get the Animator component attached to the player GameObject
        animator = GetComponent<Animator>();
        quieto = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { quieto = !quieto; }

        if (!quieto)
        {
            // Get input from the player
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Determine whether the player is running or walking
            float speed = run ? runSpeed : walkSpeed;

            // Calculate movement direction
            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            // Move the character in the calculated direction
            transform.Translate(movement * speed * Time.deltaTime);

            // Check if the player is moving forward, backward, or sideways
            bool walking = Mathf.Abs(verticalInput) > 0.1f || Mathf.Abs(horizontalInput) > 0.1f;

            // Set animation parameters based on player movement
            animator.SetBool("walking", walking && !run);
            animator.SetBool("run", run);

            // Set the "idle" parameter in the Animator controller when the player stops moving
            animator.SetBool("idle", !walking && !run);

            // Check if the player presses the Shift key to start running
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                run = true;
            }

            // Check if the player releases the Shift key to stop running
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                run = false;
            }

            // Check if the player presses the left mouse button to trigger hit animation
            if (Input.GetMouseButtonDown(0))
            {
                // Trigger the "hit" animation
                animator.SetTrigger("hit");
            }

            // Check if the player presses the '6' key (you can change this key according to your setup)
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                // Trigger the "Die" animation
                animator.SetTrigger("dying");
            }
            // 
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                // Trigger the "dance" animation
                animator.SetTrigger("dance");
            }
        }
    }
}
