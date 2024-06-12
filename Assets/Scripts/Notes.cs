using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] private GameObject realNote;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NotesController.AddNote(realNote);

            Destroy(gameObject);
        }
    }
}
