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

    protected GameObject targets { get; set; }
    protected Vector2 originalHitLocation;

    protected override void Start()
    {
        base.Start();

    }

    public override void OnActivate()
    // This method will handle common tasks performed upon activation, like setting the cooldown timer,
    // updating the mana pool, and any general pre-activation setup.
    {
        if (user is Player)
        {
            Player player = user.GetComponent<Player>();
            player.currentMana = Mathf.Max(player.currentMana - manaCost, 0);
        }
        if (user.currentAttackSpeed == 0 || attackSpeed == 0)
        {
            Debug.LogError("Attack speed is 0");
        }
        cooldownTimer = (1 / attackSpeed) / user.currentAttackSpeed;
    }


    protected override IEnumerator SkillCoroutine()
    {
        float playerAttackSpeed = user.currentAttackSpeed;
        animationDuration = CalculateAnimationDuration(playerAttackSpeed, attackSpeed);
        
        if (animator!= null) // play animation only if attack has an animation
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

    protected override float CalculateAnimationDuration(float playerAttackSpeed, float attackSpeed)
    {
        // This method will calculate the duration of the attack animation based on the player's attack speed and the attack's attack speed.
        // to be implemented in child classes
        return 1/ (playerAttackSpeed * attackSpeed);
    }
}
