using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Loading Hideout");
        SceneManager.LoadScene("Hideout"); 
    }
}
