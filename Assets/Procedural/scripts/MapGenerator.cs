using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    enum GenerationType
    {
        RandomWalk,
        CorridorFirst,
        RoomFirst
    }

    [SerializeField]
    private GenerationType generationType;

    [SerializeField]
    private AbstractMapGenerator[] mapGenerators;

    [SerializeField]
    public PlayerSpawnManager playerSpawnManager;
    [SerializeField]
    public EnemySpawnManager enemySpawnManager;
    [SerializeField]
    public NavMeshManager navMeshManager;

    private SimpleRandomWalkMapGenerator randomWalkMapGenerator;
    private CorridorFirstMapGenerator corridorFirstMapGenerator;
    private RoomFirstMapGenerator roomFirstMapGenerator;

    
    private void Start()
    {
        randomWalkMapGenerator = mapGenerators[0].GetComponent<SimpleRandomWalkMapGenerator>();
        corridorFirstMapGenerator = mapGenerators[1].GetComponent<CorridorFirstMapGenerator>();
        roomFirstMapGenerator = mapGenerators[2].GetComponent<RoomFirstMapGenerator>();

   

        // Generate the map based on the selected generation type
        switch (generationType)
        {
            case GenerationType.RandomWalk:
                Debug.Log("running random walk");
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
