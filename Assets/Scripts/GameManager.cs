using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float spawnTime = 1.5f;
    private float timeCounter = 0;


    [SerializeField] DeformeController deforme;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject player;


    private void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter > spawnTime)
        {
            timeCounter = 0;

            DeformeController df = Instantiate<DeformeController>(deforme, new Vector2(spawnPoint.position.x, spawnPoint.position.y), Quaternion.identity);
            df.SetTarget(player);
        }
    }
}
