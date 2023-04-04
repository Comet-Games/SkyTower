using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestGenerator : MonoBehaviour
{
    public GameObject[] corridorPrefabs;
    public int numCorridors;

    private List<GameObject> spawnedCorridors = new List<GameObject>();

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        // Spawn the first corridor
        GameObject firstCorridor = Instantiate(corridorPrefabs[0], transform.position, Quaternion.identity);
        spawnedCorridors.Add(firstCorridor);

        // Generate the remaining corridors
        for (int i = 1; i < numCorridors; i++)
        {
            // Get the exit transform of the previous corridor
            Transform previousExit = spawnedCorridors[i - 1].GetComponent<Corridor>().exit;

            // Randomly choose a new corridor prefab that has the same entrance rotation as the previous corridor's exit
            GameObject newCorridor = null;
            while (newCorridor == null)
            {
                GameObject randomPrefab = corridorPrefabs[Random.Range(0, corridorPrefabs.Length)];
                Transform entrance = randomPrefab.GetComponent<Corridor>().entrance;
                if (entrance.rotation == previousExit.rotation)
                {
                    newCorridor = randomPrefab;
                }
            }

            // Get the entrance transform of the new corridor
            Transform newEntrance = newCorridor.GetComponent<Corridor>().entrance;

            // Position the new corridor based on the exit transform of the previous corridor
            Vector3 newPosition = previousExit.position - newEntrance.localPosition;

            // Instantiate the new corridor
            GameObject instantiatedCorridor = Instantiate(newCorridor, newPosition, Quaternion.identity);

            // Check if the new corridor collides with any of the previously spawned corridors, apart from the previous corridor
            BoxCollider2D[] overlappingColliders = new BoxCollider2D[10];
            ContactFilter2D filter = new ContactFilter2D();
            filter.useTriggers = false;
            filter.SetLayerMask(LayerMask.GetMask("Walls")); // Set the layer(s) to consider for the overlap check
            int numOverlaps = instantiatedCorridor.GetComponent<Corridor>().colliderObj.OverlapCollider(filter, overlappingColliders);
            for (int j = 0; j < numOverlaps; j++)
            {
                GameObject overlappingCorridor = overlappingColliders[j].gameObject;
                if (overlappingCorridor != spawnedCorridors[i - 1] && spawnedCorridors.Contains(overlappingCorridor))
                {
                    // Destroy the newly spawned corridor if it collides with any of the previously spawned corridors, apart from the previous corridor
                    Destroy(instantiatedCorridor);
                    Debug.Log("Collided with " + spawnedCorridors[i].name);
                    return;

                }
                else
                {
                    // Get the bounds of the overlapping collider
                    Bounds overlappingBounds = overlappingColliders[j].bounds;

                    // Check if the instantiated corridor's bounds overlap with the overlapping collider's bounds
                    Bounds instantiatedBounds = instantiatedCorridor.GetComponent<Corridor>().colliderObj.bounds;
                    if (overlappingBounds.Intersects(instantiatedBounds))
                    {
                        // Destroy the newly spawned corridor if it collides with any previously spawned corridors, even if it's not the immediate previous one
                        Destroy(instantiatedCorridor);
                        Debug.Log("Collided with " + overlappingCorridor.name);
                        return;
                    }
                }

            }
            // Add the new corridor to the list of spawned corridors
            spawnedCorridors.Add(instantiatedCorridor);
        }
    }
}