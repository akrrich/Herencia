using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private AudioSource optionSound;
    [SerializeField] private Button buttonPlay;

    [SerializeField] private GameObject allButtons;
    [SerializeField] private GameObject panelSettings;

    private float counterForPlay = 0;

    private bool timeForSoundPlay = false;
    private bool isSoundPlaying = false;

    private bool inOptionsMode = false;


    private void Update()
    {
        if (timeForSoundPlay == true)
        {
            counterForPlay += Time.deltaTime;

            if (counterForPlay > 0.35)
            {
                NotesController.CreateNewList();
                SceneManager.LoadScene("Laboratorio2");
            }
        }

        PanelAndButtonsStatus();
    }


    public void PlayButton()
    {
        if (isSoundPlaying == false)
        {
            optionSound.Play();
            isSoundPlaying = true;

            buttonPlay.transition = Selectable.Transition.None;
        }

        timeForSoundPlay = true;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SettingsButton()
    {
        inOptionsMode = true;
        optionSound.Play();
    }

    public void BackButton()
    {
        inOptionsMode = false;
        optionSound.Play();
    }

    private void PanelAndButtonsStatus()
    {
        if (inOptionsMode == true)
        {
            allButtons.SetActive(!inOptionsMode);
            panelSettings.SetActive(inOptionsMode);
        }

        else
        {
            allButtons.SetActive(!inOptionsMode);
            panelSettings.SetActive(inOptionsMode);
        }
    }
}
