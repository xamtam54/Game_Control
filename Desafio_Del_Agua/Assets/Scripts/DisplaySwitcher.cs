using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySwitcher : MonoBehaviour
{
    public Transform[] targetPositions; // Las posiciones objetivo a las que la cámara se moverá
    public float transitionSpeed = 1.0f; // Velocidad de transición de la cámara

    private Transform currentView;
    private Quaternion initialRotation; // Rotación inicial de la cámara

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
        // Transición suave de posición
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        // Calcular la rotación deseada relativa a la rotación inicial
        Quaternion desiredRotation = Quaternion.Inverse(initialRotation) * currentView.rotation;

        // Transición suave de rotación
        transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation * desiredRotation, Time.deltaTime * transitionSpeed);
    }
}
