using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotesController : MonoBehaviour
{
    private static List<GameObject> notesList = new List<GameObject>();

    [SerializeField] private GameObject panel;


    private AudioSource changeNote;

    private TMP_Text diaryValue;


    private int currentNoteIndex = 0;

    private bool openNoteMode = false;


    private void Start()
    {
        diaryValue = GetComponentInChildren<TMP_Text>();
        changeNote = GetComponent<AudioSource>();

        diaryValue.text = 0 + " / " + 10;
    }


    private void Update()
    {
        NoteMenu();
        ChangeNoteElement();
        UpdateNoteHud();
    }


    public static void AddNote(GameObject newNote)
    {
        notesList.Add(newNote);
    }


    private void NoteMenu()
    {
        if (Input.GetKeyDown(KeyCode.Q) && openNoteMode == false && notesList.Count > 0)
        {
            panel.SetActive(true);
            Time.timeScale = 0f;

            notesList[currentNoteIndex].SetActive(true);

            openNoteMode = true;
        }

        else if (Input.GetKeyDown(KeyCode.Q) && openNoteMode == true)
        {
            panel.SetActive(false);
            Time.timeScale = 1f;

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
                if (notesList.Count > 1)
                {
                    changeNote.Play();
                }

                notesList[currentNoteIndex].SetActive(false);
                currentNoteIndex = (currentNoteIndex + 1) % notesList.Count;
                notesList[currentNoteIndex].SetActive(true);
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (notesList.Count > 1)
                {
                    changeNote.Play();
                }

                notesList[currentNoteIndex].SetActive(false);
                currentNoteIndex = (currentNoteIndex - 1 + notesList.Count) % notesList.Count;
                notesList[currentNoteIndex].SetActive(true);
            }
        }
    }

    private void UpdateNoteHud()
    {
        for (int i = 0; i < notesList.Count; i++)
        {
            diaryValue.text = (i + 1) + " / " + 10;
        }
    }
}
