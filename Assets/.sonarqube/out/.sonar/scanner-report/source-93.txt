using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemySpawnTable", menuName = "ScriptableObjects/EnemySpawnTable", order = 1)]
public class EnemySpawnTable : ScriptableObject
{
    [System.Serializable]
    public class EnemyWeight
    {
        public Enemy enemyPrefab;
        public int weight;

    }

    public List<EnemyWeight> enemyWeights = new List<EnemyWeight>();
}