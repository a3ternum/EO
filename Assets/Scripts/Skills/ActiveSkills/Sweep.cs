using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sweep : MeleeAttackArea
{
    private SkillData sweepSkillData;
    protected AreaVisual sweepVisual;


    protected override void Awake() // Initialize Sweep skill data
    {
        base.Awake();
        skillName = "Sweep";

        sweepVisual = Resources.Load<AreaVisual>("SweepVisual");
        sweepSkillData = ScriptableObject.CreateInstance<SkillData>();

        tags = new HashSet<string> { "Area", "Physical", "Melee"};


        sweepSkillData.damagePerLevel = new List<float[]>
        {
            new float[] { 10f, 0f, 0f, 0f, 0f },
            new float[] { 20f, 0f, 0f, 0f, 0f },
            new float[] { 30f, 0f, 0f, 0f, 0f },
            new float[] { 40f, 0f, 0f, 0f, 0f },
            new float[] { 50f, 0f, 0f, 0f, 0f }
        };
        sweepSkillData.manaCostPerLevel = new List<float> { 15f, 0f, 0f, 0f, 0f };
        sweepSkillData.attackSpeedPerLevel = new List<float> { 3f, 1.1f, 1.2f, 1.3f, 1.4f };
        sweepSkillData.radiusPerLevel = new List<float> { 1.5f, 1.5f, 1.5f, 1.5f, 1.5f };
        sweepSkillData.durationPerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };
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
        duration = sweepSkillData.durationPerLevel[skillLevel];
    }

 
    protected override void AttackEffect()
    {
        SpawnSweepEffect();
    }


    protected void SpawnSweepEffect()
    {
        // Spawn the ice nova at the location
        AreaVisual sweepEffect = Instantiate(sweepVisual, user.transform.position, Quaternion.identity);
        sweepEffect.radius = radius;
        Debug.Log("sweep effect radius: " + sweepEffect.radius);
        sweepEffect.duration = duration;
        Debug.Log("sweep effect duration: " + sweepEffect.duration);

        DamageCreaturesInArea(user.transform.position);
    }


}