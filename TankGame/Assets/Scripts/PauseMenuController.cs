using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    bool pauseGame;
    [SerializeField]
    private GameObject pausePanel;

    private void Start()
    {
        pauseGame = false;
    }
    public void ResumeGame()
    {
        if (GameObject.Find("GameController"))
        {
            if (!GameObject.Find("GameController").GetComponent<GameController>().getGameOver())
            {
                pauseGame = false;
                pausePanel.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (GameObject.Find("GameController"))
        {
            if (!GameObject.Find("GameController").GetComponent<GameController>().getGameOver())
            {

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    pauseGame = !pauseGame;
                    pausePanel.SetActive(pauseGame);
                }
            }
        }
        if (pauseGame)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
