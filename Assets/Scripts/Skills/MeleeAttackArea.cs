using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttackArea : MeleeAttack
{

    protected override void Start()
    {
      base.Start();
    }


    public override void ActivateSkill()
    {
        bool canActivate = CanActivate();
        if (canActivate)
        {
            StartCoroutine(AttackCoroutine());
            OnActivate();
            List<Creature> targetsList = AoECollider();
            ApplyDamageAndEffects(targetsList);
        }

    }

    // perhaps refactor to allow enemies to also use this method. So that they can find the player within range
    // do this by checking for creature inheritance. if user is a player then use this method.
    // if user is a enemy, use different method.
 

    protected override void ApplyDamageAndEffects(List<Creature> targets)
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

    protected virtual List<Creature> AoECollider()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(user.transform.position, range);
        List<Creature> targetsList = new List<Creature>();


        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.layer == enemyLayer && user is Player)
            {
                Creature enemy = collider.GetComponent<Creature>();
                if (enemy != null)
                {
                    targetsList.Add(enemy);
                }
            }
            else if (collider.gameObject.layer == playerLayer && user is Enemy)
            {
                Creature player = collider.GetComponent<Creature>();
                if (player != null)
                {
                    targetsList.Add(player);
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

    // optional method for AoE skills that require a target to be selected
   


}
