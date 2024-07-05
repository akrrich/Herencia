using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;

public class NotesController : MonoBehaviour
{
    private static List<GameObject> notesList = new List<GameObject>();

    private ArmController armController;
    private VictorMapRotation victorMapRotation;

    private GameObject panel;

    private AudioSource changeNote;

    private TMP_Text diaryValue;


    private int currentNoteIndex = 0;

    private bool openNoteMode = false;
    private bool canOpneNoteMode = true;


    private VictorController victorController;

    private void HandlePersonajeInstanciado(VictorController vc)
    {
        victorController = vc;
        armController = victorController.GetComponentInChildren<ArmController>();
        victorMapRotation = victorController.GetComponentInChildren<VictorMapRotation>();
    }

    private void OnEnable()
    {
        VictorController.OnPersonajeInstanciado += HandlePersonajeInstanciado;
    }

    private void OnDisable()
    {
        if (victorController != null)
        {
            VictorController.OnPersonajeInstanciado -= HandlePersonajeInstanciado;
        }
    }

    public bool CanOpenNoteMode
    {
        set
        {
            canOpneNoteMode = value;
        }
    }

    public bool OpenNoteMode
    {
        get
        {
            return openNoteMode;
        }
    }


    private void Start()
    {
        diaryValue = GetComponentInChildren<TMP_Text>();
        changeNote = GetComponent<AudioSource>();

        diaryValue.text = 0 + " / " + 12;
        panel = transform.Find("Panel").gameObject;
    }

    private void Update()
    {
        if (canOpneNoteMode)
        {
            NoteMenu();
            ChangeNoteElement();
        }

        UpdateNoteHud();
    }


    public static void AddNote(GameObject newNote)
    {
        notesList.Add(newNote);
    }

    public static void CreateNewList()
    {
        notesList = new List<GameObject>();
    }


    private void NoteMenu()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Triangle")) && !openNoteMode && notesList.Count > 0)
        {
            panel.SetActive(true);

            victorController.CanMove = false;
            armController.CanMoveArm = false;
            victorMapRotation.CanRotate = false;

            notesList[currentNoteIndex].SetActive(true);

            openNoteMode = true;
        }

        else if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Triangle")) && openNoteMode)
        {
            panel.SetActive(false);

            victorController.CanMove = true;
            armController.CanMoveArm = true;
            victorMapRotation.CanRotate = true;

            notesList[currentNoteIndex].SetActive(false);

            openNoteMode = false;
        }
    }

    private void ChangeNoteElement()
    {
        if (openNoteMode == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SoundChangeNote();

                notesList[currentNoteIndex].SetActive(false);
                currentNoteIndex = (currentNoteIndex + 1) % notesList.Count;
                notesList[currentNoteIndex].SetActive(true);
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SoundChangeNote();

                notesList[currentNoteIndex].SetActive(false);
                currentNoteIndex = (currentNoteIndex - 1 + notesList.Count) % notesList.Count;
                notesList[currentNoteIndex].SetActive(true);
            }
        }
    }

    private void SoundChangeNote()
    {
        if (notesList.Count > 1)
        {
            changeNote.Play();
        }
    }

    private void UpdateNoteHud()
    {
        for (int i = 0; i < notesList.Count; i++)
        {
            diaryValue.text = (i + 1) + " / " + 12;
        }
    }
}
