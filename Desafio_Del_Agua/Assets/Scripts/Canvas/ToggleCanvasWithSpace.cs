using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCanvasWithSpace : MonoBehaviour
{
    public Image imageToFill;
    public Canvas canvasToShow;
    public float fillDuration = 360f; // 6 minutos 
    public float holdDuration = 0.5f; // Tiempo que se debe mantener el espacio oprimido

    private bool spacePressed = false;
    private float fillStartTime;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            spacePressed = true;
        }
        else
        {
            if (spacePressed)
            {
                canvasToShow.gameObject.SetActive(false);
                spacePressed = false;
            }
        }

        if (spacePressed && !canvasToShow.gameObject.activeSelf)
        {
            canvasToShow.gameObject.SetActive(true);
        }

        if (spacePressed)
        {
            float fillAmount = (Time.time - fillStartTime) / fillDuration;
            imageToFill.fillAmount = Mathf.Clamp01(fillAmount);
        }
    }

    public void StartFillingImage()
    {
        fillStartTime = Time.time;
        imageToFill.fillAmount = 0f;
    }
}
