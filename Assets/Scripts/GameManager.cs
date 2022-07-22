using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject youWinCanvas;

    public void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doExitGame();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Replay();
        }
    }

    public void doExitGame()
    {
        Debug.Log("Quitting Game!");
        Application.Quit();
    }

    public void GameOver()
    {
        Invoke("End", 0.25f);
    }

    public void YouWin()
    {
        Invoke("Win", 0.25f);
    }

    private void End()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    private void Win()
    {
        youWinCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
}
