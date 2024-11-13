using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttackArea : MeleeAttack
{

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



    // optional method for AoE skills that require a target to be selected
   


}
