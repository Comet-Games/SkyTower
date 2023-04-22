using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    private Enemy[] enemies; // Array of Enemy objects
    private EnemyData[] enemyData; // Array of EnemyData objects

    private void Awake()
    {
        LoadEnemyData();
        LoadEnemies();
        AssignEnemyData();
    }

    private void LoadEnemyData()
    {
        // Load all EnemyData objects from the Resources/EnemyData folder
        Object[] loadedObjects = Resources.LoadAll("EnemyData", typeof(EnemyData));

        // Convert the loadedObjects array to an EnemyData array
        enemyData = new EnemyData[loadedObjects.Length];
        for (int i = 0; i < loadedObjects.Length; i++)
        {
            enemyData[i] = (EnemyData)loadedObjects[i];
            Debug.Log(enemyData[i].enemyName);
        }
    }

    private void LoadEnemies()
    {
        // Get all children of the EnemyWave object
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        // Create a new list to store the Enemy objects
        List<Enemy> enemyList = new List<Enemy>();

        // Loop through each child transform
        foreach (Transform childTransform in childTransforms)
        {
            // Get the Enemy component on the child object
            Enemy enemy = childTransform.GetComponent<Enemy>();

            // If the child object has an Enemy component, add it to the list
            if (enemy != null)
            {
                enemyList.Add(enemy);
            }
        }

        // Convert the list to an array
        enemies = enemyList.ToArray();
    }

    private void AssignEnemyData()
    {
        // Loop through each Enemy object in the enemies array
        foreach (Enemy enemy in enemies)
        {
            // Get a random index for the enemyData array
            int randomIndex = Random.Range(0, enemyData.Length);

            // Get the EnemyData object at the random index
            EnemyData data = enemyData[randomIndex];

            // Set the Enemy object's data based on the EnemyData object
            enemy.enemyData = data;
        }
    }
}