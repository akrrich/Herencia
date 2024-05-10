using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject chestClose;
    [SerializeField] private GameObject chestOpen;

    [SerializeField] private BoxCollider2D triggerCollider;

    [SerializeField] private AudioSource musicChest;
    [SerializeField] private AudioSource soundOpenChest;

    [SerializeField] private SpriteRenderer spriteRendererCloseChest;


    private Color invisibleColor;

    private bool canInteractWithChest = true;
    private bool activeSoundOpenChest = false;


    private void Start()
    {
        spriteRendererCloseChest = chestClose.GetComponent<SpriteRenderer>();
        chestClose.SetActive(true);

        invisibleColor.a = 0f;
    }


    private void Update()
    {
        if (activeSoundOpenChest == true)
        {
            spriteRendererCloseChest.color = invisibleColor;

            musicChest.Stop();
            soundOpenChest.Play();

            chestOpen.SetActive(true);
            activeSoundOpenChest = false;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E) && canInteractWithChest == true)
        {
            activeSoundOpenChest = true;

            canInteractWithChest = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            if (canInteractWithChest == true)
            {
                musicChest.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D triggerCollider)
    {
        if (triggerCollider.gameObject.CompareTag("Player"))
        {
            musicChest.Stop();
        }
    }
}
