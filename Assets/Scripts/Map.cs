using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject allMap;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (BossDoorController.newScene == true)
        {
            allMap.SetActive(true);  
        }
    }
}
