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

            vc.PickUpItem(stat, points);

            ps.transform.parent = null;

            // Optionally, you can set the particle system to destroy itself after it has finished emitting
            Destroy(ps.gameObject, ps.main.duration);

            Destroy(this.gameObject);
        }
    }
}
