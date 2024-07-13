using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapController startingMap;
    public VictorController VictorPrefab;
    public VictorController VictorInstance;

    [Header("UI Controllers")]
    [SerializeField] PauseController pauseController;
    [SerializeField] SettingsController settingsController;
    [SerializeField] FadeController fadeController;
    [SerializeField] JournalController journalController;
    [SerializeField] FullMapController fullMapController;
    [SerializeField] DialogController dialogController;

    private bool isPaused;

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

    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0.0f : 1.0f;

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
        }
    }

    private void Start()
    {
        isPaused = false;

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
            PauseGame();
        }

        if (isPaused)
            return;

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleUIMenu(UIMenu.FullMap);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleUIMenu(UIMenu.Journal);
        }

        if (startingMap is null) 
            return;

        if (!startingMap.HasBeenInitialized)
            startingMap.InitializeFloor();
    }
}
