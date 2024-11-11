using UnityEngine;

public abstract class MeleeAttack : Attack
{
    public float KnockbackForce { get; protected set; }
    public float Range { get; protected set; }

    // abstract activate method for melee attacks
    public override void ActivateSkill(float currentMana)
    {
        base.ActivateSkill(currentMana);
    }

    public override float CalculateDamage()
    {
        return base.CalculateDamage();
    }

}
