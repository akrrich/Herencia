using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] CharacterController.Stats stat;
    [SerializeField] float points;
    [SerializeField] AudioClip itemGrabSound;

    private GameObject spriteMiniMap;

    private Transform sprite;

    private float deltaY;
    private float currentDelta;
    private int direction;

    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();

        spriteMiniMap = transform.Find("MiniMap Objetive").gameObject;

        sprite = transform.GetChild(0);
        deltaY = 0.5f;
        currentDelta = 0;
        direction = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteMiniMap.SetActive(false);

            AudioSource.PlayClipAtPoint(itemGrabSound, transform.position);

            GameObject go = collision.gameObject;
            var vc = go.GetComponent<VictorController>();

            vc.PickUpItem(stat, points);

            ps.transform.parent = null;

            // Optionally, you can set the particle system to destroy itself after it has finished emitting
            Destroy(ps.gameObject, ps.main.duration);


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
