using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerOjects;
    public Canvas Endlevel;


    void Start()
    {
        Endlevel.gameObject.SetActive(false);// turn off UI for level up
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Levelup();
        }
    }

    public void Levelup()
    {
        PlayerOjects.SendMessage("RemoveAndAddUpgrades"); // Call for all buttton the function
        Endlevel.gameObject.SetActive(true); // set active level up UI disply
    }

    public void EndLevelUp()
    {
        Endlevel.gameObject.SetActive(false); // turn off UI for level up
    }
}