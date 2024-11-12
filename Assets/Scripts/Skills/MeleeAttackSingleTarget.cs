using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttackSingleTarget : MeleeAttack
{

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
        

    public override void ActivateSkill()
    {
        List<Creature> targets = FindTargetInRange();
        bool canActivate = CanActivate();
        StartCoroutine(AttackCoroutine());
        if (canActivate && targets != null && targets.Count > 0)
        {
            originalHitLocation = targets[0].transform.position; // Store the original hit location
            // check if target is still within range of the original hit location
            if (Vector2.Distance(targets[0].transform.position, originalHitLocation) <= range)
            {
                ApplyDamageAndEffects(targets);
            }
            else
            {
                if (targets.Count > 1)
                {
                    ApplyDamageAndEffects(targets.GetRange(1, targets.Count - 1));
                }  
            }

            OnActivate();
        }
    }

    // perhaps refactor to allow enemies to also use this method. So that they can find the player within range
    // do this by checking for creature inheritance. if user is a player then use this method.
    // if user is a enemy, use different method.
  
    protected override void ApplyDamageAndEffects(List<Creature> targets) 
    {
        if (targets != null && targets.Count > 0)
        {
            foreach(var target in targets)
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

    


}
