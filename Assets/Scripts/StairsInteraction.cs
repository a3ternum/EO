using UnityEngine;
using UnityEngine.SceneManagement;
public class StairsInteraction : MonoBehaviour
{
    private static GameManager Instance;

    private void Start()
    {
        Instance = GameManager.Instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instance.EnterMap();
        }
    }
}
