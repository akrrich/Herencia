using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoorController : DoorController
{
    [SerializeField] string nextScene;

    public static bool newScene = false;

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsOpened || !collision.gameObject.CompareTag("Player"))
            return;

         newScene = true;
        
         SceneManager.LoadScene(nextScene);   
    }
}
