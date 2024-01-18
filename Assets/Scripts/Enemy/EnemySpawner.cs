using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //A list of groups of enemies to spawn
        public int waveQuota; //The total number of enemies to spawn in this wave
        public float spawnInterval;
        public int spawnCount; //The number of enemies already spawned in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; // The number of enemies to spawn in this wave
        public int spawnCount; //The number of enemies of this type already spawned in the wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; // List of all the waves in the game
    public int currentWaveCount;
    public GameManager GameManager; // The index of current wave

    [Header("Spawner Attributes")] private float spawnTimer; //Timer use to determine when to spawn the next enemy

    private Transform player;
    private BaseEnemy _baseEnemy;
    private int test = 0;

    [SerializeField] private float MinSpawnX;
    [SerializeField]private float MaxSpawnX;
    [SerializeField]private float MinSpawnY;
    [SerializeField]private float MaxSpawnY;

    public GameObject victoryMenuUI;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        CalculateWaveQuota();
        test = waves[currentWaveCount].waveQuota;
    }

    void Update()
    {
        Debug.Log(BaseEnemy.CountKilledEnemy());
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
        
        if (BaseEnemy.CountKilledEnemy() == test)
        {
            GameManager.LevelUp();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
    }

    void SpawnEnemies()
    {
        // Check if the minimum number of enemies in the wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            //Spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //Check if the minimum number of enemies of this type have been spwned
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    Vector2 spawnPosition = new Vector2(player.transform.position.x + Random.Range(MinSpawnX, MaxSpawnX),
                        player.transform.position.y + Random.Range(MinSpawnY, MaxSpawnY));
                    Instantiate(enemyGroup.enemyPrefab, spawnPosition, quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                }
            }
        }
    }

    void ChangeWave()
    
    { if (waves[currentWaveCount].spawnCount == waves[currentWaveCount].waveQuota)
        {
            if (currentWaveCount == 9)
            {
                victoryMenuUI.SetActive(true);
            }
            else
            {
                currentWaveCount++;
                CalculateWaveQuota();
                test += waves[currentWaveCount].waveQuota;  
            }
                
           
        }
    }
}