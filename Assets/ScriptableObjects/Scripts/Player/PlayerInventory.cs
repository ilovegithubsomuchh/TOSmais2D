using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.Linq;


public class PlayerInventory : MonoBehaviour
{
    public List<WeaponManager> WeaponSlots = new List<WeaponManager>(4);
    private Player _player;
    public GameManager _gameManager;

    #region Upgrade Class

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUprgadeIndex;
        public GameObject startingWeapon;
        public WeaponSO WeaponData;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public TextMeshProUGUI UpgradeName;
        public TextMeshProUGUI UpgradeDescription;
        public Image UpgradeIcon;
        public Button UpgradeButton;
    }

    [System.Serializable]
    public class WeaponList
    {
        public GameObject Weapon;
        public WeaponSO WeaponData;
    }

    public List<WeaponUpgrade> UpgradeOptions = new List<WeaponUpgrade>();
    public List<UpgradeUI> UIOptions = new List<UpgradeUI>();
    public List<WeaponList> Weapons = new List<WeaponList>();

    #endregion


    private void Start()
    {
        _player = GetComponent<Player>();
        UpgradeOptions = new List<WeaponUpgrade>();

        // Initialize UpgradeOptions for each weapon in the game
        for (int i = 0; i < Weapons.Count; i++)
        {
            UpgradeOptions.Add(new WeaponUpgrade
            {
                weaponUprgadeIndex = i,
                startingWeapon = (Weapons[i] != null) ? Weapons[i].Weapon : null,
                WeaponData = (Weapons[i] != null) ? Weapons[i].WeaponData : null
            });
        }

        // Fill UpgradeOptions with unique weapons
        for (int i = 0; i < UpgradeOptions.Count; i++)
        {
            if (UpgradeOptions[i].WeaponData == null)
            {
                bool weaponAlreadyExists = false;

                // Manual check to see if the weapon is already present in any of the upgrade options
                for (int j = 0; j < i; j++)
                {
                    if (UpgradeOptions[j].WeaponData == Weapons[i].WeaponData)
                    {
                        weaponAlreadyExists = true;
                        break;
                    }
                }

                // If the weapon is not already present, add it
                if (!weaponAlreadyExists)
                {
                    UpgradeOptions[i].weaponUprgadeIndex = i;
                    UpgradeOptions[i].startingWeapon = (Weapons[i] != null) ? Weapons[i].Weapon : null;
                    UpgradeOptions[i].WeaponData = (Weapons[i] != null) ? Weapons[i].WeaponData : null;
                }
            }
        }
    }



    public void AddWeapon(int slotIndex, WeaponManager weapon)
    {
        WeaponSlots[slotIndex] = weapon; // add a new weapon manager in the specified slotIndex
    }

    public void LevelUpWeapon(int slotIndex, int upgradeIndex)
    {
        if (WeaponSlots.Count > slotIndex &&
            (WeaponSlots != null)) // Check if index to upgrade weapon is not out of range
        {
            WeaponManager weaponManager = WeaponSlots[slotIndex];

            GameObject UpgradedWeapon = Instantiate(weaponManager.WeaponData.NextUpgrade, transform.position,
                Quaternion.identity);

            UpgradedWeapon.transform.SetParent(transform);

            AddWeapon(slotIndex, UpgradedWeapon.GetComponent<WeaponManager>());
            Destroy(transform.GetChild(slotIndex)
                .gameObject); // Create a child with the next update from the previous weaponManager, and remove the previous one
            UpgradeOptions[upgradeIndex].WeaponData = UpgradedWeapon.GetComponent<WeaponManager>().WeaponData;
            _gameManager.EndLevelUp(); // Finish the level up options in the gameManager
        }
    }

   void UpgradeOption()
{
    List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(UpgradeOptions);
    
    foreach (var upgrade in UIOptions) // Checking all possible upgrades
    {
        WeaponUpgrade weaponToUpgrade = null;

        // Loop until a different upgrade is picked
        do
        {
            if (availableWeaponUpgrades.Count == 0)
            {
                // No more unique upgrades available, break the loop
                DisableUpgradeUI(upgrade);
                
                break;
                
            }

            // Choose 1 upgrade from all possible upgrades
            weaponToUpgrade = availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];
        } while (weaponToUpgrade == null);

        if (weaponToUpgrade != null)
        {
            availableWeaponUpgrades.Remove(weaponToUpgrade);
            
            EnagleUpgradeUI(upgrade);
            bool newWeapon = false;

            for (int i = 0; i < WeaponSlots.Count; i++)
            {
                if (WeaponSlots[i] != null && WeaponSlots[i].WeaponData == weaponToUpgrade.WeaponData)
                {
                    if (!newWeapon)
                    {
                        if (!weaponToUpgrade.WeaponData.NextUpgrade)
                        {
                            DisableUpgradeUI(upgrade);
                            break;
                        }

                        upgrade.UpgradeButton.onClick.AddListener(() =>
                            LevelUpWeapon(i, weaponToUpgrade.weaponUprgadeIndex));
                        Debug.Log(weaponToUpgrade.weaponUprgadeIndex);
                        upgrade.UpgradeDescription.text = weaponToUpgrade.WeaponData.NextUpgrade
                            .GetComponent<WeaponManager>().WeaponData.UpgradeDescription;
                        upgrade.UpgradeIcon.sprite = weaponToUpgrade.WeaponData.icon;
                    }

                    break;
                }
                if (WeaponSlots[i] == null)
                {
                    newWeapon = true;
                }
            }


            if (newWeapon)
            {
                Debug.Log("newWeapon");
                upgrade.UpgradeButton.onClick.AddListener(() =>
                    _player.SpawnWeapon(weaponToUpgrade.startingWeapon));
                upgrade.UpgradeDescription.text = weaponToUpgrade.WeaponData.UpgradeDescription;
                upgrade.UpgradeIcon.sprite = weaponToUpgrade.WeaponData.icon;
            }
        }
    }
}



    void ClearUpgradeOptions()
    {
        foreach (var upgrade in UIOptions)
        {
            upgrade.UpgradeButton.onClick.RemoveAllListeners(); // Clear all buttons' listener and chosen upgrade
            DisableUpgradeUI(upgrade);
        }
    }


    public void RemoveAndAddUpgrades()
    {
        ClearUpgradeOptions(); // Clear all listeners and upgrade the weapon
        UpgradeOption();
    }

    void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.UpgradeName.transform.parent.gameObject.SetActive(false);
    }
    void EnagleUpgradeUI(UpgradeUI ui)
    {
        ui.UpgradeName.transform.parent.gameObject.SetActive(true);
    }
}