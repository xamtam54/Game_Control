using UnityEngine;

public class KeyboardInputToggle : MonoBehaviour
{
    private bool keyboardLocked = false; // Flag to indicate if keyboard input is locked

    void Update()
    {
        // Toggle keyboard input lock on pressing the "E" key
        if (Input.GetKeyDown(KeyCode.E))
        {
            keyboardLocked = !keyboardLocked;
            Debug.Log("Keyboard input lock toggled: " + keyboardLocked);
        }

        // Check if keyboard input is locked
        if (keyboardLocked)
        {
            // Check if any allowed keys (1, 2, 3, 4, Space) are pressed
            if (Input.GetKeyDown(KeyCode.Alpha1) ||
                Input.GetKeyDown(KeyCode.Alpha2) ||
                Input.GetKeyDown(KeyCode.Alpha3) ||
                Input.GetKeyDown(KeyCode.Alpha4) ||
                Input.GetKeyDown(KeyCode.Space))
            {
                // Allow the key press
                Debug.Log("Allowed key pressed!");
            }
            else
            {
                // Block all other key presses
                Debug.Log("Keyboard input locked!");
                BlockAllKeys();
            }
        }
        else
        {
            // Allow all key presses when keyboard input is not locked
            Debug.Log("Keyboard input unlocked!");
        }
    }

    void BlockAllKeys()
    {
        // Block all keys by clearing the input buffer
        Input.ResetInputAxes();
    }
}
