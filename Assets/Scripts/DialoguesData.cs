using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "DialoguesData", menuName = "ScriptableObjects/DialoguesData", order = 3)]
public class DialoguesData : ScriptableObject
{
    public int id;
    public Sprite leftCharacter;
    public Sprite rightCharacter;
    public Sprite centerObjectImage;
    public List<string> dialogList;

    public DialoguesData(DialoguesData dialogToCopy)
    {
        id = dialogToCopy.id;
        leftCharacter = dialogToCopy.leftCharacter;
        rightCharacter = dialogToCopy.rightCharacter;
        dialogList = dialogToCopy.dialogList.ConvertAll<string>(dialog => dialog); // hago una copia
        centerObjectImage = dialogToCopy.centerObjectImage;
    }
}