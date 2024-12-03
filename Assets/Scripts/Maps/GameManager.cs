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

    [SerializeField]
    private EnemySpawnManager enemySpawnManager;

    [SerializeField]
    private MapGenerator mapGenerator;

    [SerializeField]
    private NavMeshManager navMeshManager;

    public bool isInMap = false;

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

    public void EnterMap(Map map)
    {

        isInMap = true;
        SceneManager.LoadScene("ProceduralMap"); // Load the map scene
        // destroy the player object
        //Destroy(GameObject.FindGameObjectWithTag("Player"));


        // set the properties of the map generator
        Debug.Log("map generation type is " + map.generationType);
        Debug.Log("mapgeneration generation type is " + mapGenerator.generationType);
        mapGenerator.generationType = map.generationType;

        // set the properties of the enemy spawn manager
        enemySpawnManager.packSize = map.packSize;
        enemySpawnManager.packDensity = map.packDensity;
        enemySpawnManager.enemySpawnTable = map.enemySpawnTable;

        // Generate the map
        mapGenerator.GenerateMap();


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
