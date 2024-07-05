using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] private GameObject realNote;

    private GameObject spriteMiniMap;

    [SerializeField] private AudioClip audioGetNote;

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

            AudioSource.PlayClipAtPoint(audioGetNote, transform.position);

            GameObject go = collision.gameObject;

            ps.transform.parent = null;

            // Optionally, you can set the particle system to destroy itself after it has finished emitting
            Destroy(ps.gameObject, ps.main.duration);

            NotesController.AddNote(realNote);
            Destroy(this.gameObject);
        }
    }

    private void Hover()
    {
        currentDelta += Time.deltaTime * direction;

        if (Mathf.Abs(currentDelta) > deltaY)
        {
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
