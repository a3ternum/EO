using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    public bool destructable = true;

    private bool destroyProjectile = true;
    private Enemy enemy;
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

    public void setParentEnemy(Enemy parent)
    {
        enemy = parent;
    }

    protected void hitEnemy(Collider2D hitInfo)
    {
        destroyProjectile = false; // ignore enemy collisions
    }

    protected void hitPlayer(Collider2D hitInfo)
    {
        Player player = hitInfo.GetComponent<Player>();
        player.takeDamage(enemy.damage);
        destroyProjectile = true;
    }

    protected void hitTerrain(Collider2D hitInfo)
    {
        destroyProjectile = true;
        // richochet off wall logic here
    }

    protected void hitEnemyProjectile(Collider2D hitInfo)
    {

        destroyProjectile = false; // ignore enemy projectile collisions
    }

    protected void hitPlayerProjectile(Collider2D hitInfo)
    {
        PlayerProjectile playerProjectile = hitInfo.GetComponent<PlayerProjectile>();

        if (destructable && playerProjectile.destroyEnemyProjectiles)
        {
            destroyProjectile = true;
            return;
        }
        destroyProjectile = false;
    }

}
