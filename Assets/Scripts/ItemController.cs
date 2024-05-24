using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    [SerializeField] MainCharacter.Stats stat;
    [SerializeField] float points;

    private AudioSource itemGrabSound;
    private Transform sprite;

    private float deltaY;
    private float currentDelta;
    private int direction;


    private void Start()
    {
        itemGrabSound = GetComponent<AudioSource>();
        sprite = transform.GetChild(0);

        deltaY = 0.5f;
        currentDelta = 0;
        direction = 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            itemGrabSound.Play();

            GameObject go = collision.gameObject;
            var mc = go.GetComponent<MainCharacter>();

            mc.PickUpItem(stat, points);

            Destroy(this.gameObject);
        }
    }

    private void Hover()
    {
        currentDelta += Time.deltaTime * direction;

        if (Mathf.Abs(currentDelta) > deltaY) {
            currentDelta = 0;
            direction = -direction;
        }

        sprite.transform.position += new Vector3(0, Time.deltaTime * direction, 0);
    }


    private void Update()
    {
        Hover();
    }
}
