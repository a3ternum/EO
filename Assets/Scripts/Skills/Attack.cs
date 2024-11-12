using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Attack : Skill
{
    // this class will be used for melee attacks, which will be used by the player and enemies.
    // melee attacks will contain the following properties:
    // - damage: the amount of base damage the attack will deal
    // - range: the maximum distance the attack can reach
    public Animator animator;
    public float animationDuration;
    protected override void Start()
    {
        base.Start();
        
    }
    public override void ActivateSkill()
    {
        base.ActivateSkill();
    }

    public override bool CanActivate()
    {
        return base.CanActivate();
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }

    public override void UpgradeSkill()
    {
        base.UpgradeSkill();
    }

    public override void OnActivate()
    {
       base.OnActivate();
    }

    public override float CalculateDamage()
    {
        return damage;
    }
    public override void UpdateCooldown(float deltaTime)
    {
        base.UpdateCooldown(deltaTime);
    }

    public virtual void TriggerHitEffect(Vector3 position)
    {
        // This method will be used to trigger a visual effect when the attack hits a target.
        // to be implemented in child classes
    }
    protected IEnumerator AttackCoroutine()
    {
        OnActivate();
        float playerAttackSpeed = user.attackSpeed;
        animationDuration = CalculateAnimationDuration(playerAttackSpeed, attackSpeed);
        
        if (animator!= null) // play animation only if attack has an animation
        {
            animator.speed = attackSpeed * playerAttackSpeed; // Set the animation speed based on the player's attack speed and the attack's attack speed
            animator.SetTrigger("Attack"); // Play the attack animation
        }
        yield return new WaitForSeconds(animationDuration);
        if (animator != null)
        {
            animator.SetTrigger("AttackFinished"); // Finish the attack animation
            
        }



    }

    private float CalculateAnimationDuration(float playerAttackSpeed, float attackSpeed)
    {
        // This method will calculate the duration of the attack animation based on the player's attack speed and the attack's attack speed.
        // to be implemented in child classes
        return playerAttackSpeed * attackSpeed;
    }

    protected virtual void ApplyDamageAndEffects(List<Creature> targets)
    {

        if (targets != null)
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
}
