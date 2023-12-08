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
                UpgradeOptions.Add(new WeaponUpgrade());
            }
        }

        foreach (var AvailableWeapon in Weapons)
        {
            UpgradeOptions.Add(new WeaponUpgrade());
        }

        for (int i = 0; i < WeaponSlots.Count; i++)
        {
            if (WeaponSlots[i] != null)
            {
                UpgradeOptions[i].weaponUprgadeIndex = i;
                UpgradeOptions[i].startingWeapon = WeaponSlots[i].WeaponData.prefab;
                UpgradeOptions[i].WeaponData = WeaponSlots[i].WeaponData;
            }
        }

        for (int i = 0; i < UpgradeOptions.Count; i++)
        {
            if (UpgradeOptions[i].WeaponData == null)
            {
                UpgradeOptions[i].weaponUprgadeIndex = i;

                UpgradeOptions[i].startingWeapon = Weapons[x].Weapon;
                UpgradeOptions[i].WeaponData = Weapons[x].WeaponData;
                x++;
            }
        }
    }


    public void AddWeapon(int slotIndex, WeaponManager weapon)
    {
        WeaponSlots[slotIndex] = weapon;
    }

    public void LevelUpWeapon(int slotIndex, int upgradeIndex)
    {
        if (WeaponSlots.Count > slotIndex && (WeaponSlots != null))
        {
            WeaponManager weaponManager = WeaponSlots[slotIndex];

            GameObject UpgradedWeapon = Instantiate(weaponManager.WeaponData.NextUpgrade, transform.position,
                Quaternion.identity);

            UpgradedWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, UpgradedWeapon.GetComponent<WeaponManager>());

            Destroy(transform.GetChild(slotIndex).gameObject);
            UpgradeOptions[upgradeIndex].WeaponData = UpgradedWeapon.GetComponent<WeaponManager>().WeaponData;
            _gameManager.EndLevelUp();
        }
    }

    void UpgradeOption()
    {
        foreach (var upgrade in UIOptions)
        {
            WeaponUpgrade WeaponToUpgrade = UpgradeOptions[Random.Range(0, UpgradeOptions.Count)];
            if (WeaponToUpgrade != null)
            {
                bool newWeapon = false;
                for (int i = 0; i < WeaponSlots.Count; i++)
                {
                    if (WeaponSlots[i] != null && WeaponSlots[i].WeaponData == WeaponToUpgrade.WeaponData)
                    {
                        newWeapon = false;
                        Debug.Log(WeaponToUpgrade.WeaponData);

                        if (!newWeapon)
                        {
                            if (!WeaponToUpgrade.WeaponData.NextUpgrade) break;
                            upgrade.UpgradeButton.onClick.AddListener(() =>
                                LevelUpWeapon(i, WeaponToUpgrade.weaponUprgadeIndex));
                            upgrade.UpgradeDescription.text = WeaponToUpgrade.WeaponData.NextUpgrade
                                .GetComponent<WeaponManager>().WeaponData.UpgradeDescription;
                        }

                        break;
                    }
                    else
                    {
                        newWeapon = true;
                    }
                }

                if (newWeapon)
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
            upgrade.UpgradeButton.onClick.RemoveAllListeners();
        }
    }


    public void RemoveAndAddUpgrades()
    {
        ClearUpgradeOptions();
        UpgradeOption();
    }
}