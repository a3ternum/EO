using UnityEngine;

public class RangedAttack : Attack
{ 
    
    public Projectile projectilePrefab;
    protected int additionalProjectiles = 0;

    protected override void Awake()
    {
        base.Awake();
        skillName = "RangedAttack";

    }

    public void SetProjectilePrefab(Projectile prefab)
    {
        projectilePrefab = prefab;
    }

    public override void ActivateSkill()
    {
        base.ActivateSkill();
        StartCoroutine(AttackCoroutine());
        LaunchProjectiles();
    }

    protected virtual void LaunchProjectiles()
    {
        int totalProjectiles = 1 + additionalProjectiles + user.additionalProjectiles;
        float angleStep = 30f;
        float startAngle = -angleStep * (totalProjectiles - 1) / 2;
        Vector2 directionToTarget = GetDirectionToTarget();

        for (int i = 0; i < totalProjectiles; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * directionToTarget;
            Projectile projectile = Instantiate(projectilePrefab, user.transform.position, Quaternion.identity);

            // Rotate the projectile to face the direction
            float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

            projectile.Initialize(direction, projectileSpeed, duration, tickRate, damage, enemyLayer, terrainLayer, playerLayer, this);
        }
    }
    private Vector2 GetDirectionToTarget()
    {
        if (user is Player)
        {
            return ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)user.transform.position).normalized;
        }
        else if (user is Enemy)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                return ((Vector2)player.transform.position - (Vector2)user.transform.position).normalized;
            }
        }
        return Vector2.right; // Default direction if no target is found
    }
}
