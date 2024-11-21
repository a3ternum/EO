using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyStrike : MeleeAttackSingleTarget
{
    private SkillData heavyStrikeSkillData;

    protected override void Awake() // Initialize heavy strike skill data
    {
        base.Awake();
        InitializeSkill();
    }
   

    protected override void Start()
    {
        base.Start();  
    }

    public override void InitializeSkill()
    {
        skillName = "Heavy Strike";
        heavyStrikeSkillData = ScriptableObject.CreateInstance<SkillData>();

        heavyStrikeSkillData.damagePerLevel = new List<float[]>
        {
            new float[] { 10f, 0f, 0f, 0f, 0f },
            new float[] { 20f, 0f, 0f, 0f, 0f },
            new float[] { 30f, 0f, 0f, 0f, 0f },
            new float[] { 40f, 0f, 0f, 0f, 0f },
            new float[] { 50f, 0f, 0f, 0f, 0f }
        };
        heavyStrikeSkillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        heavyStrikeSkillData.attackSpeedPerLevel = new List<float> { 3f, 3.1f, 3.2f, 3.4f, 3.7f };
        heavyStrikeSkillData.strikeRangePerLevel = new List<float> { 2f, 2f, 2f, 2f, 2f };
        heavyStrikeSkillData.castSpeedPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        heavyStrikeSkillData.durationPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };


        int skillLevel = 0;
        if (heavyStrikeSkillData == null)
        {
            return;
        }
        damage = heavyStrikeSkillData.damagePerLevel[skillLevel];
        strikeRange = heavyStrikeSkillData.strikeRangePerLevel[skillLevel];
        attackSpeed = heavyStrikeSkillData.attackSpeedPerLevel[skillLevel];
        manaCost = heavyStrikeSkillData.manaCostPerLevel[skillLevel];

    }




}
