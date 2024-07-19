using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogItemController : MonoBehaviour
{
    [SerializeField] int dialogID;
    [SerializeField] AudioClip itemGrabSound;

    private GameObject spriteMiniMap;
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
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

            GameManager.Instance.ShowDialog(dialogID);

            // Optionally, you can set the particle system to destroy itself after it has finished emitting
            Destroy(ps.gameObject, ps.main.duration);

            Destroy(this.gameObject);
        }
    }
}
