using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScreens : MonoBehaviour
{
    [SerializeField] private MainCharacter mainCharacter;


    private AudioSource optionSound;

    [SerializeField] private AudioSource defeatSound;
    [SerializeField] private AudioSource winSound;


    private Button buttonPlayAgain;
    private Button buttonBackToMenu;


    private GameObject defeatImage;
    private GameObject winImage;

    private GameObject playAgain;
    private GameObject backToMenu;


    private float countForPlayButton = 0;
    private float countForBackButton = 0;


    private bool PlayButtonActive = false;
    private bool BackButtonActive = false;

    private bool isSoundPlaying = false;


    private void Start()
    { 
        optionSound = transform.Find("").GetComponent<AudioSource>();

        buttonPlayAgain = transform.Find("Button Play Again").GetComponent<Button>();
        buttonBackToMenu = transform.Find("Button Back To Menu").GetComponent<Button>();

        defeatImage = transform.Find("Panel Defeat").gameObject;
        winImage = transform.Find("Panel Win").gameObject;
        playAgain = transform.Find("Button Play Again").gameObject;
        backToMenu = transform.Find("Button Back To Menu").gameObject;
    }


    private void Update()
    {
        ConditionForChangeScene();

        DefeatScreen();
        WinScreen();
    }


    public void InteractWithButton(bool soundPlaying, AudioSource soundOption, bool buttonActive, Button button)
    {
        if (soundPlaying == false)
        {
            soundOption.Play();
            soundPlaying = true;
            buttonActive = true;

            button.transition = Selectable.Transition.None;
        }
    }

    public void InteractWithPlayAgainButton()
    {
        if (isSoundPlaying == false)
        {
            optionSound.Play();
            isSoundPlaying = true;
            PlayButtonActive = true;

            buttonPlayAgain.transition = Selectable.Transition.None;
        }
    }


    public void InteractWithBackToMenuButton()
    {
        if (isSoundPlaying == false) 
        {
            optionSound.Play();
            isSoundPlaying = true;
            BackButtonActive = true;

            buttonBackToMenu.transition = Selectable.Transition.None;
        }
    }

    private void DefeatScreen()
    {
        if (mainCharacter.Life < 1)
        {
            ActiveFinalSound(defeatSound);
            defeatImage.SetActive(true);
            
            ActiveBothButtons();
        }
    }

    private void WinScreen()
    {
        // future condiiton for win
        if (Input.GetKeyDown(KeyCode.V))
        {
            ActiveFinalSound(winSound);
            winImage.SetActive(true);

            ActiveBothButtons();
        }
    }

    private void ActiveBothButtons()
    {
        playAgain.SetActive(true);
        backToMenu.SetActive(true);
    }


    private void ActiveFinalSound(AudioSource finalAudio)
    {
        finalAudio.Play();
    }


    private void ConditionForChangeScene()
    {
        if (PlayButtonActive == true)
        {
            countForPlayButton += Time.deltaTime;
        }

        if (BackButtonActive == true)
        {
            countForBackButton += Time.deltaTime;
        }

        if (countForBackButton > 0.35f)
        {
            SceneManager.LoadScene("Menu");
        }

        if (countForPlayButton > 0.35f)
        {
            SceneManager.LoadScene("SceneInGame");
        }
    }
}
