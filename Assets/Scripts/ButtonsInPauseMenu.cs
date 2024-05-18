using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsInPauseMenu : MonoBehaviour
{
    private PauseMenu pauseMenu;

    private AudioSource optionSound;

    private GameObject panel;


    private void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();

        optionSound = GetComponent<AudioSource>();

        panel = transform.Find("panel").gameObject;
    }


    private void Update()
    {
        ButtonsStatus();
    }

    public void InteractWithResumeGameButton()
    {
        pauseMenu.ResumeGame();
    }

    public void InteractWithReturnMainMenuButton() 
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }

    public void InteractWithExitGameButton() 
    {
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }


    private void ButtonsStatus()
    {
        panel.SetActive(pauseMenu.GameInPause);
    }
}
