using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private MainCharacter character;
    [SerializeField] private ArmController armController;
    [SerializeField] private NotesController notesController;

    private GameObject panel;


    private bool gameInPause = false;

    private bool canEnterInPauseMode = true;


    public bool GameInPause
    {
        get
        {
            return gameInPause;
        }
    }

    public bool CanEnterInPauseMode
    {
        set
        {
            canEnterInPauseMode = value;
        }
    }


    private void Start()
    {
        panel = transform.Find("panel").gameObject;
    }


    private void Update()
    {
        CheckIfIsPauseOrNot();
    }


    private void CheckIfIsPauseOrNot()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canEnterInPauseMode == true)
        {
            if (gameInPause == false)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        panel.SetActive(gameInPause);

        notesController.CanOpenNoteMode = false;

        armController.CanMoveArm = false;

        character.CanMove = false;

        character.CanShootAllTime = false;

        Time.timeScale = 0f;

        gameInPause = true;
    }

    public void ResumeGame()
    {
        panel.SetActive(gameInPause);

        notesController.CanOpenNoteMode = true;

        if (notesController.OpenNoteMode == false)
        {
            armController.CanMoveArm = true;

            character.CanMove = true;

            character.CanShootAllTime = true;
        }

        Time.timeScale = 1f;

        gameInPause = false;
    }
}
