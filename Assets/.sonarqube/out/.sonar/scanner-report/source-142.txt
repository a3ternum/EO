using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MeleeAttackSingleTarget
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
        skillName = "Basic Attack";
        tags = new HashSet<string> { "Melee", "Strike", "Physical" };

        skillData = ScriptableObject.CreateInstance<SkillData>();

        skillData.damagePerLevel = new List<float[]>
        {
            new float[] { 10f, 0f, 0f, 0f, 0f },
            new float[] { 10f, 0f, 0f, 0f, 0f },
            new float[] { 10f, 0f, 0f, 0f, 0f },
            new float[] { 10f, 0f, 0f, 0f, 0f },
            new float[] { 10f, 0f, 0f, 0f, 0f }
        };
        skillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        skillData.attackSpeedPerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };
        skillData.strikeRangePerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };


        if (skillData == null)
        {
            return;
        }
        int skillLevel = 0;
        damage = skillData.damagePerLevel[skillLevel];
        strikeRange = skillData.strikeRangePerLevel[skillLevel];
        attackSpeed = skillData.attackSpeedPerLevel[skillLevel];
        manaCost = skillData.manaCostPerLevel[skillLevel];

    }
}
