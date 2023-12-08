using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public GameObject PlayerOjects;
    public Canvas Endlevel;
    
    
    void Start()
    {
        Endlevel.gameObject.SetActive(false);

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
        PlayerOjects.SendMessage("RemoveAndAddUpgrades");
        Endlevel.gameObject.SetActive(true);
    }

   public void EndLevelUp()
    {
        Endlevel.gameObject.SetActive(false);
        
    }
}
