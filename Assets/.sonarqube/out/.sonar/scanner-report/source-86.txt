using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
public class Fireball : OffensiveSpell
{

    private SkillData fireballSkillData;



    protected override void Awake() // Initialize heavy strike skill data
    {
        base.Awake();
        skillName = "Fireball";

        projectilePrefab = Resources.Load<Projectile>("FireballProjectile");

        if (projectilePrefab == null)
        {
            Debug.LogError("Fireball prefab not found in Resources folder!");
        }

        fireballSkillData = ScriptableObject.CreateInstance<SkillData>();

        tags = new HashSet<string> { "Area", "Fire", "Projectile" };

        fireballSkillData.damagePerLevel = new List<float[]>
        {
            new float[] { 0f, 25f, 0f, 0f, 0f },
            new float[] { 0f, 30f, 0f, 0f, 0f },
            new float[] { 0f, 35f, 0f, 0f, 0f },
            new float[] { 0f, 40f, 0f, 0f, 0f },
            new float[] { 0f, 45f, 0f, 0f, 0f }
        };
        fireballSkillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        fireballSkillData.radiusPerLevel = new List<float> { 0.18f, 0.18f, 0.18f, 0.18f, 0.18f };
        fireballSkillData.castSpeedPerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };
        fireballSkillData.durationPerLevel = new List<float> { 10f, 10, 10f, 10f, 10f };
        fireballSkillData.tickRatePerLevel = new List<float>() { 1f, 1f, 1f, 1f, 1f };
        fireballSkillData.projectileSpeedPerLevel = new List<float> { 5f, 5.2f, 5.4f, 5.6f, 5.8f };

    }


    protected override void Start()
    {
        base.Start();


        int skillLevel = 0;
        if (fireballSkillData == null)
        {
            Debug.Log("Fireball skill data is null");
            return;
        }
        damage = fireballSkillData.damagePerLevel[skillLevel];
        radius = fireballSkillData.radiusPerLevel[skillLevel];
        manaCost = fireballSkillData.manaCostPerLevel[skillLevel];
        castSpeed = fireballSkillData.castSpeedPerLevel[skillLevel];
        duration = fireballSkillData.durationPerLevel[skillLevel];
        tickRate = fireballSkillData.tickRatePerLevel[skillLevel];
        projectileSpeed = fireballSkillData.projectileSpeedPerLevel[skillLevel];
    }

    protected override void AttackEffect()
    {
        LaunchProjectiles(DetermineTargetLocation());
    }





}
