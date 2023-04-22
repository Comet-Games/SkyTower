using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyWave[] waves;
    public float spawnDelay = 0.5f; // Delay between spawning enemies
    public float waveDelay = 0.5f; // Delay between spawning waves
    private int currentWaveIndex = 0; // Current enemy index in the array
    private float waveTimer = 0.0f; // Timer for delaying waves

    private void Start()
    {
        LoadWaves();
    }

    private void Update()
    {
        // If the wave delay timer is active, decrease it by deltaTime
        if (waveTimer > 0.0f)
        {
            waveTimer -= Time.deltaTime;
            return; // Don't continue with the update if we're still waiting for the wave timer
        }

        // Spawn enemies from the current wave
        EnemyWave currentWaveObject = waves[currentWaveIndex];
        currentWaveObject.gameObject.SetActive(true);

        // If the current wave has no more enemies, move to the next wave
        if (currentWaveObject.transform.childCount == 0)
        {
            // Reset the wave delay timer
            waveTimer = waveDelay;

            currentWaveIndex++;

            // If we've reached the end of the waves, open the doors
            if (currentWaveIndex >= waves.Length)
            {
                GetComponentInParent<EnemyRoom>().OpenTheDoors();
                return;
            }

            // Set the next wave to inactive
            waves[currentWaveIndex].gameObject.SetActive(false);
        }
    }

    private void LoadWaves()
    {
        // Get all children of the EnemyWave object
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        // Create a new list to store the Enemy objects
        List<EnemyWave> waveList = new List<EnemyWave>();

        // Loop through each child transform
        foreach (Transform childTransform in childTransforms)
        {
            // Get the Enemy component on the child object
            EnemyWave wave = childTransform.GetComponent<EnemyWave>();

            // If the child object has an Enemy component, add it to the list
            if (wave != null)
            {
                waveList.Add(wave);
            }
        }

        // Convert the list to an array
        waves = waveList.ToArray();
        foreach (EnemyWave wave in waves)
        {
            wave.gameObject.SetActive(false);
        }
        waves[0].gameObject.SetActive(true);
    }
}