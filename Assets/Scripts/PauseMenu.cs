using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private MainCharacter character;


    private bool gameInPause = false;

    private bool canEnterInPauseMode = true;


    public bool GameInPause
    {
        get
        {
            return gameInPause;
        }

        set
        {
            gameInPause = value;
        }
    }

    public bool CanEnterInPauseMode
    {
        set
        {
            canEnterInPauseMode = value;
        }
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
        character.CanShootAllTime = false;

        Time.timeScale = 0f;

        gameInPause = true;
    }

    public void ResumeGame()
    {
        character.CanShootAllTime = true;

        Time.timeScale = 1f;

        gameInPause = false;
    }
}
