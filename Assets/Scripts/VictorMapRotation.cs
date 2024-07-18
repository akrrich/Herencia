using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class VictorMapRotation : MonoBehaviour
{
    private VictorController victorController;

    private Transform victor;

    private void Awake()
    {
        victorController = GetComponentInParent<VictorController>();
        victor = victorController.transform;
    }


    private void Update()
    {
        if (!GameManager.Instance.IsPaused)
        {
            Vector3 mousePosition = Input.mousePosition;

            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
            mousePosition.z = 0;

            Vector3 direction = mousePosition - victor.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += 230f;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
