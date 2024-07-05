using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private VictorController victor;
    [SerializeField] private NotesController notesController;

    private VictorMapRotation victorMapRotation;
    private ArmController armController;

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

        armController = victor.GetComponentInChildren<ArmController>();
        victorMapRotation = victor.GetComponentInChildren<VictorMapRotation>();
    }


    private void Update()
    {
        CheckIfIsPauseOrNot();
    }


    private void CheckIfIsPauseOrNot()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Options")) && canEnterInPauseMode == true)
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

        victor.CanMove = false;

        notesController.CanOpenNoteMode = false;

        victorMapRotation.CanRotate = false;

        armController.CanMoveArm = false;

        Time.timeScale = 0f;

        gameInPause = true;
    }

    public void ResumeGame()
    {
        panel.SetActive(gameInPause);

        notesController.CanOpenNoteMode = true;

        victorMapRotation.CanRotate = true;

        if (notesController.OpenNoteMode == false)
        {
            armController.CanMoveArm = true;

            victor.CanMove = true;
        }

        Time.timeScale = 1f;

        gameInPause = false;
    }
}
