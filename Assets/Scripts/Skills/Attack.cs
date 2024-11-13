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

    protected GameObject creatureWeapon;
    protected GameObject targets { get; set; }
    protected Vector2 originalHitLocation;

    protected override void Start()
    {
        base.Start();
        creatureWeapon = transform.Find("firePoint/Weapon")?.gameObject;
        if (creatureWeapon != null)
        {
            animator = creatureWeapon.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator component not found on " + gameObject.name);
            }
        }
        else
        {
            Debug.LogWarning("Weapon object not found on " + gameObject.name);
        }

    }
  
    protected IEnumerator AttackCoroutine()
    {
        OnActivate();
        float playerAttackSpeed = user.currentAttackSpeed;
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
}
