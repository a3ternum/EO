using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject packSpawnPoint;
    [SerializeField] private int minSpawnDistance = 3;
    [SerializeField] private int packDensity = 1;
    [SerializeField] private Vector2Int playerStartPosition = Vector2Int.zero;
    [SerializeField] private int minPlayerDistance = 10;
    [SerializeField] private float spawnProbability = 0.05f;

    public void GenerateSpawns(HashSet<Vector2Int> floorTiles, List<Vector2Int> roomCenters, List<Vector2Int> corridorTiles, string mapType)
    {
        switch (mapType)
        {
            case "RandomWalk":
                SpawnRandomlyOnFloor(floorTiles, playerStartPosition, minPlayerDistance, minSpawnDistance);
                break;
            case "RoomsFirst":
                SpawnInRooms(roomCenters);
                SpawnInCorridors(corridorTiles);
                break;
            case "CorridorFirst":
                SpawnInRooms(roomCenters);
                SpawnInCorridors(corridorTiles);
                break;
        }
    }

    private void SpawnRandomlyOnFloor(HashSet<Vector2Int> floorTiles, Vector2Int StartPosition, int minPlayerDistance, int minSpawnDistance)
    {
        HashSet<Vector2Int> spawnPoints = new HashSet<Vector2Int>();
        // determine the eligible spawn points

        foreach (var tile in floorTiles)
        {
            if (IsFarEnoughFromOthers(tile, spawnPoints, minSpawnDistance) && IsFarEnoughFromPosition(tile, StartPosition, minPlayerDistance))
            {
                if (Random.value < spawnProbability * packDensity)
                {
                    spawnPoints.Add(tile);
                    SpawnEnemyPack(tile, packSpawnPoint);
                }
                
            }
        }
    }

    // update these methods to spawn enemies in rooms and corridors properly
    private void SpawnInRooms(List<Vector2Int> roomCenters)
    {
        foreach (var roomCenter in roomCenters)
        {
            Vector2Int spawnPoint = roomCenter + new Vector2Int(Random.Range(-1, 1), Random.Range(-1, 1));
            SpawnEnemyPack(spawnPoint, packSpawnPoint);
        }
    }

    private void SpawnInCorridors(List<Vector2Int> corridorTiles)
    {
        HashSet<Vector2Int> spawnPoints = new HashSet<Vector2Int>();
        foreach (var tile in corridorTiles)
        {
            if (IsFarEnoughFromOthers(tile, spawnPoints, minSpawnDistance))
            {
                spawnPoints.Add(tile);
                SpawnEnemyPack(tile, packSpawnPoint);
            }
        }
    }

    private bool IsFarEnoughFromOthers(Vector2Int point, HashSet<Vector2Int> points, int minDistance)
    {
        foreach (var existingPoint in points)
        {
            if (Vector2Int.Distance(point, existingPoint) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsFarEnoughFromPosition(Vector2Int point, Vector2Int position, int minDistance)
    {
        return Vector2Int.Distance(point, position) > minDistance;
    }

    private void SpawnEnemyPack(Vector2Int position, GameObject packSpawn)
    { 
        Instantiate(packSpawn, new Vector3(position.x, position.y, 0), Quaternion.identity);   
    }
}
