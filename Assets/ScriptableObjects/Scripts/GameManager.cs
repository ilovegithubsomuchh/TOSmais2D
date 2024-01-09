using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Public references to necessary objects in the GameManager
    public GameObject PlayerObjects;
    public GameObject Enemy;
    public Canvas EndLevelUI;
    private bool isLeveling; // Boolean to track if the level is currently being upgraded

    void Start()
    {
        // Deactivate the level up UI at the start
        EndLevelUI.gameObject.SetActive(false);
        
        // Initialize the level control variable
        isLeveling = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Nothing in the Update function for now
    }

    // Method called to trigger the level up
    // ReSharper disable Unity.PerformanceAnalysis
    public void LevelUp()
    {
        // Check if the level is not already in the process of upgrading
        if (!isLeveling)
        {
            // Mark that the level is currently being upgraded
            isLeveling = true;
            
            // Pause the game time
            Time.timeScale = 0f;
            
            // Call the "RemoveAndAddUpgrades" function on all PlayerObjects
            PlayerObjects.SendMessage("RemoveAndAddUpgrades");

            // Activate the level up UI display
            EndLevelUI.gameObject.SetActive(true);
        }
    }

    // Method called when the level up is complete
    public void EndLevelUp()
    {
        Debug.Log("here");
        // Toggle the isLeveling variable
        isLeveling = !isLeveling;
        
        // Resume the game time
        Time.timeScale = 1f;
        
        // Call the "ChangeWave" function on the Enemy
        Enemy.SendMessage("ChangeWave");
        
        // Deactivate the level up UI
        EndLevelUI.gameObject.SetActive(false);
    }
}