using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttackArea : Attack
{

    protected override void Start()
    {
      base.Start();
    }


    public override void ActivateSkill()
    {
        List<Enemy> targets = FindTargetInRange();
        bool canActivate = CanActivate();
        if (canActivate)
        {
            StartCoroutine(AttackCoroutine());
            OnActivate();
            List<Enemy> targetsList = AoECollider();
            ApplyDamageAndEffects(targetsList);
        }

    }

    // perhaps refactor to allow enemies to also use this method. So that they can find the player within range
    // do this by checking for creature inheritance. if user is a player then use this method.
    // if user is a enemy, use different method.
    protected virtual List<Enemy> FindTargetInRange()
    {
        // raycast should be shot out in direction of mouse 
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // find all enemies within range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(user.transform.position, range);
        Enemy closestEnemy = null;
        List<Enemy> targetsList = new List<Enemy>();
        float closestDistance = float.MaxValue;

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    float distanceToTarget = Vector2.Distance(targetPosition, enemy.transform.position);
                    if (distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget;
                        closestEnemy = enemy;
                    }
                }
            }
        }
        if (closestEnemy != null)
        {
            targetsList.Add(closestEnemy);
        }
        return targetsList;
    }

    protected override void ApplyDamageAndEffects(List<Enemy> targets)
    {
        if (targets != null && targets.Count > 0)
        {
            foreach (var target in targets)
            {
                target.TakeDamage(CalculateDamage());

                //Rigidbody targetRb = target.GetComponent<Rigidbody>();
                //if (targetRb != null)
                //{
                //    Vector3 knockbackDir = (target.transform.position - transform.position).normalized;
                //    targetRb.AddForce(knockbackDir * KnockbackForce, ForceMode.Impulse);
                //}

                TriggerHitEffect(target.transform.position);
            }
        }
    }

    protected virtual List<Enemy> AoECollider()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(user.transform.position, range);
        List<Enemy> targetsList = new List<Enemy>();

        foreach (var collider in hitColliders) {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    targetsList.Add(enemy);
                }
            }
        }
        return targetsList;
    } 

    protected virtual IEnumerator AoEColliderRoutine()
    {
        while (true)
        {
            yield return null;
        }
    }
    
}
