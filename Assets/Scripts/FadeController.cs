using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Image panel;  // Asigna tu panel en el inspector
    public float duration = 2f;  // Duración del fade in

    private float startTime;
    private Color panelColor;

    void Start()
    {
        startTime = Time.time;
        panelColor = panel.color;
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;
        panelColor.a = Mathf.Lerp(1, 0, t);
        panel.color = panelColor;

        // Si el fade ha terminado, desactivar el script para optimización
        if (t >= 1)
        {
            enabled = false;
        }
    }
}
