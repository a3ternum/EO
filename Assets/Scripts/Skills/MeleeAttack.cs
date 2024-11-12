using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class MeleeAttack : Attack
{
    // technically only setting isMelee is relevant here.
    private bool isMelee = true;
    public float KnockbackForce { get; protected set; }
    public float Range { get; protected set; }



    protected override void Start()
    {
      base.Start();
    }

    protected virtual List<Creature> FindTargetInRange()
    {
        // raycast should be shot out in direction of mouse 
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // find all enemies within range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(user.transform.position, range);
        Creature closestTarget = null;
        List<Creature> targetsList = new List<Creature>();
        float closestDistance = float.MaxValue;

        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.layer == enemyLayer && user is Player || collider.gameObject.layer == playerLayer && user is Enemy)
            {
                Creature creature = collider.GetComponent<Creature>();
                if (creature != null)
                {
                    float distanceToTarget = Vector2.Distance(targetPosition, creature.transform.position);
                    if (distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget;
                        closestTarget = creature;
                    }
                }
            }
            
        }
        if (closestTarget != null)
        {
            targetsList.Add(closestTarget);
        }
        return targetsList;
    }

}
