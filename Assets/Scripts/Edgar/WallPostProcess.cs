using Edgar.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Edgar/Julian/Wall post-processing", fileName = "WallPostProcessing")]
public class WallPostProcess : DungeonGeneratorPostProcessingGrid2D
{
    public override void Run(DungeonGeneratorLevelGrid2D level)
    {
        AssignWalls(level);
    }

    public void AssignWalls(DungeonGeneratorLevelGrid2D level)
    {
        var tilemaps = level.GetSharedTilemaps();
        var tilemapsRoot = level.RootGameObject.transform.Find(GeneratorConstantsGrid2D.TilemapsRootName);
        var walls = tilemapsRoot.Find("Walls");
        walls.tag = "Wall";
    }
}
