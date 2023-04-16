using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] waves;
    public float spawnDelay = 1.0f; // Delay between spawning enemies
    private int currentWaveIndex = 0; // Current enemy index in the array

    private void Awake()
    {
    }

    private void Update()
    {
        GameObject currentWaveObject = waves[currentWaveIndex];
        currentWaveObject.SetActive(true);
        if(currentWaveObject.transform.childCount == 0)
        {
            currentWaveIndex++;
        }

        if(currentWaveIndex >= waves.Length)
        {
            GetComponentInParent<EnemyRoom>().OpenTheDoors();
        }
    }
}
