using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FullMapController : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private bool isFullMapOpened;

    public bool IsFullMapOpened { get => isFullMapOpened;}

    void Start()
    {
        isFullMapOpened = false;
    }

    private void OpenFullMap()
    {
        isFullMapOpened = true;
        panel.SetActive(true);
    }

    private void CloseFullMap()
    {
        isFullMapOpened = false;
        panel.SetActive(false);
    }

    public void ToggleActive()
    {
        SetActive(!isFullMapOpened);
    }

    public void SetActive(bool v)
    {
        if (v)
            OpenFullMap();
        else
            CloseFullMap();
    }
}
