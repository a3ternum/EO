using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyStrike : MeleeAttackSingleTarget
{
    private SkillData heavyStrikeSkillData;

    private void Awake() // Initialize heavy strike skill data
    {
        skillName = "Heavy Strike";
        heavyStrikeSkillData = ScriptableObject.CreateInstance<SkillData>();

        heavyStrikeSkillData.damagePerLevel = new List<float> { 10f, 20f, 30f, 40f, 50f };
        heavyStrikeSkillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        heavyStrikeSkillData.attackSpeedPerLevel = new List<float> { 3f, 1.1f, 1.2f, 1.3f, 1.4f };
        heavyStrikeSkillData.rangePerLevel = new List<float>{4f, 4.4f, 4.6f, 4.8f, 5f};
        heavyStrikeSkillData.castTimePerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        heavyStrikeSkillData.durationPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
    }
   

    protected override void Start()
    {
        base.Start();


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

    public override void ActivateSkill()
    {
        base.ActivateSkill();
    }

    protected override List<Enemy> FindTargetInRange()
    {
        return base.FindTargetInRange();
    }

    protected override void ApplyDamageAndEffects(List<Enemy> targets)
    {
        base.ApplyDamageAndEffects(targets);
    }


}
