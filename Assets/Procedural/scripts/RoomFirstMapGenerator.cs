using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstMapGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private int minRoomWidth = 5;
    [SerializeField]
    private int minRoomHeight = 5;

    [SerializeField]
    private int mapWidth = 20;
    [SerializeField]
    private int mapHeight = 20;

    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;

    [Range(1, 3)]
    [SerializeField]
    private int corridorBrushSize = 1;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgo.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, 
            new Vector3Int(mapWidth, mapHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }
        

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);

        if (corridorBrushSize == 2)
        { 
            corridors = IncreaseCorridorSizeByOne(corridors);
            floor.UnionWith(corridors);
        }

        if (corridorBrushSize == 3)
        {
            corridors = IncreaseCorridorBrush3By3(corridors);
            floor.UnionWith(corridors);
        }

        floor.UnionWith(corridors);

        tileMapVisualizer.ClearMap();
        tileMapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tileMapVisualizer);

    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int x = offset; x < room.size.x - offset; x++)
            {
                for (int y = offset; y < room.size.y - offset; y++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(x, y);
                    floor.Add(position);
                }
            }
            // if we want to decorate the rooms procedurally we would have to save each room individually into a separate hashset.
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closestRoom = FindClosestPoint(currentRoomCenter, roomCenters);
            roomCenters.Remove(closestRoom);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closestRoom);
            currentRoomCenter = closestRoom;
            corridors.UnionWith(newCorridor);
        }

        return corridors;

    }

    private Vector2Int FindClosestPoint(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float length = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2Int.Distance(position, currentRoomCenter);
            if (currentDistance < length)
            {
                length = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);

        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomsBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomsBounds.center.x), Mathf.RoundToInt(roomsBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);

            foreach (var position in roomFloor)
            {
                if (position.x >= (roomsBounds.min.x + offset) && position.x <= (roomsBounds.max.x - offset) &&
                    position.y >= (roomsBounds.min.y + offset) && position.y <= (roomsBounds.max.y - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    public HashSet<Vector2Int> IncreaseCorridorSizeByOne(HashSet<Vector2Int> corridor)
    {
        HashSet<Vector2Int> newCorridor = new HashSet<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;
        List<Vector2Int> corridorList = new List<Vector2Int>(corridor);  // Convert to List to enable indexing

        for (int i = 1; i < corridorList.Count; i++)
        {
            Vector2Int directionFromCell = corridorList[i] - corridorList[i - 1];

            if (previousDirection != Vector2Int.zero && directionFromCell != previousDirection)
            {
                // handle corner
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridorList[i - 1] + new Vector2Int(x, y));
                    }
                }
                previousDirection = directionFromCell;
            }
            else
            {
                // add a single cell in the direction + 90 degrees
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridorList[i - 1]);
                newCorridor.Add(corridorList[i - 1] + newCorridorTileOffset);
            }

            previousDirection = directionFromCell;
        }
        return newCorridor;
    }

    public HashSet<Vector2Int> IncreaseCorridorBrush3By3(HashSet<Vector2Int> corridor)
    {
        HashSet<Vector2Int> newCorridor = new HashSet<Vector2Int>();
        List<Vector2Int> corridorList = new List<Vector2Int>(corridor);  // Convert to List to enable indexing

        for (int i = 1; i < corridorList.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridorList[i - 1] + new Vector2Int(x, y));
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
