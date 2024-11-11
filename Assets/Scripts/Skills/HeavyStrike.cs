using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyStrike : MeleeAttackSingleTarget
{
    private SkillData heavyStrikeSkillData;

    private void Awake() // Initialize heavy strike skill data
    {
        heavyStrikeSkillData = ScriptableObject.CreateInstance<SkillData>();

        heavyStrikeSkillData.damagePerLevel = new List<float> { 10f, 20f, 30f, 40f, 50f };
        heavyStrikeSkillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        heavyStrikeSkillData.attackSpeedPerLevel = new List<float> { 1f, 1.1f, 1.2f, 1.3f, 1.4f };
        heavyStrikeSkillData.rangePerLevel = new List<float>{2f, 2.4f, 2.6f, 2.8f, 3f};
        heavyStrikeSkillData.castTimePerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        heavyStrikeSkillData.durationPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
    }
   

    private void Start()
    {
        int skillLevel = 0;
        if (heavyStrikeSkillData == null)
        {
            return;
        }
        damage = heavyStrikeSkillData.damagePerLevel[skillLevel];
        range = heavyStrikeSkillData.rangePerLevel[skillLevel];
        attackSpeed = heavyStrikeSkillData.attackSpeedPerLevel[skillLevel];
        manaCost = heavyStrikeSkillData.manaCostPerLevel[skillLevel];
    }

    public override float CalculateDamage()
    {
        return base.CalculateDamage();
    }

    public override void ActivateSkill(float currentMana)
    {
        Debug.Log("skill damage is:" + CalculateDamage());
        base.ActivateSkill(currentMana);
    }

    protected override Enemy FindTargetInRange()
    {
        return base.FindTargetInRange();
    }

    protected override void ApplyDamageAndEffects(Enemy target)
    {
        base.ApplyDamageAndEffects(target);
    }


}
