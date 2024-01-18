using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public GameObject victoryMenuUI;
    
    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
        victoryMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void LoadMainMenu()
    {
        victoryMenuUI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
