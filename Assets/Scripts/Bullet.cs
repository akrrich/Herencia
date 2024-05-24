using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy enemy;

    private string[] allTags = {"Chest", "Tile Map Colliders, Enemy"};


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.isTrigger)
        {
            for (int i = 0; i < allTags.Length; i++)
            {
                if (collision.gameObject.CompareTag(allTags[i]))
                {
                    Destroy(this.gameObject);
                }
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy.life = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject go = collision.gameObject;
            var dc = go.GetComponent<DeformeController>();
            
            if (dc.IsDead)
                return;

            dc.Die();
            Destroy(this.gameObject);
            return;
        }
        
        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Prop"))
        {
            Destroy(this.gameObject);
        }
    }
}
