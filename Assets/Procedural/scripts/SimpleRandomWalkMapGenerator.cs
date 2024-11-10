using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class SimpleRandomWalkMapGenerator : AbstractMapGenerator
{
    [SerializeField]
    protected SimpleRandomWalkData randomWalkParameters;

    [SerializeField]
    protected MapGenerator mapGenerator;

    public override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        tileMapVisualizer.ClearMap();
        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);

        // spawn player
        mapGenerator.playerSpawnManager.GenerateSpawn(startPosition);

        // bake navmesh
        mapGenerator.navMeshManager.GenerateSurface();

        // spawn mobs on the floor
        mapGenerator.enemySpawnManager.GenerateSpawns(floorPositions, new List<Vector2Int>(), new List<Vector2Int>(), "RandomWalk");
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData randomWalkParameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < randomWalkParameters.iterations; i++)
        {
            
            var path = ProceduralGenerationAlgo.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
            floorPositions.UnionWith(path);
            if (randomWalkParameters.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }

     
  
}
