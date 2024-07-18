using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    private AudioSource soundOption;


    private void Start()
    {
        soundOption = GetComponent<AudioSource>();
    }

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
        soundOption.Play();
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
