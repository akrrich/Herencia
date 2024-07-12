using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VictorController;

public class JournalNoteController : MonoBehaviour
{
    [SerializeField, Range(0, 11)] int noteId;
    [SerializeField] bool isJournal;
    [SerializeField] AudioClip itemGrabSound;

    private GameObject spriteMiniMap;
    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();

        spriteMiniMap = transform.Find("MiniMap Objetive").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spriteMiniMap.SetActive(false);

            AudioSource.PlayClipAtPoint(itemGrabSound, transform.position);

            GameObject go = collision.gameObject;
            var vc = go.GetComponent<VictorController>();

            vc.PickUpJournalNote(noteId, isJournal);

            ps.transform.parent = null;

            Destroy(ps.gameObject, ps.main.duration);
            Destroy(this.gameObject);
        }
    }
}
