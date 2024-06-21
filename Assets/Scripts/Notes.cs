using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] private GameObject realNote;

    private AudioSource audioGetNote;
    private Renderer objectRenderer;
    private Collider2D objectCollider;


    private void Start()
    {
        objectCollider = GetComponent<Collider2D>();   
        objectRenderer = GetComponent<Renderer>();
        audioGetNote = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioGetNote.Play();

            StartCoroutine(DestroyAfterSound());

            objectRenderer.enabled = false;
            objectCollider.enabled = false;

            NotesController.AddNote(realNote);
        }
    }

    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(audioGetNote.clip.length);

        Destroy(gameObject);
    }
}
