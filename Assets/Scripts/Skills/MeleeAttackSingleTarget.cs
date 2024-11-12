using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttackSingleTarget : MeleeAttack
{


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
  
    

    


}
