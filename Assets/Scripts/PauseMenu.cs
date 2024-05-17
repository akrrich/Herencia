using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private MainCharacter character;


    [SerializeField] private GameObject pauseMenuImage;


    private bool gameInPause = false;


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


    private void Update()
    {
        CheckIfIsPauseOrNot();
    }


    private void CheckIfIsPauseOrNot()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        pauseMenuImage.SetActive(true);

        character.CanShootAllTime = false;

        Time.timeScale = 0f;

        gameInPause = true;
    }

    public void ResumeGame()
    {
        pauseMenuImage.SetActive(false);

        character.CanShootAllTime = true;

        Time.timeScale = 1f;

        gameInPause = false;
    }
}
