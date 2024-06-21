using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] VictorController victor;
    [SerializeField] MapController startingMap;

    private static GameManager instance;
    public VictorController Victor { get => victor; set => victor = value; }
    public static GameManager Instance { 
        get { 
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    private void Update()
    {
        if(!startingMap.HasBeenInitialized)
            startingMap.InitializeFloor();
    }
}
