using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ButtonsInPauseMenu : MonoBehaviour
{
    [SerializeField] private Button buttonMainMenu;
    [SerializeField] private Button buttonExitGame;


    private PauseMenu pauseMenu;

    private AudioSource optionSound;

    private GameObject panel;


    private float counterForMenu = 0f;
    private float counterForExitGame = 0f;


    private bool timeForSoundMenu = false;
    private bool timeForExitGame = false;

    private bool isSoundPlaying = false;


    private void Start()
    {
        pauseMenu = GetComponent<PauseMenu>();

        optionSound = GetComponent<AudioSource>();

        panel = transform.Find("panel").gameObject;
    }


    private void Update()
    {
        if (timeForSoundMenu == true)
        {
            counterForMenu += Time.deltaTime;

            if (counterForMenu > 0.35f)
            {
                NotesController.CreateNewList();

                SceneManager.LoadScene("Menu");
            }
        }

        if (timeForExitGame == true) 
        {
            counterForExitGame += Time.deltaTime;

            if (counterForExitGame > 0.35f)
            {
                //UnityEditor.EditorApplication.isPlaying = false;

                Application.Quit();
            }
        }

        ButtonsStatus();
    }


    public void InteractWithResumeGameButton()
    {
        optionSound.Play();

        pauseMenu.ResumeGame();
    }

    public void InteractWithReturnMainMenuButton() 
    {
        if (isSoundPlaying == false)
        {
            optionSound.Play();

            isSoundPlaying = true;

            Time.timeScale = 1f;

            buttonMainMenu.transition = Selectable.Transition.None;
        }

        timeForSoundMenu = true;
    }

    public void InteractWithExitGameButton() 
    {
        optionSound.Play();

        Time.timeScale = 1f;

        buttonExitGame.transition = Selectable.Transition.None;

        timeForExitGame = true;
    }


    private void ButtonsStatus()
    {
        panel.SetActive(pauseMenu.GameInPause);
    }
}
