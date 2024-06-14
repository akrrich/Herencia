using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DoorController : MonoBehaviour
{
    [SerializeField] Sprite openedSprite;
    [SerializeField] Sprite closedSprite;

    private bool opened;
    public bool IsOpened { get => opened; }
    public bool IsClosed { get => !opened; }

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

    protected abstract void OnTriggerEnter2D(Collider2D collision);
}
