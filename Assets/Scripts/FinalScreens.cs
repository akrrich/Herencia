using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScreens : MonoBehaviour
{
    [SerializeField] private MainCharacter mainCharacter;

    [SerializeField] private AnimationMainCharacter anim;

    [SerializeField] private PauseMenu pauseMenu;


    [SerializeField] private AudioSource winSound;

    private AudioSource optionSound;


    private Button buttonPlayAgain;
    private Button buttonBackToMenu;


    private GameObject finalPanel;

    private GameObject playAgain;
    private GameObject backToMenu;


    private float countForPlayButton = 0;
    private float countForBackButton = 0;


    private bool PlayButtonActive = false;
    private bool BackButtonActive = false;

    private bool isSoundPlaying = false;


    private string sceneInGameName = "SceneInGame";
    private string sceneMenuName = "Menu";


    private void Start()
    {
        optionSound = GetComponent<AudioSource>();

        buttonPlayAgain = transform.Find("Button Play Again").GetComponent<Button>();
        buttonBackToMenu = transform.Find("Button Back To Menu").GetComponent<Button>();

        finalPanel = transform.Find("Panel Win").gameObject;
        playAgain = transform.Find("Button Play Again").gameObject;
        backToMenu = transform.Find("Button Back To Menu").gameObject;
    }


    private void Update()
    {
        ConditionForChangeScenes(ref PlayButtonActive, ref countForPlayButton, ref sceneInGameName);
        ConditionForChangeScenes(ref BackButtonActive, ref countForBackButton, ref sceneMenuName);

        DefeatScreen();
        WinScreen();
    }


    public void InteractWithPlayAgainButton()
    {
        if (isSoundPlaying == false)
        {
            Time.timeScale = 1f;

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
            Time.timeScale = 1f;

            optionSound.Play();
            isSoundPlaying = true;
            BackButtonActive = true;

            buttonBackToMenu.transition = Selectable.Transition.None;
        }
    }


    private void DefeatScreen()
    {
        if (mainCharacter.Alive == false && anim.CanShowDefeatScreen == true)
        {
            mainCharacter.CanShootAllTime = false;

            pauseMenu.CanEnterInPauseMode = false;

            anim.CanDoAnimations = false;

            Time.timeScale = 0f;

            finalPanel.SetActive(true);

            ActiveBothButtons();
        }
    }

    private void WinScreen()
    {
        // future condiiton for win
        if (Input.GetKeyDown(KeyCode.V))
        {
            mainCharacter.CanShootAllTime = false;

            pauseMenu.CanEnterInPauseMode = false;

            anim.CanDoAnimations = false;

            Time.timeScale = 0f;

            finalPanel.SetActive(true);

            winSound.Play();

            ActiveBothButtons();
        }
    }

    private void ActiveBothButtons()
    {
        playAgain.SetActive(true);
        backToMenu.SetActive(true);
    }

    private void ConditionForChangeScenes(ref bool buttonActive, ref float counterForButton, ref string nameScene)
    {
        if (buttonActive == true)
        {
            counterForButton += Time.deltaTime;
        }

        if (counterForButton > 0.35f)
        {
            SceneManager.LoadScene(nameScene);
        }
    }
}
