using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "NoteData", menuName = "ScriptableObjects/NoteData", order = 2)]
public class NoteData : ScriptableObject
{
    public string title;
    public string text;
    public Sprite cover;

    public int id;
    public bool discovered;

    public NoteData(NoteData noteToCopy)
    {
        title = noteToCopy.title;
        text = noteToCopy.text;
        cover = noteToCopy.cover;
        id = noteToCopy.id;
        discovered = noteToCopy.discovered;
    }
}
