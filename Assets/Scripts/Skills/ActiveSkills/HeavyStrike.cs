using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyStrike : MeleeAttackSingleTarget
{
    private SkillData skillData;

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
        tags = new HashSet<string> { "Melee", "Strike", "Physical"};

        skillData = ScriptableObject.CreateInstance<SkillData>();

        skillData.damagePerLevel = new List<float[]>
        {
            new float[] { 10f, 0f, 0f, 0f, 0f },
            new float[] { 20f, 0f, 0f, 0f, 0f },
            new float[] { 30f, 0f, 0f, 0f, 0f },
            new float[] { 40f, 0f, 0f, 0f, 0f },
            new float[] { 50f, 0f, 0f, 0f, 0f }
        };
        skillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        skillData.attackSpeedPerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };
        skillData.strikeRangePerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };


        int skillLevel = 0;
        if (skillData == null)
        {
            return;
        }
        damage = skillData.damagePerLevel[skillLevel];
        strikeRange = skillData.strikeRangePerLevel[skillLevel];
        attackSpeed = skillData.attackSpeedPerLevel[skillLevel];
        manaCost = skillData.manaCostPerLevel[skillLevel];

    }




}
