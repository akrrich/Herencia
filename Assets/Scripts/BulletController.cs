using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] AudioClip splashSound;

    private CharacterController owner;

    private Rigidbody2D rb;
    private Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Update()
    { 
        rb.velocity = direction;
    }
    internal void Init(CharacterController owner, Vector2 shootDirection)
    {
        this.owner = owner;
        direction = shootDirection * speed;
    }

    private void _Destroy()
    {
        AudioSource.PlayClipAtPoint(splashSound, transform.position);
        Destroy(this.gameObject);
    }

    private CharacterController GetChildComponentController(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (owner is VictorController && collision.gameObject.CompareTag("Enemy"))
            return go.GetComponent<EnemyController>();

        if (owner is EnemyController && collision.gameObject.CompareTag("Player"))
            return go.GetComponent<VictorController>();

        return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Prop"))
        {
            _Destroy();
            return;
        }

        CharacterController cc;
        cc = GetChildComponentController(collision);

        if (cc == null || !cc.IsAlive)
            return;
            
        cc.ApplyDamage(damage);
        _Destroy();
    }
}
