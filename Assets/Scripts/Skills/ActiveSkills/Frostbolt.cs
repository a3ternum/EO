using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Frostbolt : OffensiveSpell
{
    private SkillData skillData;



    protected override void Awake() 
    {
        base.Awake();
        skillName = "FrostBolt";

        projectilePrefab = Resources.Load<Projectile>("FrostBoltProjectile");

        if (projectilePrefab == null)
        {
            Debug.LogError("Frostbolt prefab not found in Resources folder!");
        }

        skillData = ScriptableObject.CreateInstance<SkillData>();

        tags = new HashSet<string> { "Area", "Cold", "Projectile" };

        skillData.damagePerLevel = new List<float[]>
        {
            new float[] { 0f, 0f, 15f, 0f, 0f },
            new float[] { 0f, 0f, 20f, 0f, 0f },
            new float[] { 0f, 0f, 25f, 0f, 0f },
            new float[] { 0f, 0f, 30f, 0f, 0f },
            new float[] { 0f, 0f, 35f, 0f, 0f }
        };
        skillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        skillData.radiusPerLevel = new List<float> { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f };
        skillData.castSpeedPerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };
        skillData.durationPerLevel = new List<float> { 6f, 6f, 6f, 6f, 6f };
        skillData.tickRatePerLevel = new List<float>() { 1f, 1f, 1f, 1f, 1f };
        skillData.projectileSpeedPerLevel = new List<float> { 3f, 3f, 3f, 3f, 3f };

    }


    protected override void Start()
    {
        base.Start();


        int skillLevel = 0;
        if (skillData == null)
        {
            Debug.Log("Frost bolt skill data is null");
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
