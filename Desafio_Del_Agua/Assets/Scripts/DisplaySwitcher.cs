using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySwitcher : MonoBehaviour
{
    public Transform[] targetPositions; // Las posiciones objetivo a las que la c�mara se mover�
    public float transitionSpeed = 1.0f; // Velocidad de transici�n de la c�mara

    private Transform currentView;
    private Quaternion initialRotation; // Rotaci�n inicial de la c�mara

    void Start()
    {
        currentView = transform;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToTargetPosition(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToTargetPosition(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToTargetPosition(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchToTargetPosition(3);
        }
    }

    private void SwitchToTargetPosition(int index)
    {
        if (index >= 0 && index < targetPositions.Length)
        {
            currentView = targetPositions[index];
        }
    }

    private void LateUpdate()
    {
        // Transici�n suave de posici�n
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        // Calcular la rotaci�n deseada relativa a la rotaci�n inicial
        Quaternion desiredRotation = Quaternion.Inverse(initialRotation) * currentView.rotation;

        // Transici�n suave de rotaci�n
        transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation * desiredRotation, Time.deltaTime * transitionSpeed);
    }
}
