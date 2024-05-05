using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy enemy;

    private string[] allTags = {"Chest", "Tile Map Colliders"};


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
        if (collision.isTrigger)
        {
            return;
        }

        for (int i = 0; i < allTags.Length; i++)
        {
            if (collision.gameObject.CompareTag(allTags[i]))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
