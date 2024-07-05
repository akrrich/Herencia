using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FullMapController : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private bool isFullMapOpened;

    void Start()
    {
        isFullMapOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            if (isFullMapOpened)
                CloseFullMap();
            else
                OpenFullMap();
    }

    private void OpenFullMap()
    {
        isFullMapOpened = true;
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    private void CloseFullMap()
    {
        isFullMapOpened = false;
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}
