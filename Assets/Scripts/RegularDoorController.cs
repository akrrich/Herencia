using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularDoorController : DoorController
{
    [SerializeField] public RegularDoorController ConnectsTo;
    [SerializeField] Transform spawnPoint;

    public Transform SpawnPoint { get => spawnPoint; }

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsOpened && ConnectsTo != null)
        {
            GameObject go = collision.gameObject;
            var vc = go.GetComponent<VictorController>();

            var fc = ConnectsTo.GetComponentInParent<MapController>();

            fc.HandleEnteringFloor();

            vc.transform.position = ConnectsTo.SpawnPoint.position;
        }
    }
}
