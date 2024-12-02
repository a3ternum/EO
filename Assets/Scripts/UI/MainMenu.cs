using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("loading scene 0");
        SceneManager.LoadScene(0); 
    }
}
