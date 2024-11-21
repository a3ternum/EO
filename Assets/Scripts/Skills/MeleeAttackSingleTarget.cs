using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class MeleeAttackSingleTarget : MeleeAttack
{
    private float singleTargetRangeParameter = 0.2f; // tweak this value!!!!
    public override void ActivateSkill()
    {
        StartCoroutine(ActivateSkillCoroutine());
    }

    protected IEnumerator ActivateSkillCoroutine()
    {
        List<Creature> targets = FindTargetInRange();
        bool canActivate = CanActivate();
        if (canActivate && targets != null && targets.Count > 0)
        {
            originalHitLocation = targets[0].transform.position;
            Debug.Log("original hit location is " + originalHitLocation);
        }

        yield return StartCoroutine(AttackCoroutine());
        if (canActivate && targets != null && targets.Count > 0)
        {
            // check if our strike location still overlaps with the collider of the target
            Collider2D targetCollider = targets[0].GetComponent<Collider2D>();
            Debug.Log("creature location is " + targets[0].transform.position);
            if (targetCollider != null && targetCollider.OverlapPoint(originalHitLocation))
            {
                Debug.Log("Target is still in range");
                ApplyDamageAndEffects(new List<Creature> { targets[0] });
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
    protected virtual List<Creature> FindTargetInRange()
    {
        // raycast should be shot out in direction of mouse 
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // find all enemies within range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(user.transform.position, strikeRange);
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
