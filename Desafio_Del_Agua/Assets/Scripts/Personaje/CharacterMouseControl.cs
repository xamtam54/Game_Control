using UnityEngine;

public class CharacterMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Sensibilidad del rat�n para la rotaci�n del personaje
    public Transform playerBody; // Referencia al transform del cuerpo (o ra�z) del jugador
    private float rotationY = 0f; // Rotaci�n alrededor del eje Y
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

            // Rotar el cuerpo del jugador horizontalmente seg�n la entrada del rat�n
            rotationY += mouseX;
            rotationY %= 360f;
            playerBody.localRotation = Quaternion.Euler(0f, rotationY, 0f);
        }
    }
}