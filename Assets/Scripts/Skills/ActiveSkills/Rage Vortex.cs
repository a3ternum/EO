using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RageVortex : MeleeAttackArea
{
    public RageVortexProjectile vortex;

    private SkillData rageVortexSkillData;



    protected override void Awake() // Initialize heavy strike skill data
    {
        base.Awake();
        skillName = "Rage Vortex";

        // Load the vortex prefab from the Resources folder
        vortex = Resources.Load<RageVortexProjectile>("RageVortexProjectile");

        if (vortex == null)
        {
            Debug.LogError("Vortex prefab not found in Resources folder!");
        }

        rageVortexSkillData = ScriptableObject.CreateInstance<SkillData>();
        rageVortexSkillData.damagePerLevel = new List<float[]>
        {
            new float[] { 10f, 0f, 0f, 0f, 0f },
            new float[] { 20f, 0f, 0f, 0f, 0f },
            new float[] { 30f, 0f, 0f, 0f, 0f },
            new float[] { 40f, 0f, 0f, 0f, 0f },
            new float[] { 50f, 0f, 0f, 0f, 0f }
        };
        rageVortexSkillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        rageVortexSkillData.attackSpeedPerLevel = new List<float> { 3f, 1.1f, 1.2f, 1.3f, 1.4f };
        rageVortexSkillData.radiusPerLevel = new List<float> { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
        rageVortexSkillData.castTimePerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        rageVortexSkillData.durationPerLevel = new List<float> { 3f, 4f, 5f, 6f, 7f };
        rageVortexSkillData.tickRatePerLevel = new List<float>() { 0.2f, 0.58f, 0.56f, 0.54f, 0.52f };
        rageVortexSkillData.projectileSpeedPerLevel = new List<float> { 5f, 5.2f, 5.4f, 5.6f, 5.8f };

    }


    protected override void Start()
    {
        base.Start();


        int skillLevel = 0;
        if (rageVortexSkillData == null)
        {
            Debug.Log("rage vortex skill data is null");
            return;
        }
        damage = rageVortexSkillData.damagePerLevel[skillLevel];
        radius = rageVortexSkillData.radiusPerLevel[skillLevel];
        attackSpeed = rageVortexSkillData.attackSpeedPerLevel[skillLevel];
        manaCost = rageVortexSkillData.manaCostPerLevel[skillLevel];
        castTime = rageVortexSkillData.castTimePerLevel[skillLevel];
        duration = rageVortexSkillData.durationPerLevel[skillLevel];
        tickRate = rageVortexSkillData.tickRatePerLevel[skillLevel];
        projectileSpeed = rageVortexSkillData.projectileSpeedPerLevel[skillLevel];
    }



    public override void ActivateSkill()
    {
        bool canActivate = CanActivate();
        if (canActivate)
        {
            StartCoroutine(AttackCoroutine());
            OnActivate();
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        RageVortexProjectile projectile = Instantiate(vortex, user.transform.position, Quaternion.identity);
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - user.transform.position;
        projectile.Initialize(direction, this);
    }
}