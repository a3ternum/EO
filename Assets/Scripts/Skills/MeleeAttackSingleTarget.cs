using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MeleeAttackSingleTarget : MeleeAttack
{
    private float singleTargetRangeParameter = 0.2f; // tweak this value!!!!
    private SingleTargetIndicatorCircle SingleTargetIndicatorCircle;

    

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
            yield return StartCoroutine(AttackCoroutine());
        
            // check if our strike location still overlaps with the collider of the target
            Collider2D targetCollider = targets[0].GetComponent<Collider2D>();
            if (targetCollider != null && targetCollider.OverlapPoint(originalHitLocation))
            {
                ApplyDamageAndEffects(new List<Creature> { targets[0] });
            }
            else // check for new targets in original hit location and hit the first one
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(originalHitLocation, singleTargetRangeParameter);
                foreach (var collider in hitColliders)
                {
                    if (collider.gameObject.layer == enemyLayer && user is Player || collider.gameObject.layer == playerLayer && user is Enemy)
                    {
                        Creature creature = collider.GetComponent<Creature>();
                        if (creature != null)
                        {
                            ApplyDamageAndEffects(new List<Creature> { creature });
                            break;
                        }
                    }
                }
            }

            OnActivate(); // maybe move this to the start of the coroutine
        }
    }

    protected override IEnumerator AttackCoroutine()
    {
        OnActivate();
        float playerAttackSpeed = user.currentAttackSpeed;
        animationDuration = CalculateAnimationDuration(playerAttackSpeed, attackSpeed);

        // spawn SingleTargetIndicatorCircle at the location of the target
        SingleTargetIndicatorCircle = Instantiate(Resources.Load<SingleTargetIndicatorCircle>("SingleTargetIndicatorCircle"), originalHitLocation, Quaternion.identity);
        SingleTargetIndicatorCircle.animationDuration = animationDuration;

        if (animator != null) // play animation only if attack has an animation
        {
            animator.speed = attackSpeed * playerAttackSpeed; // Set the animation speed based on the player's attack speed and the attack's attack speed
            animator.SetTrigger("Attack"); // Play the attack animation
        }
        user.canMove = false;
        yield return new WaitForSeconds(animationDuration);
        user.canMove = true;
        if (animator != null)
        {
            animator.SetTrigger("AttackFinished"); // Finish the attack animation

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
            if ((collider.gameObject.layer == enemyLayer && user is Player) || (collider.gameObject.layer == playerLayer && user is Enemy))
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
