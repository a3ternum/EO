using UnityEngine;

public class RespawnMenu : MonoBehaviour
{
   

    [SerializeField]
    private GameObject respawnMenu;

 
    private void Start()
    {
        if (respawnMenu != null)
        {
            respawnMenu.SetActive(false); // Ensure the Respawn Menu panel is hidden initially
        }
        else
        {
            Debug.LogError("Respawn Menu not assigned in the Inspector!");
        }
    }

    public void ShowRespawnMenu()
    {
        if (respawnMenu != null)
        {
            respawnMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("Respawn Menu not assigned in the Inspector!");
        }
        // Time.timeScale = 0f; // Pause the game
    }


    public void RespawnPlayer()
    {
        // load the scene with index 0
        Debug.Log("loading scene 0");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
