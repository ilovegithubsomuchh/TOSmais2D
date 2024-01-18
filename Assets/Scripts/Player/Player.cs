using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerInventory))] // Mandatory Player's component.
public class Player : MonoBehaviour
{
    [Header("References")] public PlayerSO PlayerData;
    private PlayerInventory _playerInventory;
    public GameManager _gameManager;
    private Animator _animator;


    [Header("Inventory Variables")] public int weaponIndex;
    
    [SerializeField] [Header("Player Stats")] private float _maxlife;
    private static float _currentLife;
    private static readonly int IsDead = Animator.StringToHash("IsDead");


    private bool awake;
    [SerializeField] GameObject gameOver;

    private void Awake()
    {
        GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _playerInventory = GetComponent<PlayerInventory>(); // Including  component variables 
        SpawnBaseWeapon(PlayerData.baseWeapon); // spawn the base player weapon
        _currentLife = _maxlife;
    }

    public void TakeDamage(int dmg)
    {
        _currentLife -= dmg;
    }

    private void Update()
    {

        if (_currentLife <= 0)
        {
            _animator.SetBool(IsDead, true);
            gameOver.SetActive(true);
            Time.timeScale = 0f;
        }
        
    }
    public void SpawnBaseWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        _playerInventory.AddWeapon(weaponIndex, weapon.GetComponent<WeaponManager>());
        weaponIndex++; // once we spawned the weapon, make sur the next one goes in the next slot
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