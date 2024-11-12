using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class MeleeAttack : Attack
{
    // technically only setting isMelee is relevant here.
    private bool isMelee = true;
    public float KnockbackForce { get; protected set; }
    public float Range { get; protected set; }

    protected GameObject ourWeapon;
    protected GameObject targets { get; set; }
    protected Vector2 originalHitLocation;

    protected override void Start()
    {
        base.Start();
        ourWeapon = transform.Find("firePoint/Weapon")?.gameObject;
        if (ourWeapon != null)
        {
            animator = ourWeapon.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component not found on " + gameObject.name);
            }
        }
        else
        {
            Debug.LogError("Weapon object not found on " + gameObject.name);
        }

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
            if (collider.gameObject.layer == enemyLayer && user is Player)
            {
                Creature enemy = collider.GetComponent<Creature>();
                if (enemy != null)
                {
                    float distanceToTarget = Vector2.Distance(targetPosition, enemy.transform.position);
                    if (distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget;
                        closestTarget = enemy;
                    }
                }
            }
            else if (collider.gameObject.layer == playerLayer && user is Enemy)
            {
                Creature player = collider.GetComponent<Creature>();
                if (player != null)
                {
                    float distanceToTarget = Vector2.Distance(targetPosition, player.transform.position);
                    if (distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget;
                        closestTarget = player;
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
