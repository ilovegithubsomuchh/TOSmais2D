using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource MenuSource;
    
    [Header("---------- Audio Clip ----------")]
    public AudioClip musicMenu;

    private void Start()
    {
        MenuSource.clip = musicMenu;
        MenuSource.Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
