using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveSpell : Spell
{
    protected int additionalProjectiles = 0;

    protected Projectile projectilePrefab;

    protected virtual void LaunchProjectiles(Vector2 targetPosition)
    {
        int totalProjectiles = 1 + additionalProjectiles + user.currentAdditionalProjectiles;
        float angleStep = 30f;
        float startAngle = -angleStep * (totalProjectiles - 1) / 2;
        Vector2 directionToTarget = GetDirectionToTarget(targetPosition);

        for (int i = 0; i < totalProjectiles; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * directionToTarget;
            Projectile projectile = Instantiate(projectilePrefab, user.transform.position, Quaternion.identity);

            // Rotate the projectile to face the direction
            float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

            projectile.Initialize(direction, this);
        }
    }

    protected Vector2 GetDirectionToTarget(Vector2 targetPosition)
    {
        if (user is Player)
        {
            return (targetPosition - (Vector2)user.transform.position).normalized;
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

    protected virtual List<Creature> AoECollider(Vector2 position)
    {
        float increasedRadius = radius * (1 + areaOfAttackIncrease);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, increasedRadius);
        List<Creature> targetsList = new List<Creature>();


        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.layer == enemyLayer && user is Player)
            {
                if (collider.TryGetComponent(out Creature enemy))
                {
                    targetsList.Add(enemy);
                }
            }
            else if (collider.gameObject.layer == playerLayer && user is Enemy)
            {
                if (collider.TryGetComponent(out Creature player))
                {
                    targetsList.Add(player);
                }
            }
        }

        return targetsList;
    }


}
 
