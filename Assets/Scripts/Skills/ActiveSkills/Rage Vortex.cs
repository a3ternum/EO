using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RageVortex : MeleeAttackArea
{
    public VortexProjectile vortex;

    private SkillData rageVortexSkillData;



    protected override void Awake() // Initialize heavy strike skill data
    {
        base.Awake();
        skillName = "Rage Vortex";

        // Load the vortex prefab from the Resources folder
        vortex = Resources.Load<VortexProjectile>("vortex");

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
        rageVortexSkillData.rangePerLevel = new List<float> { 3f, 3.4f, 3.6f, 3.8f, 4f };
        rageVortexSkillData.castTimePerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        rageVortexSkillData.durationPerLevel = new List<float> { 3f, 4f, 5f, 6f, 7f };
        rageVortexSkillData.tickRatePerLevel = new List<float>() { 0.6f, 0.58f, 0.56f, 0.54f, 0.52f };
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
        range = rageVortexSkillData.rangePerLevel[skillLevel];
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


    public override void ApplyDamageAndEffects(List<Creature> targets)
    {
        if (targets != null && targets.Count > 0)
        {
            foreach (var target in targets)
            {
                target.TakeDamage(CalculateDamage());
                TriggerHitEffect(target.transform.position);
            }
        }
    }

    private void LaunchProjectile()
    {
        VortexProjectile projectile = Instantiate(vortex, user.transform.position, Quaternion.identity);
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - user.transform.position;
        projectile.Initialize(direction, projectileSpeed, duration, tickRate, damage, enemyLayer, terrainLayer, playerLayer, this);
    }
}