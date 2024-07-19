using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject allButtons;
    [SerializeField] private GameObject panelSettings;

    private AudioSource actionSound;

    private bool inOptionsMode = false;

    private Scene currentScene;

    private string[] allScenes = { "Menu", "Laboratorio", "BosqueMuerto", "ElPalacio"};


    private IEnumerator PlayClickSoundAndChangeScene(string sceneToLoad)
    {
        actionSound.Play();

        // Espera hasta que el sonido termine de reproducirse
        yield return new WaitForSeconds(actionSound.clip.length);

        ChangeScene(sceneToLoad);
    }

    private IEnumerator PlayClickSoundAndQuit()
    {
        actionSound.Play();

        // Espera hasta que el sonido termine de reproducirse
        yield return new WaitForSeconds(actionSound.clip.length);

        Application.Quit();
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

    public void SettingsButton()
    {
        inOptionsMode = true;
        actionSound.Play();

        PanelAndButtonsStatus();
    }
    public void BackButton()
    {
        inOptionsMode = false;
        actionSound.Play();

        PanelAndButtonsStatus();
    }

    public void CreditsButton()
    {
        StartCoroutine(PlayClickSoundAndChangeScene("Creditos"));
    }

    public void MainMenuButtonInGame()
    {
        if(GameManager.Instance.IsPaused)
            GameManager.Instance.PlayGame();
        StartCoroutine(PlayClickSoundAndChangeScene("Menu"));
    }

    public void MainMenuButtonInFinalScreen()
    {
        StartCoroutine(PlayClickSoundAndChangeScene("Menu"));
    }

    public void ExitButton()
    {
        StartCoroutine(PlayClickSoundAndQuit());
    }

    private void Start()
    {
        actionSound = GetComponent<AudioSource>();
        inOptionsMode = false;
        PanelAndButtonsStatus();
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void PanelAndButtonsStatus()
    {
        if (allScenes.Contains(currentScene.name))
        {
            if (inOptionsMode)
            {
                if (panelSettings)
                    panelSettings.SetActive(true);
            }
            else
            {
                if (panelSettings)
                    panelSettings.SetActive(false);
            }
            allButtons.SetActive(!inOptionsMode);
        }
    }
}
