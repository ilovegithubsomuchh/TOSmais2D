using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerInventory : MonoBehaviour
{
    // Weapon slots and player reference
    public List<WeaponManager> WeaponSlots = new List<WeaponManager>(4);
    private Player _player;
    public GameManager _gameManager;

    // Serializable classes for weapon upgrades, UI, and available weapons
    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
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

    // Lists to store weapon upgrades, UI elements, and available weapons
    public List<WeaponUpgrade> UpgradeOptions = new List<WeaponUpgrade>();
    public List<UpgradeUI> UIOptions = new List<UpgradeUI>();
    public List<WeaponList> Weapons = new List<WeaponList>();

    // Initialization on start
    private void Start()
    {
        InitializeWeaponUpgrades();
    }

    // Initialize weapon upgrades based on available weapon slots and weapons
    private void InitializeWeaponUpgrades()
    {
        _player = GetComponent<Player>();

        for (int i = 0; i < WeaponSlots.Count; i++)
        {
            if (WeaponSlots[i] != null)
            {
                UpgradeOptions.Add(new WeaponUpgrade
                {
                    weaponUpgradeIndex = i,
                    startingWeapon = WeaponSlots[i].WeaponData.prefab,
                    WeaponData = WeaponSlots[i].WeaponData
                });
            }
        }

        // Add additional upgrades for available weapons
        for (int i = UpgradeOptions.Count; i < WeaponSlots.Count; i++)
        {
            UpgradeOptions.Add(new WeaponUpgrade
            {
                weaponUpgradeIndex = i,
                startingWeapon = Weapons[i - UpgradeOptions.Count].Weapon,
                WeaponData = Weapons[i - UpgradeOptions.Count].WeaponData
            });
        }
    }

    // Add a new weapon to a specified slot index
    public void AddWeapon(int slotIndex, WeaponManager weapon)
    {
        WeaponSlots[slotIndex] = weapon;
    }

    // Level up a weapon in the specified slot index
    public void LevelUpWeapon(int slotIndex, int upgradeIndex)
    {
        if (IsValidSlotIndex(slotIndex))
        {
            WeaponManager weaponManager = WeaponSlots[slotIndex];
            GameObject upgradedWeapon = Instantiate(weaponManager.WeaponData.NextUpgrade, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);

            // Replace the existing weapon with the upgraded one
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<WeaponManager>());
            Destroy(transform.GetChild(slotIndex).gameObject);

            // Update the weapon data in the upgrade options
            UpgradeOptions[upgradeIndex].WeaponData = upgradedWeapon.GetComponent<WeaponManager>().WeaponData;

            // Signal the end of the level-up process
            _gameManager.EndLevelUp();
        }
    }

    // Check if the slot index is valid
    private bool IsValidSlotIndex(int slotIndex)
    {
        return WeaponSlots != null && WeaponSlots.Count > slotIndex;
    }

    // Main method for handling weapon upgrades
    void UpgradeOption()
    {
        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(UpgradeOptions);

        // Iterate through UI options for displaying upgrades
        foreach (var upgrade in UIOptions)
        {
            // Get a random available weapon upgrade
            WeaponUpgrade weaponToUpgrade = GetRandomUpgrade(availableWeaponUpgrades);

            if (weaponToUpgrade != null)
            {
                // Enable UI for the current upgrade option
                EnableUpgradeUI(upgrade);

                // Check if the weapon is already in slots or a new weapon
                if (!IsWeaponInSlots(weaponToUpgrade))
                {
                    HandleNewWeapon(upgrade, weaponToUpgrade);
                }
                else if (CanUpgradeWeapon(weaponToUpgrade))
                {
                    HandleUpgradeForExistingWeapon(upgrade, weaponToUpgrade);
                }
                else
                {
                    // If the weapon cannot be upgraded, disable the UI
                    DisableUpgradeUI(upgrade);
                }
            }
        }
    }

    // Get a random available weapon upgrade and remove it from the list
    private WeaponUpgrade GetRandomUpgrade(List<WeaponUpgrade> availableWeaponUpgrades)
    {
        if (availableWeaponUpgrades.Count > 0)
        {
            int randomIndex = Random.Range(0, availableWeaponUpgrades.Count);
            WeaponUpgrade weaponToUpgrade = availableWeaponUpgrades[randomIndex];
            availableWeaponUpgrades.RemoveAt(randomIndex);
            return weaponToUpgrade;
        }

        return null;
    }

    // Check if the weapon is already in the player's weapon slots
    private bool IsWeaponInSlots(WeaponUpgrade weaponToUpgrade)
    {
        foreach (var slot in WeaponSlots)
        {
            if (slot != null && slot.WeaponData == weaponToUpgrade.WeaponData)
            {
                return true;
            }
        }

        return false;
    }

    // Check if the weapon can be upgraded
    private bool CanUpgradeWeapon(WeaponUpgrade weaponToUpgrade)
    {
        return weaponToUpgrade.WeaponData.NextUpgrade != null;
    }

    // Handle UI and listeners for upgrading an existing weapon
    private void HandleUpgradeForExistingWeapon(UpgradeUI upgrade, WeaponUpgrade weaponToUpgrade)
    {
        int slotIndex = GetWeaponSlotIndex(weaponToUpgrade);
        upgrade.UpgradeButton.onClick.AddListener(() => LevelUpWeapon(slotIndex, weaponToUpgrade.weaponUpgradeIndex));
        upgrade.UpgradeDescription.text = weaponToUpgrade.WeaponData.NextUpgrade.GetComponent<WeaponManager>().WeaponData.UpgradeDescription;
    }

    // Get the slot index of a weapon in the player's weapon slots
    private int GetWeaponSlotIndex(WeaponUpgrade weaponToUpgrade)
    {
        for (int i = 0; i < WeaponSlots.Count; i++)
        {
            if (WeaponSlots[i] != null && WeaponSlots[i].WeaponData == weaponToUpgrade.WeaponData)
            {
                return i;
            }
        }

        return -1;
    }

    // Handle UI and listeners for spawning a new weapon
    private void HandleNewWeapon(UpgradeUI upgrade, WeaponUpgrade weaponToUpgrade)
    {
        upgrade.UpgradeButton.onClick.AddListener(() => _player.SpawnWeapon(weaponToUpgrade.startingWeapon));
        upgrade.UpgradeDescription.text = weaponToUpgrade.WeaponData.UpgradeDescription;
    }

    // Method to clear upgrade options
    void ClearUpgradeOptions()
    {
        foreach (var upgrade in UIOptions)
        {
            upgrade.UpgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgrade);
        }
    }

    // Method to disable upgrade UI
    private void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.UpgradeName.transform.parent.gameObject.SetActive(false);
    }

    // Method to enable upgrade UI
    private void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.UpgradeName.transform.parent.gameObject.SetActive(true);
    }

    // Method called to remove and add upgrades
    public void RemoveAndAddUpgrades()
    {
        ClearUpgradeOptions();
        UpgradeOption();
    }
}
