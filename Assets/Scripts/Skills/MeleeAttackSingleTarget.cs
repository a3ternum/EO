using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class MeleeAttackSingleTarget : MeleeAttack
{
    private float singleTargetRangeParameter = 0.2f; // tweak this value!!!!
    public override void ActivateSkill()
    {
        List<Creature> targets = FindTargetInRange();
        bool canActivate = CanActivate();
        StartCoroutine(AttackCoroutine());
        if (canActivate && targets != null && targets.Count > 0)
        {
            originalHitLocation = targets[0].transform.position; // Store the original hit location
            // check if our strike location still overlaps with the collider of the target
            Collider2D targetCollider = targets[0].GetComponent<Collider2D>();
            if (targetCollider != null && targetCollider.OverlapPoint(originalHitLocation))
            {
                ApplyDamageAndEffects(targets);
            }
            else // check for new targets in original hit location and hit the first one
            {
                
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(originalHitLocation, singleTargetRangeParameter); 
                foreach (var collider in hitColliders)
                {
                    Creature creature = collider.GetComponent<Creature>();
                    if (creature != null)
                    {
                        ApplyDamageAndEffects(new List<Creature> { creature });
                        break;
                    }
                }
            }
        

            OnActivate();
        }
    }

}
