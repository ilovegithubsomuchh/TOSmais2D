using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public GameObject PlayerOjects;
    public Canvas Endlevel;
    
    
    void Start()
    {
        PlayerOjects.SendMessage("RemoveAndAndUpgrades");
        Endlevel.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
