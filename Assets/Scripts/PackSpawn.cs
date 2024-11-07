using System.Collections;
using UnityEngine;

public class PackSpawn : MonoBehaviour
{
    // ideally what we want to do is have a list of enemies that we can spawn, and then we can randomly select one of them to spawn

    [SerializeField]
    private GameObject enemy;
    

    public int packSize;
    public float spawnRadius = 1.5f;

    private void Start()
    {
        packSize = Random.Range(1, 5);
        spawnPack();
    }

    public void spawnPack()
    {
        for (int i = 0; i < packSize; i++)
        {
            Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-spawnRadius, spawnRadius), transform.position.y + Random.Range(-spawnRadius, spawnRadius));
            Instantiate(enemy, spawnPosition, Quaternion.identity);

            

            
        }
    }
}
