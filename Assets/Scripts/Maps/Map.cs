using UnityEngine;
/// <summary>
/// Map class is a container for all data related to a map instance that can be spawned by the player.
/// It contains information such as:
/// - map size and shape (e.g. which map generator to use)
/// - pack density
/// - pack size
/// - spawn probability
/// - enemy spawn table
/// 
/// in the future it will also contain unique map modifiers such as:
/// - map quality
/// - increased monster damage
/// - increased monster attack/cast speed
/// - increased monster health
/// - increased monster movement speed
/// - increased monster pack size
/// - extra monster projectiles
/// - increases monster resistances
/// - increased monster critical strike chance
/// - etc...
/// 
/// On top, Map will inherit from a base class called Item, which will allow it to be stored in the player's inventory.
/// </summary>

public class Map : MonoBehaviour
{

    private string mapType;
    private float packDensity;
    private float packSize;
    private float spawnProbability;

    private EnemySpawnTable enemySpawnTable;

    // add more map modifiers here

    private void Start()
    {
        
    }

    private void OnUse()
    {
        // generate the map


    }



}
