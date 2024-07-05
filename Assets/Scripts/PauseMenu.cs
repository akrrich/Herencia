using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private NotesController notesController;

    private VictorMapRotation victorMapRotation;
    private ArmController armController;

    private GameObject panel;

    private bool gameInPause = false;

    private bool canEnterInPauseMode = true;


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

        victorController.CanMove = false;

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

            victorController.CanMove = true;
        }

        Time.timeScale = 1f;

        gameInPause = false;
    }
}
