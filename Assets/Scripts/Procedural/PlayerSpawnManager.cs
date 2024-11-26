using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject playerPrefab;
    public void GenerateSpawn(Vector2Int startPosition)
    {
        SpawnPlayer(startPosition);
    }

    private void SpawnPlayer(Vector2Int startPosition)
    {
        // spawn player at the start position
        Instantiate(playerPrefab, new Vector3(startPosition.x + 0.5f, startPosition.y + 0.5f, 0), Quaternion.identity);
    }
}
