using UnityEngine;

public class MeleeAttackSingleTarget : MeleeAttack
{
    protected GameObject target { get; set; }
    

    public override void ActivateSkill(float currentMana)
    {
        Debug.Log("Activating Skill");
        Enemy target = FindTargetInRange();
        if (CanActivate(currentMana) && target != null)
        {
            PlayAttackAnimation();
            Debug.Log("damage is " + CalculateDamage() + " and target is " + target);
            ApplyDamageAndEffects(target);

            OnActivate(currentMana);
        }
    }

    // rewrite later to also allow enemy mobs to use this method

    protected virtual Enemy FindTargetInRange() 
    {
        // raycast should be shot out in direction of mouse 
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // find all enemies within range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(user.transform.position, range);
        Enemy closestEnemy = null;
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
        return closestEnemy;
    }




    protected virtual void ApplyDamageAndEffects(Enemy target) 
    {
        if (target != null)
        {
            Debug.Log("Applying damage to target" + CalculateDamage());
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
