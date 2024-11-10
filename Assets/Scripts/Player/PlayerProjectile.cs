using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed;
    public float projectileDamage;
    public Rigidbody2D rb;
    public int pierce; // value to track how many enemies our object is allowed to pass through before being destroyed

    public bool destroyEnemyProjectiles = false;
    public bool destructable = false;

    private bool destroyProjectile = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
       
    }

    public void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        // float damage should be turned into a list later, with the damage values being one for each element.

        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int terrainLayer = LayerMask.NameToLayer("Terrain");
        int enemyProjectileLayer = LayerMask.NameToLayer("EnemyProjectile");
        int playerProjectileLayer = LayerMask.NameToLayer("PlayerProjectile");

        // if we hit enemy
        if (hitInfo.gameObject.layer == enemyLayer)
        {
            hitEnemy(hitInfo);
        }

        // if we hit a wall
        if (hitInfo.gameObject.layer == terrainLayer)
        {
            hitTerrain(hitInfo);
        }

        // if we hit an enemy projectile
        if (hitInfo.gameObject.layer == enemyProjectileLayer)
        {
            hitEnemyProjectile(hitInfo);
        }

        // if we hit a player projectile
        if (hitInfo.gameObject.layer == playerProjectileLayer)
        {
            hitPlayerProjectile(hitInfo);
        }

        // if we hit the player
        if (hitInfo.gameObject.layer == playerLayer)
        {
            hitPlayer(hitInfo);
        }

        if (destroyProjectile)
        {
            Destroy(gameObject);
        }
        

    }

    protected void hitEnemy(Collider2D hitInfo)
    {
        Bandit enemy = hitInfo.GetComponent<Bandit>();
        // need to find out how much damage the player does
        enemy.takeDamage(projectileDamage);

        destroyProjectile = true;
        
    }

    protected void hitPlayer(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();

        // ignore player collision for now.
        destroyProjectile = false;
    }

    protected void hitTerrain(Collider2D hitInfo)
    {
        // richochet off wall logic here
        destroyProjectile = true;
    }

    protected void hitEnemyProjectile(Collider2D hitInfo) 
    {
        EnemyProjectile enemyProjectile = hitInfo.GetComponent<EnemyProjectile>();

        // check if enemy projectile is destructable.
        if (destructable)
        {
            destroyProjectile = true;
            return;
        }
        destroyProjectile = false;
    }

    protected void hitPlayerProjectile(Collider2D hitInfo)
    {
        // nothing happens
        destroyProjectile = false;
    }

}
