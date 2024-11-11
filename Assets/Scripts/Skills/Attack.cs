using UnityEngine;

public class Attack : Skill
{
    // this class will be used for melee attacks, which will be used by the player and enemies.
    // melee attacks will contain the following properties:
    // - damage: the amount of base damage the attack will deal
    // - range: the maximum distance the attack can reach

    public override void ActivateSkill(float currentMana)
    {
        throw new System.NotImplementedException();
    }

    public override bool CanActivate(float currentMana)
    {
        return base.CanActivate(currentMana);
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }

    public override void UpgradeSkill()
    {
        base.UpgradeSkill();
    }

    public override void OnActivate(float currentMana)
    {
       base.OnActivate(currentMana);
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

    public virtual void PlayAttackAnimation()
    {
        // This method will be used to play the attack animation.
        // to be implemented in child classes
    }
}
