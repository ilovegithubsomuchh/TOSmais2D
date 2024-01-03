using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(PlayerMovement), typeof(PlayerInventory))] // Mandatory Player's component.
public class Player : MonoBehaviour
{
    [Header("References")] public PlayerSO PlayerData;
    public PlayerInventory _playerInventory;
    public GameManager _gameManager;


    [Header("Inventory Variables")] public int weaponIndex;


    private void Awake()
    {
        GetComponent<PlayerMovement>();
        _playerInventory = GetComponent<PlayerInventory>(); // Including  component variables 
        SpawnWeapon(PlayerData.baseWeapon); // spawn the base player weapon
    }

    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        _playerInventory.AddWeapon(weaponIndex, weapon.GetComponent<WeaponManager>());
        weaponIndex++; // once we spawned the weapon, make sur the next one goes in the next slot
        _gameManager.EndLevelUp(); // if player is leveling up, make sure to endLevelup State
    }
    
}