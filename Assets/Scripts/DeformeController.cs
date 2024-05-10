using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformeController : MonoBehaviour
{

    [SerializeField] GameObject target;
    [SerializeField] AudioClip dieSound;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveTo = target.transform.position - transform.position;

        rb.velocity = moveTo.normalized * 3;
    }
    public void Die()
    {
        AudioSource.PlayClipAtPoint(dieSound, transform.position);
        Destroy(this.gameObject);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
