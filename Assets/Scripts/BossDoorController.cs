using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoorController : DoorController
{
    [SerializeField] SceneAsset nextScene;

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsOpened || !collision.gameObject.CompareTag("Player"))
            return;
        
         SceneManager.LoadScene(nextScene.name);   
    }
}
