using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicArrowAttack : RangedAttack
{
    //public BasicArrowProjectile basicArrow;

    private SkillData basicArrowSkillData;

    protected override void Awake()
    {
        base.Awake();
        skillName = "BasicArrowAttack";

        // Load the basic arrow prefab from the Resources folder
        projectilePrefab = Resources.Load<Projectile>("BasicArrow");
        if (projectilePrefab == null)
        {
            Debug.LogError("BasicArrow prefab not found in Resources folder!");
        }        
     

        // Load the vortex prefab from the Resources folder
        //basicArrow = Resources.Load<BasicArrowProjectile>("BasicArrow");

        //if (basicArrow == null)
        //{
        //    Debug.LogError("basicArrow prefab not found in Resources folder!");
        //}
        basicArrowSkillData = ScriptableObject.CreateInstance<SkillData>();
        basicArrowSkillData.damagePerLevel = new List<float> { 10f, 20f, 30f, 40f, 50f };
        basicArrowSkillData.manaCostPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        basicArrowSkillData.attackSpeedPerLevel = new List<float> { 3f, 1.1f, 1.2f, 1.3f, 1.4f };
        basicArrowSkillData.rangePerLevel = new List<float> { 3f, 3.4f, 3.6f, 3.8f, 4f };
        basicArrowSkillData.castTimePerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        basicArrowSkillData.durationPerLevel = new List<float> { 3f, 4f, 5f, 6f, 7f };
        basicArrowSkillData.tickRatePerLevel = new List<float>() { 0.6f, 0.58f, 0.56f, 0.54f, 0.52f };
        basicArrowSkillData.projectileSpeedPerLevel = new List<float> { 5f, 5.2f, 5.4f, 5.6f, 5.8f };
        basicArrowSkillData.radiusPerLevel = new List<float> { 0f, 0f, 0f, 0f, 0f };
        basicArrowSkillData.pierceCountPerLevel = new List<float> { 2f, 2f, 3f, 4f, 5f };
       
    }

    protected override void Start()
    {
        base.Start();

        int skillLevel = 0;
        if (basicArrowSkillData == null)
        {
            Debug.Log("skill data is null");
            return;
        }
        damage = basicArrowSkillData.damagePerLevel[skillLevel];
        range = basicArrowSkillData.rangePerLevel[skillLevel];
        attackSpeed = basicArrowSkillData.attackSpeedPerLevel[skillLevel];
        manaCost = basicArrowSkillData.manaCostPerLevel[skillLevel];
        castTime = basicArrowSkillData.castTimePerLevel[skillLevel];
        duration = basicArrowSkillData.durationPerLevel[skillLevel];
        tickRate = basicArrowSkillData.tickRatePerLevel[skillLevel];
        projectileSpeed = basicArrowSkillData.projectileSpeedPerLevel[skillLevel];

    }
}