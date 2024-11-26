using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spell : Skill
{

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
        Debug.Log("setting cooldown timer to " + cooldownTimer);
    }

    protected override IEnumerator SkillCoroutine()
    {
        float playerCastSpeed = user.currentCastSpeed;
        animationDuration = CalculateAnimationDuration(playerCastSpeed, castSpeed);

        if (animator != null) // play animation only if spell has an animation
        {
            animator.speed = playerCastSpeed; // Set the animation speed based on the player's cast speed and the cast time of the skill
            animator.SetTrigger("Spell"); // Play the spell animation
        }
        user.canMove = false;
        yield return new WaitForSeconds(animationDuration);
        user.canMove = true;
        if (animator != null)
        {
            animator.SetTrigger("SpellFinished"); // Finish the spell animation
        }
    }
   
}
