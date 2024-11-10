using UnityEngine;
using UnityEngine.SceneManagement;
public class StairsInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with stairs");
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("ProceduralMap");
        }
    }
}
