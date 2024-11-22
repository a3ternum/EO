using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class Arc : OffensiveSpell
{
    private SkillData skillData;

    private int baseChainCount = 3; // Number of times the arc can chain to other enemies
    // Arc fires a chain of lightning that will chain to nearby enemies

    protected override void Awake() // Initialize arc skill data
    {
        base.Awake();
        skillName = "Freezing Pulse";

        projectilePrefab = Resources.Load<Projectile>("ArcProjectile");

        if (projectilePrefab == null)
        {
            Debug.LogError("Arc prefab not found in Resources folder!");
        }

        skillData = ScriptableObject.CreateInstance<SkillData>();

        tags = new HashSet<string> { "Chain", "Lightning"};

        skillData.damagePerLevel = new List<float[]>
        {
            new float[] { 0f, 0f, 0f, 10f, 0f },
            new float[] { 0f, 0f, 0f, 12f, 0f },
            new float[] { 0f, 0f, 0f, 16f, 0f },
            new float[] { 0f, 0f, 0f, 21f, 0f },
            new float[] { 0f, 0f, 0f, 27f, 0f }
        };
        skillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        skillData.castSpeedPerLevel = new List<float> { 1f, 1f, 1f, 1f, 1f };
        skillData.durationPerLevel = new List<float> { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };
        skillData.tickRatePerLevel = new List<float>() { 1f, 1f, 1f, 1f, 1f };

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



    public override void ActivateSkill()
    {
        bool canActivate = CanActivate();
        if (canActivate)
        {
            StartCoroutine(ActivateSkillCoroutine());
        }
    }
    protected override IEnumerator ActivateSkillCoroutine()
    {
        yield return StartCoroutine(SpellCoroutine());
        OnActivate();
        LaunchProjectiles(DetermineTargetLocation());
    }


}
