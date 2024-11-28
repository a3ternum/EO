using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FreezingPulse : OffensiveSpell
{
    private SkillData skillData;



    protected override void Awake() // Initialize heavy strike skill data
    {
        base.Awake();
        skillName = "Freezing Pulse";

        projectilePrefab = Resources.Load<Projectile>("FreezingPulseProjectile");

        if (projectilePrefab == null)
        {
            Debug.LogError("freezing pulse prefab not found in Resources folder!");
        }

        skillData = ScriptableObject.CreateInstance<SkillData>();

        tags = new HashSet<string> { "Area", "Cold", "Projectile" };

        skillData.damagePerLevel = new List<float[]>
        {
            new float[] { 0f, 0f, 25f, 0f, 0f },
            new float[] { 0f, 0f, 30f, 0f, 0f },
            new float[] { 0f, 0f, 35f, 0f, 0f },
            new float[] { 0f, 0f, 40f, 0f, 0f },
            new float[] { 0f, 0f, 45f, 0f, 0f }
        };
        skillData.manaCostPerLevel = new List<float> { 15f, 20f, 23f, 27f, 32f };
        skillData.radiusPerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };
        skillData.castSpeedPerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };
        skillData.durationPerLevel = new List<float> { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
        skillData.tickRatePerLevel = new List<float>() { 1f, 1f, 1f, 1f, 1f };
        skillData.projectileSpeedPerLevel = new List<float> { 10f, 10f, 10f, 10f, 10f };

    }


    protected override void Start()
    {
        base.Start();


        int skillLevel = 0;
        if (skillData == null)
        {
            Debug.Log("Freezing pulse skill data is null");
            return;
        }
        damage = skillData.damagePerLevel[skillLevel];
        radius = skillData.radiusPerLevel[skillLevel];
        manaCost = skillData.manaCostPerLevel[skillLevel];
        castSpeed = skillData.castSpeedPerLevel[skillLevel];
        duration = skillData.durationPerLevel[skillLevel];
        tickRate = skillData.tickRatePerLevel[skillLevel];
        projectileSpeed = skillData.projectileSpeedPerLevel[skillLevel];
    }


    protected override void AttackEffect()
    {
        LaunchProjectiles(DetermineTargetLocation());
    }



}
