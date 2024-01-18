using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverr : MonoBehaviour
{
    public GameObject GameOverMenuUI;
    
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
        GameOverMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void LoadMainMenu()
    {
        GameOverMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
