using Edgar.Unity;
using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Edgar/Julian/NavMesh post-processing", fileName = "NavMeshPostProcessing")]
public class NavMeshPostProcess : DungeonGeneratorPostProcessingGrid2D
{
    public GameObject navmeshPrefab;
    public override void Run(DungeonGeneratorLevelGrid2D level)
    {
        AssignNavmesh(level);
    }

    public void AssignNavmesh(DungeonGeneratorLevelGrid2D level)
    {
        var tilemaps = level.GetSharedTilemaps();
        var tilemapsRoot = level.RootGameObject.transform.Find(GeneratorConstantsGrid2D.TilemapsRootName);
        var floor = tilemapsRoot.Find("Floor");
        var navmesh = floor.gameObject.AddComponent<NavMeshPlus.Components.NavMeshModifier>();
        navmesh.overrideArea = true;
        navmesh.area = 0;
        var walls = tilemapsRoot.Find("Walls");
        var navmesh1 = walls.gameObject.AddComponent<NavMeshPlus.Components.NavMeshModifier>();
        navmesh1.overrideArea = true;
        navmesh1.area = 1;
        navmeshPrefab = FindObjectOfType<NavMeshSurface>().gameObject;
        //Instantiate(navmeshPrefab);
        NavMeshSurface navSurface = navmeshPrefab.GetComponent<NavMeshPlus.Components.NavMeshSurface>();
        navSurface.BuildNavMeshAsync();
    }
}
