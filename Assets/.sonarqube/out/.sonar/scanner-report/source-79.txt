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
    [Range(1, 3)]
    [SerializeField]
    private int corridorBrushSize = 1;

    private HashSet<Vector2Int> floorPositions;

    public override void RunProceduralGeneration()
    {
        floorPositions = CorridorFirstGeneration();

        // spawn player
        mapGenerator.playerSpawnManager.GenerateSpawn(startPosition);

        // bake navmesh
        mapGenerator.navMeshManager.GenerateSurface();

        // spawn mobs on the floor
        mapGenerator.enemySpawnManager.GenerateSpawns(floorPositions, new List<Vector2Int>(), new List<Vector2Int>(), "CorridorFirst");

    }

    private HashSet<Vector2Int> CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnds(deadEnds, roomPositions);
        floorPositions.UnionWith(roomPositions);

        if (corridorBrushSize == 2)
        {
            for (int i = 0; i < corridors.Count; i++)
            {
                corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
                floorPositions.UnionWith(corridors[i]);     
            }
        }
        if (corridorBrushSize == 3)
        {
            for (int i = 0; i < corridors.Count; i++)
            {
                corridors[i] = IncreaseCorridorBrush3By3(corridors[i]);
                floorPositions.UnionWith(corridors[i]);
            }
        }

        tileMapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);

        return floorPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgo.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count - 1];
            floorPositions.UnionWith(corridor);
            potentialRoomPositions.Add(currentPosition);
        }
        return corridors;
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

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            if (CountNeighbours(floorPositions, position) == 1)
            {
                deadEnds.Add(position);
            }
        }
        return deadEnds;
    }

    private int CountNeighbours(HashSet<Vector2Int> floorPositions, Vector2Int position)
    {
        int neighbourCount = 0;
        foreach (var direction in Direction2D.cardinalDirectionsList)
        {
            if (floorPositions.Contains(position + direction))
            {
                neighbourCount++;
            }
        }
        return neighbourCount;
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (!roomFloors.Contains(position))
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    public List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;
        for (int i = 1; i < corridor.Count; i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
            if(previousDirection != Vector2Int.zero && directionFromCell != previousDirection)
            {
                // handle corner
                for (int x = -1; x < 2; x++) 
                { 
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }
                previousDirection = directionFromCell;
            }
            else
            {
                // add a single cell in the direction + 90 degrees
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }
        }
        return newCorridor;

    }

    public List<Vector2Int> IncreaseCorridorBrush3By3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for (int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }


    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
            return Vector2Int.right;
        if (direction == Vector2Int.right)
            return Vector2Int.down;
        if (direction == Vector2Int.down)
            return Vector2Int.left;
        if (direction == Vector2Int.left)
            return Vector2Int.up;
        return Vector2Int.zero;
    }


}