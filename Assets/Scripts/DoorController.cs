using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] Sprite openedSprite;
    [SerializeField] Sprite closedSprite;

    [SerializeField] DoorController ConnectsTo;
    [SerializeField] Transform spawnPoint;


    private bool opened;
    public bool IsOpened { get => opened;}
    public bool IsClosed { get => !opened; }
    public Transform SpawnPoint { get => spawnPoint; }

    private SpriteRenderer sr;

    public void Open()
    { 
        opened = true;
        SetDoorState();
    }

    public void Close()
    {
        opened = false;
        SetDoorState();
    }

    private void SetDoorState()
    {
        sr.sprite = IsOpened ? openedSprite : closedSprite;
    }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        opened = false;
        SetDoorState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsOpened && ConnectsTo != null)
        {
            GameObject go = collision.gameObject;
            var mc = go.GetComponent<MainCharacter>();

            var fc = ConnectsTo.GetComponentInParent<MapController>();

            fc.HandleEnteringFloor();

            mc.transform.position = ConnectsTo.SpawnPoint.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
