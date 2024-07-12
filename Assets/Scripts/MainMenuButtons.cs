using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private AudioSource actionSound;

    private IEnumerator PlayClickSoundAndChangeScene(string sceneToLoad)
    {
        actionSound.Play();

        // Espera hasta que el sonido termine de reproducirse
        yield return new WaitForSeconds(actionSound.clip.length);

        ChangeScene(sceneToLoad);
    }

    private void ChangeScene(string sceneToLoad)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void PlayButton()
    {
        StartCoroutine(PlayClickSoundAndChangeScene("Laboratorio"));
    }
    public void OptionsButton()
    {
        
    }


    public void CreditsButton()
    {
        StartCoroutine(PlayClickSoundAndChangeScene("Creditos"));
    }

    public void MainMeniButton()
    {
        StartCoroutine(PlayClickSoundAndChangeScene("Menu"));
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
