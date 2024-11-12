using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RageVortex : MeleeAttackArea
{
    public float projectileSpeed = 3f;
    public GameObject vortex;

    private SkillData rageVortexSkillData;


    protected override void Awake() // Initialize heavy strike skill data
    {
        base.Awake();
        skillName = "Rage Vortex";

        // Load the vortex prefab from the Resources folder
        vortex = Resources.Load<GameObject>("vortex");

        if (vortex == null)
        {
            Debug.LogError("Vortex prefab not found in Resources folder!");
        }

        rageVortexSkillData = ScriptableObject.CreateInstance<SkillData>();
        rageVortexSkillData.damagePerLevel = new List<float> { 10f, 20f, 30f, 40f, 50f };
        rageVortexSkillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        rageVortexSkillData.attackSpeedPerLevel = new List<float> { 3f, 1.1f, 1.2f, 1.3f, 1.4f };
        rageVortexSkillData.rangePerLevel = new List<float> { 3f, 3.4f, 3.6f, 3.8f, 4f };
        rageVortexSkillData.castTimePerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        rageVortexSkillData.durationPerLevel = new List<float> { 3f, 4f, 5f, 6f, 7f };
        rageVortexSkillData.tickRatePerLevel = new List<float>() { 0.6f, 0.58f, 0.56f, 0.54f, 0.52f };

        
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
    }



    public override void ActivateSkill()
    {
        bool canActivate = CanActivate();
        if (canActivate)
        {
            StartCoroutine(AttackCoroutine());
            OnActivate();
            //StartCoroutine(AoEColliderRoutine());
            LaunchProjectile();
        }
    }

    protected override IEnumerator AoEColliderRoutine()
    {
        // Create a vortex GameObject
        GameObject newVortex = Instantiate(vortex, user.transform.position, Quaternion.identity);
        CircleCollider2D collider = newVortex.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - user.transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // move the vortex
            newVortex.transform.position += (Vector3)direction.normalized * projectileSpeed * Time.deltaTime;

            // check for collisions
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(newVortex.transform.position, collider.radius);
            List<Creature> targetsList = new List<Creature>();

            foreach (var hitCollider in hitColliders)
            {
                if (user is Player)
                {
                    if (collider.gameObject.layer == enemyLayer)
                    {
                        Creature enemy = hitCollider.GetComponent<Creature>();
                        if (enemy != null)
                        {
                            if (!lastHitTime.ContainsKey(enemy) || Time.time - lastHitTime[enemy] >= tickRate)
                            {
                                targetsList.Add(enemy);
                                lastHitTime[enemy] = Time.time;
                            }

                        }
                    }
                }
                if (user is Enemy)
                {
                    if (collider.gameObject.layer == playerLayer)
                    {
                        Creature player = hitCollider.GetComponent<Creature>();
                        if (player != null)
                        {
                            if (!lastHitTime.ContainsKey(player) || Time.time - lastHitTime[player] >= tickRate)
                            {
                                targetsList.Add(player);
                                lastHitTime[player] = Time.time;
                            }
                        }
                    }

                }
                // Apply damage to all enemies in range
                ApplyDamageAndEffects(targetsList);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Destroy(newVortex);
        }
    }

    protected override void ApplyDamageAndEffects(List<Creature> targets)
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
        GameObject newVortex = Instantiate(vortex, user.transform.position, Quaternion.identity);
        
        VortexProjectile projectile = newVortex.GetComponent<VortexProjectile>();
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - user.transform.position;
        projectile.Initialize(direction, projectileSpeed, duration, tickRate, damage, enemyLayer, terrainLayer, playerLayer);
    }
}