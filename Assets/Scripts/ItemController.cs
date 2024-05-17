using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private AudioSource itemGrabSound;

    private BoxCollider2D boxCollider;

    private SpriteRenderer itemRenderer;


    private bool activeItem = false;


    private void Start()
    {
        itemGrabSound = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        itemRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            itemGrabSound.Play();

            Destroy(boxCollider);

            Color newColor = itemRenderer.color;
            newColor.a = 0f;

            itemRenderer.color = newColor;

            activeItem = true;

            Destroy(this.gameObject, 1.5f);
        }
    }


    public void ChangeStatValue(ref float statVlue)
    {
        if (activeItem == true)
        {
            statVlue++;

            activeItem = false;
        }
    }
}
