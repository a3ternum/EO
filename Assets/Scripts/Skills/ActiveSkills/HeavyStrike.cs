using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyStrike : MeleeAttackSingleTarget
{
    private SkillData heavyStrikeSkillData;

    protected override void Awake() // Initialize heavy strike skill data
    {
        base.Awake();
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






}
