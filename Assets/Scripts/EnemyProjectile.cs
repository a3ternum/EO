using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int terrainLayer = LayerMask.NameToLayer("Terrain");

        // if we hit enemy
        if (hitInfo.gameObject.layer == enemyLayer)
        {
            // get the enemy script
            Bandit enemy = hitInfo.GetComponent<Bandit>();

            // ignore enemy collision
            return;
        }

        // if we hit a wall
        if (hitInfo.gameObject.layer == terrainLayer)
        {
            // object is destroyed at end of method so ignore here.

            // (richochet off wall logic here)

        }

        // if we hit the player
        if (hitInfo.gameObject.layer == playerLayer)
        {
            // get the player script
            Player player = hitInfo.GetComponent<Player>();

            // deal damage to player
            player.takeDamage(10);
        }

        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
