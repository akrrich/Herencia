using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsInPauseMenu : MonoBehaviour
{
    private PauseMenu pauseMenu;

    private AudioSource optionSound;

    private GameObject buttonPauseGame;
    private GameObject buttonResumeGame;
    private GameObject buttonReturnMainMenu;
    private GameObject buttonExitGame;


    private void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();

        optionSound = GetComponent<AudioSource>();

        buttonPauseGame = transform.Find("Button Pause Game").gameObject;
        buttonResumeGame = transform.Find("Button Resume Game").gameObject;
        buttonReturnMainMenu = transform.Find("Button Main Menu").gameObject;
        buttonExitGame = transform.Find("Button Exit").gameObject;
    }


    private void Update()
    {
        ButtonsStatus();
    }


    public void InteractWithPauseButton()
    {
        pauseMenu.PauseGame();
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
        if (pauseMenu.GameInPause == false)
        {
            buttonPauseGame.SetActive(true);
            buttonResumeGame.SetActive(false);
            buttonReturnMainMenu.SetActive(false);
            buttonExitGame.SetActive(false);
        }

        else
        {
            buttonPauseGame.SetActive(false);
            buttonResumeGame.SetActive(true);
            buttonReturnMainMenu.SetActive(true);
            buttonExitGame.SetActive(true);
        }
    }
}
