using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.AI;
public class EnemySpawnManager : MonoBehaviour
{
    public PackSpawn packSpawn; // maybe turn this into a resource? 

    public EnemySpawnTable enemySpawnTable;



    public float packDensity = 1;
    public float packSize = 5;
    public Vector2Int playerStartPosition;
    [SerializeField]
    private float minPlayerDistance;

    private float minSpawnDistance = 2; // minimum distance between two spawn points
    private float spawnProbability = 0.01f; // fixed value that determines the probability of a pack spawning at a given point
    private HashSet<Vector2Int> floorPositions;


    private void Awake()
    {
        // set packSpawn parameters
        packSpawn.enemySpawnTable = enemySpawnTable;
        // this will later depend on things like map Quality, map density etc

    }


    public void GenerateSpawns(HashSet<Vector2Int> floorTiles, List<Vector2Int> roomCenters, List<Vector2Int> corridorTiles, string mapType)
    {
        floorPositions = floorTiles;
        switch (mapType)
        {
            case "RandomWalk":
                SpawnRandomlyOnFloor(floorPositions, playerStartPosition, minPlayerDistance, minSpawnDistance);
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

    private void SpawnRandomlyOnFloor(HashSet<Vector2Int> floorTiles, Vector2Int StartPosition, float minPlayerDistance, float minSpawnDistance)
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
                    SpawnEnemyPack(tile, packSpawn);
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
            SpawnEnemyPack(spawnPoint, packSpawn);
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
                SpawnEnemyPack(tile, packSpawn);
            }
        }
    }

    private bool IsFarEnoughFromOthers(Vector2Int point, HashSet<Vector2Int> points, float minDistance)
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

    private bool IsFarEnoughFromPosition(Vector2Int point, Vector2Int position, float minDistance)
    {
        return Vector2Int.Distance(point, position) > minDistance;
    }

    private void SpawnEnemyPack(Vector2Int position, PackSpawn packSpawn)
    {
        Vector3 spawnPosition = new Vector3(position.x, position.y, 0); 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            PackSpawn pack = Instantiate(packSpawn, spawnPosition, Quaternion.identity);
            pack.packSize = packSize;
        }
        else
        {
            Debug.LogWarning("spawn point is out of navMeshSurface");
        }

    }
}
