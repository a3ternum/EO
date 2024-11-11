using UnityEngine;

public abstract class MeleeAttack : Attack
{
    private bool isMelee = true;
    public float KnockbackForce { get; protected set; }
    public float Range { get; protected set; }

    protected override void Start()
    {
        base.Start();
    }

    // abstract activate method for melee attacks
    public override void ActivateSkill()
    {
        base.ActivateSkill();
    }

    public override float CalculateDamage()
    {
        return base.CalculateDamage();
    }

}
