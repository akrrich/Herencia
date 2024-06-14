using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoorController : DoorController
{
    [SerializeField] string nextScene;

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsOpened || !collision.gameObject.CompareTag("Player"))
            return;
        
         SceneManager.LoadScene(nextScene);   
    }
}
