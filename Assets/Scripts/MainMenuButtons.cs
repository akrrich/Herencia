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

    private float counterForPlay = 0;
    private float counterForExit = 0;

    private bool timeForSoundPlay = false;
    private bool timeForSoundExit = false;
    private bool isSoundPlaying = false;


    private void Update()
    {
        if (timeForSoundPlay == true)
        {
            counterForPlay += Time.deltaTime;

            if (counterForPlay > 0.35)
            {
                NotesController.CreateNewList();
                SceneManager.LoadScene("LaboratorioAbandonado");
            }
        }

        ExitMenuWithKey();
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

    private void ExitMenuWithKey()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isSoundPlaying == false)
            {
                optionSound.Play();
                isSoundPlaying = true;

                buttonPlay.transition = Selectable.Transition.None;
            }

            timeForSoundExit = true;
        }
    }
}
