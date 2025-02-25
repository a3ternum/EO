using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
public class PackSpawn : MonoBehaviour
{
    // ideally what we want to do is have a list of enemies that we can spawn, and then we can randomly select one of them to spawn

    public EnemySpawnTable enemySpawnTable;


    public float spawnRadius = 1.5f;
    public float packSize;
    private int totalWeight;


    private void Awake()
    {
        // calculate the total weight of all enemies
        foreach (var enemyWeight in enemySpawnTable.enemyWeights)
        {
            totalWeight += enemyWeight.weight;
        }
    }

    private void Start()
    {
        spawnPack();
    }

    public void spawnPack()
    {
        if (enemySpawnTable.enemyWeights.Count == 0)
        {
            Debug.LogWarning("No enemies to spawn.");
            return;
        }

        packSize = Random.Range(1, packSize);


        NavMeshHit hit;
        int attempts = 0;

        // choose an enemy type from the array of enemies
        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;
        Enemy enemyToSpawn = null;

        foreach (var enemyWeight in enemySpawnTable.enemyWeights)
        {
            currentWeight += enemyWeight.weight;
            if (randomValue < currentWeight)
            {
                enemyToSpawn = enemyWeight.enemyPrefab;
                break;
            }
        }

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
                    Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                }
            }
            
        }
        Destroy(gameObject);
    }
}
