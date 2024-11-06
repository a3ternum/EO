using UnityEngine;

public class WindSlash : PlayerProjectile
{
    void Start()
    {
        speed = 20f;
        // find a way to get the player's damage instead of hardcoding it
        projectileDamage = 10f;
        destroyEnemyProjectiles = false;
        destructable = false;
        rb.linearVelocity = transform.right * speed;
    }


}
