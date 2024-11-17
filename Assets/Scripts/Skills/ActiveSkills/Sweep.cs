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

        sweepSkillData.damagePerLevel = new List<float[]>
        {
            new float[] { 10f, 0f, 0f, 0f, 0f },
            new float[] { 20f, 0f, 0f, 0f, 0f },
            new float[] { 30f, 0f, 0f, 0f, 0f },
            new float[] { 40f, 0f, 0f, 0f, 0f },
            new float[] { 50f, 0f, 0f, 0f, 0f }
        };
        sweepSkillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        sweepSkillData.attackSpeedPerLevel = new List<float> { 3f, 1.1f, 1.2f, 1.3f, 1.4f };
        sweepSkillData.radiusPerLevel = new List<float> { 3f, 3.4f, 3.6f, 3.8f, 4f };
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
        radius = sweepSkillData.radiusPerLevel[skillLevel];
        attackSpeed = sweepSkillData.attackSpeedPerLevel[skillLevel];
        manaCost = sweepSkillData.manaCostPerLevel[skillLevel];
    }


}