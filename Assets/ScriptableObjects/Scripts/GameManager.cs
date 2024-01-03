using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerOjects;
    public GameObject Ennemy;
    public Canvas Endlevel;
    private bool isLeveling;


    void Start()
    {
        Endlevel.gameObject.SetActive(false);// turn off UI for level up
        isLeveling = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Levelup()
    {
     
        if (!isLeveling)
        {
            isLeveling = true;
            Time.timeScale = 0f;
            PlayerOjects.SendMessage("RemoveAndAddUpgrades"); // Call for all button the function
            Endlevel.gameObject.SetActive(true); // set active level up UI display
        }
    }

    public void EndLevelUp()
    {
        isLeveling = !isLeveling;
        Time.timeScale = 1f;
        Ennemy.SendMessage("ChangeWave");
        Endlevel.gameObject.SetActive(false); // turn off UI for level up
    }
}