using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] private Transform victor;

    private void LateUpdate()
    {
        Vector2 newPosition = victor.position;

        newPosition.y = transform.position.y;

        transform.position = newPosition;   
    }
}
