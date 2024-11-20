using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spell : Skill
{
    public Animator animator;
    public float animationDuration;



    public override void OnActivate()
    // This method will handle common tasks performed upon activation, like setting the cooldown timer,
    // updating the mana pool, and any general pre-activation setup.
    {
        if (user is Player)
        {
            Player player = user.GetComponent<Player>();
            player.currentMana = Mathf.Max(player.currentMana - manaCost, 0);
        }
        if (user.currentCastSpeed == 0)
        {
            Debug.LogError("Cast speed is 0");
        }
        cooldownTimer = (1 / castSpeed) / user.currentCastSpeed;
    }

    protected IEnumerator SpellCoroutine()
    {
        OnActivate();
        float playerCastSpeed = user.currentCastSpeed;
        animationDuration = CalculateAnimationDuration(playerCastSpeed, castSpeed);

        if (animator != null) // play animation only if spell has an animation
        {
            animator.speed = playerCastSpeed; // Set the animation speed based on the player's cast speed and the cast time of the skill
            animator.SetTrigger("Attack"); // Play the spell animation
        }
        yield return new WaitForSeconds(animationDuration);
        if (animator != null)
        {
            animator.SetTrigger("AttackFinished"); // Finish the spell animation
        }
    }

    private float CalculateAnimationDuration(float playerCastSpeed, float castSpeed)
    {
        // This method will calculate the duration of the spell animation based on the player's cast speed and the spell's cast speed.
        // to be implemented in child classes
        return (1 / castSpeed) / playerCastSpeed;
    }
   
}
