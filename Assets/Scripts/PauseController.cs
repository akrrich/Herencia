using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
        pauseMenu.SetActive(isActive);
        settingsMenu.SetActive(false);
    }

    public void ShowSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void HideSettings()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
