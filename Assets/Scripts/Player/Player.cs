using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(PlayerMovement), typeof(PlayerInventory))]
public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    //  public List<GameObject> SpawnedWeapons;
    public PlayerSO PlayerData;
    public int weaponIndex;
    public PlayerInventory _playerInventory;


    private void Awake()
    {
        GetComponent<PlayerMovement>();
        _playerInventory = GetComponent<PlayerInventory>();
        SpawnWeapon(PlayerData.baseWeapon);
    }

    public void SpawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        _playerInventory.AddWeapon(weaponIndex, weapon.GetComponent<WeaponManager>());
        weaponIndex++;
    }


    private void Update()
    {
        // var index = Random.Range(0, weaponIndex);
        // _playerInventory.LevelUpWeapon(index);
    }
}