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

    private float counter = 0f;

    private bool activeItem = false;

    private bool activeItemSound = false;

    private bool activeCounterForDestroItem = false;

    [SerializeField] private float amount;

    private void Start()
    {
        itemGrabSound = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        itemRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        PlaySound();
        DestroyItem();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(boxCollider);

            Color newColor = itemRenderer.color;
            newColor.a = 0f;

            itemRenderer.color = newColor;

            activeItemSound = true;
            activeItem = true;

            activeCounterForDestroItem = true;

            GameObject go = collision.gameObject;
            var player = go.GetComponent<MainCharacter>();

            player.IncreaseLife(amount);

        }
    }


    public void ChangeStatValue()
    {
        if (activeItem == true)
        { 
            activeItem = false;
        }
    }


    private void PlaySound()
    {
        if (activeItemSound == true)
        {
            itemGrabSound.Play();

            activeItemSound = false;
        }
    }

    private void DestroyItem()
    {
        if (activeCounterForDestroItem == true)
        {
            counter += Time.deltaTime;

            if (counter > 0.5f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
