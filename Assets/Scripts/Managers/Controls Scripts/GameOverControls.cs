using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverControls : MonoBehaviour
{
    public GameObject pausPanel;
    public void MainMinu()
    {
        SceneManager.LoadScene(0);
    }
    public void Reload()
    {
        SceneManager.LoadScene(1);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pausPanel.SetActive(true);
    }
    public void Continue()
    {
        Time.timeScale = 1;
        pausPanel.SetActive(false);
    }
    public void EndGame()
    {
        Time.timeScale = 1;
        pausPanel.SetActive(false);
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.UpdateLives(-gameManager.lives);
    }
}
