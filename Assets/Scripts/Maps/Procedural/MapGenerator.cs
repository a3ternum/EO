using UnityEngine;
using YourNamespace.Maps;

public class MapGenerator : MonoBehaviour
{
    public GenerationType generationType;

    [SerializeField]
    private AbstractMapGenerator[] mapGenerators;

    public PlayerSpawnManager playerSpawnManager;
    public EnemySpawnManager enemySpawnManager;
    public NavMeshManager navMeshManager;

    private SimpleRandomWalkMapGenerator randomWalkMapGenerator;
    private CorridorFirstMapGenerator corridorFirstMapGenerator;
    private RoomFirstMapGenerator roomFirstMapGenerator;


    public void GenerateMap()
    {
        randomWalkMapGenerator = mapGenerators[0].GetComponent<SimpleRandomWalkMapGenerator>();
        corridorFirstMapGenerator = mapGenerators[1].GetComponent<CorridorFirstMapGenerator>();
        roomFirstMapGenerator = mapGenerators[2].GetComponent<RoomFirstMapGenerator>();



        // Generate the map based on the selected generation type
        switch (generationType)
        {
            case GenerationType.RandomWalk:
                randomWalkMapGenerator.RunProceduralGeneration();
                break;
            case GenerationType.CorridorFirst:
                corridorFirstMapGenerator.RunProceduralGeneration();
                break;
            case GenerationType.RoomFirst:
                roomFirstMapGenerator.RunProceduralGeneration();
                break;
        }
    }

}
