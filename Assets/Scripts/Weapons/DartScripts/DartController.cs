using System.Collections;
using UnityEngine;

public class DartController : WeaponManager
{
    private int dartToSpawn; // Counter to keep track of the spawned darts
    private GameObject Dart; // Reference to the Dart GameObject
    private bool isAttacking = false; // Flag to check if an attack is in progress
    private float cooldownDuration = 0.4f; // Cooldown duration between individual spawns

    // Override the Update method from the base class (WeaponManager)
    protected override void Update()
    {
        base.Update(); // Call the Update method of the base class

        // Check if an attack is not in progress
        if (!isAttacking)
        {
            // Trigger the DartSpawnCoroutine when an attack is not in progress
            StartCoroutine(DartSpawnCoroutine());
        }
    }

    // Coroutine to handle the dart spawning with cooldown
    private IEnumerator DartSpawnCoroutine()
    {
        // Set the flag to indicate that an attack is in progress
        isAttacking = true;

        // Loop to spawn darts based on the specified NumberToSpawn in WeaponData
        for (int i = 0; i < WeaponData.NumberToSpawn; i++)
        {
            // Calculate a random spawn position in front of the player
            float randomX = transform.position.x; // Keep the same X position as the player
            float randomY =
                transform.position.y + UnityEngine.Random.Range(-2f, 2f); // Randomize Y position within a range

            // Instantiate a dart at the calculated spawn position with no rotation
            Dart = Instantiate(WeaponData.prefab, new Vector3(randomX, randomY, 0), Quaternion.identity);

            // Increment the dartToSpawn counter
            dartToSpawn++;

            // Wait for the specified cooldown duration before the next spawn
            yield return new WaitForSeconds(cooldownDuration);
        }
    }

    // Override the base Attack method to include dart spawning with cooldown
    protected override void Attack()
    {
        base.Attack();
        isAttacking = false;
        if (!isAttacking)
        {
            StartCoroutine(DartSpawnCoroutine());
        }
    }
}