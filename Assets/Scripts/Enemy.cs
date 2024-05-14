using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private BoxCollider2D colliderEnemy;

    [SerializeField] private GameObject enemy;


    public int life = 3;


    private void Update()
    {
        if (life < 1)
        {
            enemy.SetActive(false);
        }
    }


    /*private void OnTriggerEnter2D(Collider2D colliderTrigger)
    {
        if (colliderTrigger.gameObject.CompareTag("Player"))
        {

        }
    }*/

    private void OnCollisionEnter2D(Collision2D colliderEnemy)
    {
        if (colliderEnemy.gameObject.CompareTag("Main Char Bullet"))
        {
            Destroy(this.gameObject);
        }
    }
}
