using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Player player;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private PlayerSpawnManager playerSpawnManager;


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
        playerData.resetPlayerData();
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void EnterMap()
    {

        isInMap = true;
        SceneManager.LoadScene("ProceduralMap"); // Load the map scene
        // destroy the player object
        Destroy(GameObject.FindGameObjectWithTag("Player"));


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
