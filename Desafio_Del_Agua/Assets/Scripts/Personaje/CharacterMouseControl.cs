using UnityEngine;

public class CharacterMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensibilidad del ratón para la rotación del personaje
    public Transform playerBody; // Referencia al transform del cuerpo (o raíz) del jugador
    private float rotationY = 0f; // Rotación alrededor del eje Y
    private bool quieto;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro de la pantalla
        quieto = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            quieto = !quieto;
            if (quieto)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (!quieto)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            // Rotar el cuerpo del jugador horizontalmente según la entrada del ratón
            rotationY += mouseX;
            rotationY %= 360f;
            playerBody.localRotation = Quaternion.Euler(0f, rotationY, 0f);
        }
    }
}