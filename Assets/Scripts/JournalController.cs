using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using System;
using System.Linq;

public class JournalController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject journalNotesCounterUI;
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text text;
    [SerializeField] Image cover;
    [SerializeField] TMP_Text notesCounter;

    [Header("Notas")]
    [SerializeField] List<NoteData> notesList;

    [Header("Sounds")]
    [SerializeField] AudioClip nextPageAudio;
    [SerializeField] AudioClip openJournalAudio;
    private AudioSource audioSource;

    public bool hasJournal;
    public bool IsJournalOpened;
    private int currentNote;

    private VictorController victorController;
    private void HandlePersonajeInstanciado(VictorController vc)
    {
        victorController = vc;
        victorController.OnJournalNotePicked += HandeJournalNotePicked;
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
            victorController.OnJournalNotePicked -= HandeJournalNotePicked;
        }
    }
    private void HandeJournalNotePicked(int noteId, bool isJournal)
    {
        if(isJournal)
        {
            hasJournal = true;
            journalNotesCounterUI.SetActive(true);
        }
        else
        {
            notesList[noteId].discovered = true;
            currentNote = noteId;

            TMP_Text noteCountUIText = journalNotesCounterUI.GetComponentInChildren<TMP_Text>();
            noteCountUIText.text = $"{notesList.Count(note => note.discovered)} / {notesList.Count}";
            
            SetPage();
        }
    }
    void Start()
    {
        // Hago una copia para no modificar los prefabs
        notesList = notesList.ConvertAll(note => Instantiate(note));
        audioSource = GetComponent<AudioSource>();
        currentNote = -1;
        hasJournal = false;
        IsJournalOpened = false;

        journalNotesCounterUI.SetActive(false);

        SetPage();
    }

    private void NextNote()
    {
        for (int i = currentNote + 1; i < notesList.Count; i++)
        {
            if (notesList[i].discovered)
            {
                currentNote = i;
                break;
            }
        }

        if(currentNote != -1)
        {
            SetPage();
            audioSource.clip = nextPageAudio;
            audioSource.Play();
        }
    }

    private void PreviousNote()
    {
        for (int i = currentNote - 1; i >= 0; i--)
        {
            if (notesList[i].discovered)
            {
                currentNote = i;
                break;
            }
        }

        if (currentNote != -1)
        {
            SetPage();
            audioSource.clip = nextPageAudio;
            audioSource.Play();
        }
    }
    private void Update()
    {
        if (GameManager.Instance.IsPaused)
            return;

        if (IsJournalOpened && Input.GetKeyDown(KeyCode.RightArrow))
            NextNote();

        if (IsJournalOpened && Input.GetKeyDown(KeyCode.LeftArrow))
            PreviousNote();
    }

    public void SetJournal(bool v)
    {
        if (hasJournal)
        {
            if (v)
                OpenJournal();
            else
                CloseJournal();
        }
    }

    private void OpenJournal()
    {
        panel.SetActive(true);
        IsJournalOpened=true;
        audioSource.clip = openJournalAudio;
        audioSource.Play();
    }

    private void CloseJournal()
    {
        panel.SetActive(false);
        IsJournalOpened = false;
        audioSource.clip = openJournalAudio;
        audioSource.Play();
    }

    public void SetPage()
    {
        if(currentNote == -1)
        {
            this.title.text = "";
            this.text.text = "";
            this.cover.sprite = null;
            notesCounter.text = "";
            return;
        }
        
        this.title.text = notesList[currentNote].title;
        this.text.text = notesList[currentNote].text;
        this.cover.sprite = notesList[currentNote].cover;
        notesCounter.text = $"{notesList[currentNote].id + 1} / {notesList.Count}";
    }

    public void ToggleActive() {
        SetActive(!IsJournalOpened);
    }

    public void SetActive(bool v)
    {
        SetJournal(v);
        panel.SetActive(v);
    }
}
