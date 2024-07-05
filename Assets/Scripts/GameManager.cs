using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapController startingMap;
    public VictorController VictorPrefab;
    public VictorController VictorInstance;

    // Variable estática para almacenar la instancia única del GameManager.
    public static GameManager Instance { get; private set; }
    
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
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Update()
    {
        if (startingMap is null) 
            return;

        if (!startingMap.HasBeenInitialized)
            startingMap.InitializeFloor();
    }
}
