using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public MapController startingMap;
    public VictorController VictorPrefab;
    public VictorController VictorInstance;

    [Header("UI Controllers")]
    [SerializeField] PauseController pauseController;
    [SerializeField] FadeController fadeController;
    [SerializeField] JournalController journalController;
    [SerializeField] FullMapController fullMapController;
    [SerializeField] DialogController dialogController;

    private bool isPaused;
    private bool isShowingDialogUI;
    private bool shouldShowDialog;
    private int currentDialogID;
    public enum UIMenu
    {
        None,
        Journal,
        FullMap,
        Dialog
    }

    // Variable estática para almacenar la instancia única del GameManager.
    public static GameManager Instance { get; private set; }
    public bool IsPaused { get => isPaused; }

    private void Awake()
    {
        // Si ya hay una instancia y no es esta, destruye este objeto.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Si esta es la primera instancia, asígnala y marca este objeto para no destruirlo al cargar una nueva escena.
            Instance = this;
        }
    }
    public void PauseGame(bool showPauseMenu=true)
    {
        isPaused = true;
        Time.timeScale = 0;

        if(showPauseMenu)
            pauseController.SetActive(isPaused);
    }

    public void PlayGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        pauseController.SetActive(isPaused);
    }

    public void ShowSettings()
    {
        if (!isPaused)
            return;

        pauseController.ShowSettings();
    }

    public void HideSettings()
    {
        if (!isPaused)
            return;

        pauseController.HideSettings();
    }

    public void HideDialog()
    {
        PlayGame();
        dialogController.SetActive(false);
        isShowingDialogUI = false;
    }

    public void ShowDialog(int dialogID)
    {;
        shouldShowDialog = true;
        currentDialogID = dialogID;
    }

    private void ToggleUIMenu(UIMenu menu)
    {
        switch (menu)
        {
            case UIMenu.Journal:
                if (journalController.hasJournal)
                {
                    journalController.ToggleActive();
                    fullMapController.SetActive(false);
                }
                break;

            case UIMenu.FullMap:
                fullMapController.ToggleActive();
                journalController.SetActive(false);
                break;

            case UIMenu.Dialog:
                dialogController.SetActive(true);
                dialogController.SetDialog(currentDialogID);
                journalController.SetActive(false);
                fullMapController.SetActive(false);
                isShowingDialogUI = true;
                PauseGame(showPauseMenu: false);
                break;
        }
    }

    private void Start()
    {
        isPaused = false;
        isShowingDialogUI = false;
        currentDialogID = -1;

        pauseController.SetActive(false);
        fadeController.SetActive(true);
        journalController.SetActive(false);
        fullMapController.SetActive(false);
        dialogController.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                PlayGame();
            else
                PauseGame();
        }

        // TEST
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowDialog(0);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            ShowDialog(1);
        }

        if (isPaused)
            return;

        if (isShowingDialogUI)
            return;

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUIMenu(UIMenu.FullMap);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleUIMenu(UIMenu.Journal);
        }

        if (shouldShowDialog)
        {
            ToggleUIMenu(UIMenu.Dialog);
            shouldShowDialog = false;
        }

        if (startingMap is null) 
            return;

        if (!startingMap.HasBeenInitialized)
            startingMap.InitializeFloor();
    }
}
