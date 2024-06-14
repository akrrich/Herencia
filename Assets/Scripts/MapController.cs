using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] DoorController doorUp;
    [SerializeField] DoorController doorRight;
    [SerializeField] DoorController doorBottom;
    [SerializeField] DoorController doorLeft;

    [SerializeField] List<EnemyController> enemigos;

    private bool hasBeenInitialized;
    private bool cleared;
    public bool HasBeenInitialized { get => hasBeenInitialized; }
    public bool Cleared { get => cleared; }


    private void HandleDoors(bool open)
    {
        if (doorUp != null && doorUp.isActiveAndEnabled)
            if (open)
                doorUp.Open();
            else
                doorUp.Close();

        if (doorRight != null && doorRight.isActiveAndEnabled)
            if (open)
                doorRight.Open();
            else
                doorRight.Close();

        if (doorBottom != null && doorBottom.isActiveAndEnabled)
            if (open)
                doorBottom.Open();
            else
                doorBottom.Close();

        if (doorLeft != null && doorLeft.isActiveAndEnabled)
            if (open)
                doorLeft.Open();
            else
                doorLeft.Close();
    }
    public void InitializeFloor()
    {
        hasBeenInitialized = true;

        HandleDoors(open: false);
    }

    public void ClearFloor()
    {
        cleared = true;

        HandleDoors(open: true);
    }
    public void HandleEnteringFloor()
    {
        if (!hasBeenInitialized)
        {
            InitializeFloor();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hasBeenInitialized = false;
        cleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemigos.TrueForAll(enemigo => !enemigo.IsAlive))
        {
            ClearFloor();
        }
    }
}
