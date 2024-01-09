using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


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
        var x = 0;
        _player = GetComponent<Player>();
        for (int i = 0; i < WeaponSlots.Count; i++)
        {
            if (WeaponSlots[i] != null)
            {
                UpgradeOptions.Add(
                    new WeaponUpgrade()); // for each weapon in the game add in a list an entry for possible upgrades
            }
        }

        for (int i = 0; i < WeaponSlots.Count; i++)
        {
            if (WeaponSlots[i] != null)
            {
                UpgradeOptions[i].weaponUprgadeIndex = i;
                UpgradeOptions[i].startingWeapon = WeaponSlots[i].WeaponData.prefab;
                UpgradeOptions[i].WeaponData = WeaponSlots[i].WeaponData; // getting player's weapon on start and put it on the list for future upgrades
            }
        }

        for (int i = 0; i < UpgradeOptions.Count; i++)
        {
            if (UpgradeOptions[i].WeaponData == null)
            {
                UpgradeOptions[i].weaponUprgadeIndex = i;

                UpgradeOptions[i].startingWeapon = Weapons[x].Weapon;
                UpgradeOptions[i].WeaponData = Weapons[x].WeaponData; // for each other upgrade options, fill the list with all available weapons 
                x++;
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
        List<WeaponUpgrade> availableWeaponUgrades = new List<WeaponUpgrade>(UpgradeOptions);
        foreach (var upgrade in UIOptions) // Checking all possibles upgrades
        {
            WeaponUpgrade
                WeaponToUpgrade =
                    availableWeaponUgrades[Random.Range(0, UpgradeOptions.Count)]; // Choose 1 upgrade from all possibles 
            availableWeaponUgrades.Remove(WeaponToUpgrade);
            if (WeaponToUpgrade != null) // Double checking 
            {
                EnagleUpgradeUI(upgrade);
                bool newWeapon = false;
                for (int i = 0; i < WeaponSlots.Count; i++)
                {
                    if (WeaponSlots[i] != null &&
                        WeaponSlots[i].WeaponData == WeaponToUpgrade.WeaponData) // Browse through all the list 
                    {
                        if (!newWeapon) // if not a new weapon then upgrade the previous one in the specified slot index
                        {
                            if (!WeaponToUpgrade.WeaponData.NextUpgrade)
                            {
                                 DisableUpgradeUI(upgrade);
                                break;
                            }
                            upgrade.UpgradeButton.onClick.AddListener(() =>
                                LevelUpWeapon(i, WeaponToUpgrade.weaponUprgadeIndex));
                            upgrade.UpgradeDescription.text = WeaponToUpgrade.WeaponData.NextUpgrade
                                .GetComponent<WeaponManager>().WeaponData.UpgradeDescription; 
                        }

                        break;
                    }
                    else
                    {
                        newWeapon = true; // If not the it must be a new weapon
                    }
                }

                if (newWeapon) // if it's a new weapon, you add a new weapon 
                {
                    upgrade.UpgradeButton.onClick.AddListener(() =>
                        _player.SpawnWeapon(WeaponToUpgrade.startingWeapon));
                    upgrade.UpgradeDescription.text = WeaponToUpgrade.WeaponData.UpgradeDescription;
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