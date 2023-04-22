using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyWave[] waves;
    public float spawnDelay = 1.0f; // Delay between spawning enemies
    private int currentWaveIndex = 0; // Current enemy index in the array

    private void Start()
    {
        LoadWaves();
    }

    private void Update()
    {
        EnemyWave currentWaveObject = waves[currentWaveIndex];
        currentWaveObject.gameObject.SetActive(true);
        if(currentWaveObject.transform.childCount == 0)
        {
            currentWaveIndex++;
        }

        if(currentWaveIndex >= waves.Length)
        {
            GetComponentInParent<EnemyRoom>().OpenTheDoors();
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
