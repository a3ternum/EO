using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class PackSpawn : MonoBehaviour
{
    // ideally what we want to do is have a list of enemies that we can spawn, and then we can randomly select one of them to spawn

    [SerializeField]
    private Enemy[] enemies;
    

    public int packSize;
    public float spawnRadius = 1.5f;

    private void Start()
    {
        packSize = Random.Range(1, packSize);
        spawnPack();
    }

    public void spawnPack()
    {
        NavMeshHit hit;
        int attempts = 0;

        // choose an enemy type from the array of enemies
        int enemyIndex = Random.Range(0, enemies.Length);
        for (int i = 0; i < packSize; i++)
        {

            // spawn the enemy at a random position within the spawn radius
            // if spawn location is not valid, try again
            bool invalidSpawnPosition = true;
            
            while (invalidSpawnPosition && attempts<10)
            {
                attempts += 1;
                Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-spawnRadius, spawnRadius), transform.position.y + Random.Range(-spawnRadius, spawnRadius));
                
                if ((Physics2D.OverlapCircle(spawnPosition, 0.1f) == null) && NavMesh.SamplePosition(spawnPosition, out hit, 1.0f, NavMesh.AllAreas))
                {
                    invalidSpawnPosition = false;
                    Instantiate(enemies[enemyIndex], spawnPosition, Quaternion.identity);
                }
            }
            
        }
    }
}
