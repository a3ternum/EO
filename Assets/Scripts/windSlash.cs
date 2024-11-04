using UnityEngine;

public class windSlash : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 20f;
    public Rigidbody2D rb;


    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {

        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int terrainLayer = LayerMask.NameToLayer("Terrain");

        // if we hit main character
        if (hitInfo.gameObject.layer == playerLayer)
        {
            // get the player script
            Player player = hitInfo.GetComponent<Player>();

            // ignore player collision
            return;
        }

        // if we hit a wall

        if (hitInfo.gameObject.layer == terrainLayer)
        {
            // object is destroyed at end of method so ignore here.

            // (richochet off wall logic here)

        }
        // if we hit an enemy
        if (hitInfo.gameObject.layer == enemyLayer)
        {
            // get the enemy script
            Bandit enemy = hitInfo.GetComponent<Bandit>();

            // deal damage to enemy
            enemy.takeDamage(10);
        }

        // destroy wind slash
        Destroy(gameObject);
    }
  
}
