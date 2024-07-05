using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class JournalController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text text;
    [SerializeField] Image cover;

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
        }
        else
        {
            notesList[noteId].discovered = true;
            currentNote = noteId;
            SetPage(notesList[noteId].title, notesList[noteId].text, notesList[noteId].cover);
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

        SetPage("", "", null);
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
            SetPage(notesList[currentNote].title, notesList[currentNote].text, notesList[currentNote].cover);
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
            SetPage(notesList[currentNote].title, notesList[currentNote].text, notesList[currentNote].cover);
            audioSource.clip = nextPageAudio;
            audioSource.Play();
        }
    }

    private void OpenJournal()
    {
        panel.SetActive(true);
        IsJournalOpened=true;
        audioSource.clip = openJournalAudio;
        audioSource.Play();
        Time.timeScale = 0;
    }

    private void CloseJournal()
    {
        panel.SetActive(false);
        IsJournalOpened = false;
        audioSource.clip = openJournalAudio;
        audioSource.Play();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
       if(hasJournal) 
       {
        if (Input.GetKeyDown(KeyCode.J))
            if(IsJournalOpened)
                CloseJournal();
            else
                OpenJournal();

            if (Input.GetKeyDown(KeyCode.RightArrow))
                NextNote();

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                PreviousNote();
        }
    }

    public void SetPage(string title, string text, Sprite cover)
    {
        this.title.text = title;
        this.text.text = text;
        this.cover.sprite = cover;
    }
}
