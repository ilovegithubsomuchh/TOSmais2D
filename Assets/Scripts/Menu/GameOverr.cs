using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverr : MonoBehaviour
{
    public GameObject GameOverMenuUI;
    
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
