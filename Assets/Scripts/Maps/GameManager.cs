using Codice.Client.BaseCommands;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Player player;

    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    private PlayerSpawnManager playerSpawnManager;

    private EnemySpawnManager enemySpawnManager;

    private MapGenerator mapGenerator;

    private NavMeshManager navMeshManager;

    public bool isInMap = false;

    private Map map;

    // Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        playerStats.resetPlayerData();
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);

        
    }

    public void EnterMap(Map mapToEnter)
    {

        isInMap = true;
        map = mapToEnter;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("ProceduralMap"); // Load the map scene
                                                 // destroy the player object
    }
      private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ProceduralMap")
        {
            // Unsubscribe from the event
            SceneManager.sceneLoaded -= OnSceneLoaded;

            // Initialize spawn managers and other components
            enemySpawnManager = FindFirstObjectByType<EnemySpawnManager>();
            if (enemySpawnManager == null)
            {
                Debug.LogError("enemySpawnManager is null");
            }
            playerSpawnManager = FindFirstObjectByType<PlayerSpawnManager>();
            if (playerSpawnManager == null)
            {
                Debug.LogError("playerSpawnManager is null");
            }
            mapGenerator = FindFirstObjectByType<MapGenerator>();
            if (mapGenerator == null)
            {
                Debug.LogError("mapGenerator is null");
            }
            navMeshManager = FindFirstObjectByType<NavMeshManager>();
            if (navMeshManager == null)
            {
                Debug.LogError("navMeshManager is null");
            }

            // Set the properties of the map generator
            mapGenerator.generationType = map.generationType;

            // Set the properties of the enemy spawn manager
            enemySpawnManager.packSize = map.packSize;
            enemySpawnManager.packDensity = map.packDensity;
            enemySpawnManager.enemySpawnTable = map.enemySpawnTable;

            // Generate the map
            mapGenerator.GenerateMap();
        }
 

}
    public void ReturnToHideout()
    {
        isInMap = false;
        SceneManager.LoadScene("HideoutScene"); // Load the hideout scene

        // Call method to move the player after the scene has loaded
        SpawnPlayerInHideout();
    }

    private void SpawnPlayerInHideout()
    {
        // Wait until the new scene is loaded
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "HideoutScene" && player != null)
            {
                // Set the player's position to (0, 0, 0)
                Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
            }
        };
    }

    private void LoadPlayerData()
    {
        // Load player data from ScriptableObject or Save System
        // This ensures the player’s data persists across scenes.
    }

    private void SavePlayerData()
    {
        // Save player data (experience, stats, inventory, etc.) to ScriptableObject or Save System
    }

}
