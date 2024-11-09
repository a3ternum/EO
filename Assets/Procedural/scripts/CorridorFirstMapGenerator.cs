using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class CorridorFirstMapGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private int corridorLength = 10;
    [SerializeField]
    private int corridorCount = 5;
    [Range(0.02f, 1.0f)]
    [SerializeField]
    private float roomPercent = 0.1f;
    

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        floorPositions.UnionWith(roomPositions);

        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        for (int i=0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgo.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            floorPositions.UnionWith(corridor);
            potentialRoomPositions.Add(currentPosition);
        }
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRooms)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRooms.Count * roomPercent);

        List<Vector2Int> RoomToCreate = potentialRooms.OrderBy(x => Random.value).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in RoomToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

}
