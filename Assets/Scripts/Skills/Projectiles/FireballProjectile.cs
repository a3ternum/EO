using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireballProjectile : Projectile
{
    private Animator animator;
    protected override void Awake()
    {
        base.Awake();
        destroyOnHit = true;
        animator = GetComponent<Animator>();

    }

    protected void Start()
    {
        // trigger the spawn animator on the fireball object
        animator.SetTrigger("Spawn");
        Debug.Log("Initial Animator State: " + animator.GetCurrentAnimatorStateInfo(0).IsName("Spawned"));

    }

    protected override IEnumerator MoveAndHandleCollisions()
    {
        int currentPierceCount = pierceCount;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position += (Vector3)direction.normalized * projectileSpeed * Time.deltaTime;

            Collider2D[] hitColliders = GetHitColliders();
            List<Creature> targetsList = new List<Creature>();

            foreach (var hitCollider in hitColliders)
            {
                Creature creature = hitCollider.GetComponent<Creature>();
                if (creature == null && hitCollider.gameObject.layer == terrainLayer)
                {
                    // Destroy the projectile if it hits terrain
                    Destroy(gameObject);
                    yield break; // Exit the coroutine as the projectile is destroyed
                }

                bool isEnemyTarget = (hitCollider.gameObject.layer == enemyLayer && skill.user is Player);
                bool isPlayerTarget = (hitCollider.gameObject.layer == playerLayer && skill.user is Enemy);
                //Debug.Log(isEnemyTarget || isPlayerTarget);
                if (!isEnemyTarget && !isPlayerTarget)
                {
                    elapsedTime += Time.deltaTime;
                    continue;
                }

                if (!(!lastHitTime.ContainsKey(creature) || Time.time - lastHitTime[creature] >= tickRate)) // if creature is not allowed to be hit yet
                {
                    elapsedTime += Time.deltaTime;
                    continue;
                }
                // make sure target is on screen before adding it to the list
                if (IsTargetOnScreen(creature))
                {
                    targetsList.Add(creature);
                    lastHitTime[creature] = Time.time;

                    if (destroyOnHit && currentPierceCount <= 0)
                    {
                        skill.ApplyDamageAndEffects(targetsList);
                        FireballExplosion();
                        // trigger the explosion animator on the fireball object
                        Debug.Log("Fireball explosion triggered");
                        animator.SetTrigger("Explode");
                       
                        // destroy the projectile after the explosion animation is done
                        yield return new WaitForSeconds(0.5f);
                        Destroy(gameObject);
                        yield break; // Exit the coroutine as the projectile is destroyed
                    }
                    else if (destroyOnHit && currentPierceCount > 0)
                    {
                        currentPierceCount--;
                    }
                } 
            }
            skill.ApplyDamageAndEffects(targetsList);   
            yield return null;
        }
        Destroy(gameObject);
    }


    protected void FireballExplosion() // fireball explodes with radius that is 1.5x the radius of the fireball. damaging every enemy in the radius
    {
        // get all enemies in the radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius * 2f);
        List<Creature> targetsList = new List<Creature>();
        foreach (var collider in hitColliders)
        {
            bool isEnemyTarget = (collider.gameObject.layer == enemyLayer && skill.user is Player);
            bool isPlayerTarget = (collider.gameObject.layer == playerLayer && skill.user is Enemy);
            if (!isEnemyTarget && !isPlayerTarget)
                continue;

            Creature creature = collider.GetComponent<Creature>();
            if (creature == null)
                continue;
            targetsList.Add(creature);
        }

        ApplyFireballDamage(targetsList);
    }

    protected void ApplyFireballDamage(List<Creature> targets) // for now fireball explosion just does the same damage as the fireball itself
    {   
        if (targets != null && targets.Count > 0)
        {
            foreach (var target in targets)
            {
                //supply our damage and accuracy. Later we will add more parameters
                // such as elemental penetration, phys overwhelm, enemy block mitigation etc
                // this way we can make sure that certain abilities cannot be blocked or evaded/missed
                // in particular we want spells to be unevasible so we will override this method in the spell class
                target.TakeDamage(skill.CalculateDamage(), skill.user);
                TriggerHitEffect(target.transform.position);
            }
        }
    }

    protected void TriggerHitEffect(Vector3 position)
    {
        // spawn a fireball explosion effect
        //GameObject fireballExplosion = Instantiate(skill.hitEffect, position, Quaternion.identity);
        //Destroy(fireballExplosion, 1f);
    }

}
