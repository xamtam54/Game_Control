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
        if (Input.GetKeyDown(KeyCode.Alpha1) && targetPositions[0] != null)
        {
            SwitchToTargetPosition(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && targetPositions[1] != null)
        {
            SwitchToTargetPosition(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && targetPositions[2] != null)
        {
            SwitchToTargetPosition(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && targetPositions[3] != null)
        {
            SwitchToTargetPosition(3);
        }/*
        if (Input.GetKeyDown(KeyCode.Space) && targetPositions[4] != null)
        {
            HideAllTargetPositions();
            //aqui se envia a la camara del jugador
        }*/
    }

    private void SwitchToTargetPosition(int index)
    {
        if (index >= 0 && index < targetPositions.Length)
        {
            currentView = targetPositions[index];
            HideAllTargetPositions();
            targetPositions[index].gameObject.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
        Quaternion desiredRotation = Quaternion.Inverse(initialRotation) * currentView.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation * desiredRotation, Time.deltaTime * transitionSpeed);
    }

    void HideAllTargetPositions()
    {
        foreach (Transform target in targetPositions)
        {
            target.gameObject.SetActive(false);
        }
    }
}
