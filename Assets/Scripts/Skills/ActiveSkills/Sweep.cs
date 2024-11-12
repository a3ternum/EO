using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sweep : MeleeAttackArea
{
    private SkillData sweepSkillData;

    protected override void Awake() // Initialize heavy strike skill data
    {
        base.Awake();
        skillName = "Sweep";

        sweepSkillData = ScriptableObject.CreateInstance<SkillData>();

        sweepSkillData.damagePerLevel = new List<float> { 10f, 20f, 30f, 40f, 50f };
        sweepSkillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        sweepSkillData.attackSpeedPerLevel = new List<float> { 3f, 1.1f, 1.2f, 1.3f, 1.4f };
        sweepSkillData.rangePerLevel = new List<float> { 3f, 3.4f, 3.6f, 3.8f, 4f };
        sweepSkillData.castTimePerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        sweepSkillData.durationPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
    }


    protected override void Start()
    {
        base.Start();


        int skillLevel = 0;
        if (sweepSkillData == null)
        {
            return;
        }
        damage = sweepSkillData.damagePerLevel[skillLevel];
        range = sweepSkillData.rangePerLevel[skillLevel];
        attackSpeed = sweepSkillData.attackSpeedPerLevel[skillLevel];
        manaCost = sweepSkillData.manaCostPerLevel[skillLevel];
    }

    public override float CalculateDamage()
    {
        return base.CalculateDamage();
    }

    public override void ActivateSkill()
    {
        base.ActivateSkill();
    }

    protected override void ApplyDamageAndEffects(List<Creature> targets)
    {
        base.ApplyDamageAndEffects(targets);
    }

    protected override List<Creature> AoECollider()
    {
        return base.AoECollider();
    }
}