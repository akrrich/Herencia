using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Puertas")]
    [SerializeField] GameObject doorBoss;
    [SerializeField] GameObject doorUp;
    [SerializeField] GameObject doorRight;
    [SerializeField] GameObject doorBottom;
    [SerializeField] GameObject doorLeft;

    [Header("Lista Enemigos")]
    [SerializeField] List<EnemyController> enemigos;

    private bool hasBeenInitialized;
    private bool cleared;

    public BossDoorController doorBossController;
    public RegularDoorController doorUpController;
    public RegularDoorController doorRightController;
    public RegularDoorController doorBottomController;
    public RegularDoorController doorLeftController;

    public bool HasBeenInitialized { get => hasBeenInitialized; }
    public bool Cleared { get => cleared; }

    public enum Doors
    {
        Up,
        Right,
        Bottom,
        Left,
        Boss
    }

    public void SetEnabledSingleDoor(Doors door, bool value)
    {
        switch (door)
        {
            case Doors.Up:
                doorUp.SetActive(value); break;

            case Doors.Right:
                doorRight.SetActive(value); break;

            case Doors.Bottom:
                doorBottom.SetActive(value); break;

            case Doors.Left:
                doorLeft.SetActive(value); break;

            case Doors.Boss:
                doorBoss.SetActive(value); break;
        }
    }

    public void SetEnabledAllDoors(bool value)
    {
        doorUp.SetActive(value);
        doorRight.SetActive(value);
        doorBottom.SetActive(value);
        doorLeft.SetActive(value);
    }

    private void HandleDoors(bool open)
    {
        if (doorUpController != null && doorUpController.isActiveAndEnabled)
            if (open)
                doorUpController.Open();
            else
                doorUpController.Close();

        if (doorRightController != null && doorRightController.isActiveAndEnabled)
            if (open)
                doorRightController.Open();
            else
                doorRightController.Close();

        if (doorBottomController != null && doorBottomController.isActiveAndEnabled)
            if (open)
                doorBottomController.Open();
            else
                doorBottomController.Close();

        if (doorLeftController != null && doorLeftController.isActiveAndEnabled)
            if (open)
                doorLeftController.Open();
            else
                doorLeftController.Close();

        if (doorBossController != null && doorBossController.isActiveAndEnabled)
            if (open)
                doorBossController.Open();
            else
                doorBossController.Close();
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
            foreach (var enemigo in enemigos)
            {
                enemigo.target = GameManager.Instance.VictorInstance;
            }
        }
    }

    static public void ConnectDoors(MapController a, MapController b,Doors aDirection)
    {
        switch (aDirection)
        {
            case Doors.Up:
                a.SetEnabledSingleDoor(Doors.Bottom, true);
                b.SetEnabledSingleDoor(Doors.Up, true);

                a.doorBottomController.ConnectsTo = b.doorUpController;
                b.doorUpController.ConnectsTo = a.doorBottomController;
                break;

            case Doors.Right:
                a.SetEnabledSingleDoor(Doors.Right, true);
                b.SetEnabledSingleDoor(Doors.Left, true);

                a.doorRightController.ConnectsTo = b.doorLeftController;
                b.doorLeftController.ConnectsTo = a.doorRightController;
                break;

            case Doors.Bottom:
                a.SetEnabledSingleDoor(Doors.Up, true);
                b.SetEnabledSingleDoor(Doors.Bottom, true);

                a.doorUpController.ConnectsTo = b.doorBottomController;
                b.doorBottomController.ConnectsTo = a.doorUpController;
                break;

            case Doors.Left:
                a.SetEnabledSingleDoor(Doors.Left, true);
                b.SetEnabledSingleDoor(Doors.Right, true);

                a.doorLeftController.ConnectsTo = b.doorRightController;
                b.doorRightController.ConnectsTo =a.doorLeftController;
                break;
        }
    }
    private void Awake()
    {
        if(doorBoss != null)
            doorBossController = doorBoss.GetComponent<BossDoorController>();

        doorUpController = doorUp.GetComponent<RegularDoorController>();
        doorRightController = doorRight.GetComponent<RegularDoorController>();
        doorBottomController = doorBottom.GetComponent<RegularDoorController>();
        doorLeftController = doorLeft.GetComponent<RegularDoorController>();
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
