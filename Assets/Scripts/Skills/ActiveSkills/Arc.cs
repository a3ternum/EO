using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class Arc : OffensiveSpell
{
    private SkillData skillData;

    private int[] baseChainCountList; // Number of times the arc can chain to other enemies
    private int baseChainCount;
    private float arcRange = 4f; // Maximum distance the arc can chain to other enemies
    // Arc fires a chain of lightning that will chain to nearby enemies

    protected override void Awake() // Initialize arc skill data
    {
        base.Awake();
        

    }

    public override void InitializeSkill()
    {
        base.InitializeSkill();
        skillName = "Arc";

        projectilePrefab = Resources.Load<Projectile>("ArcProjectile");

        if (projectilePrefab == null)
        {
            Debug.LogError("Arc prefab not found in Resources folder!");
        }

        skillData = ScriptableObject.CreateInstance<SkillData>();

        tags = new HashSet<string> { "Chain", "Lightning" };

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

        baseChainCountList = new int[] { 3, 4, 5, 6, 7 };
    }

    protected override void Start()
    {
        base.Start();


        int skillLevel = 0;
        if (skillData == null)
        {
            Debug.Log("Arc skill data is null");
            return;
        }
        damage = skillData.damagePerLevel[skillLevel];
        manaCost = skillData.manaCostPerLevel[skillLevel];
        castSpeed = skillData.castSpeedPerLevel[skillLevel];
        duration = skillData.durationPerLevel[skillLevel];
        tickRate = skillData.tickRatePerLevel[skillLevel];
        baseChainCount = baseChainCountList[skillLevel];
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
        StartCoroutine(ArcLoop());
    }

    protected IEnumerator ArcLoop()
    {
        Vector2 startingLocation = user.transform.position;
        Creature creatureToHit = user;
        Vector2 tempStartingLocation = startingLocation;
        HashSet<Creature> creaturesAlreadyHit = new HashSet<Creature>();
        for (int i = 0; i < baseChainCount; i++)
        {
            (creatureToHit, creaturesAlreadyHit) = FindNearestOtherTarget(creatureToHit, startingLocation, creaturesAlreadyHit);
            if (creatureToHit != null)
            {
                tempStartingLocation = creatureToHit.transform.position;
                FireSingleArc(startingLocation, creatureToHit);
                startingLocation = tempStartingLocation;

                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                break;
            }
        }
    }

    protected void FireSingleArc(Vector2 startingLocation, Creature creatureToHit)
    {
        // instantiate the arc prefab
        ArcEffect arcEffect = Instantiate(Resources.Load<ArcEffect>("ArcEffect"), startingLocation, Quaternion.identity);
        arcEffect.startLocation = startingLocation;
        arcEffect.targetLocation = creatureToHit.transform.position;
        creatureToHit.TakeDamage(CalculateDamage(), user);

    }
    
    protected (Creature, HashSet<Creature>) FindNearestOtherTarget(Creature previousCreature, Vector2 startingLocation, HashSet<Creature> creaturesAlreadyHit)
    {

        // Find the nearest enemy within the arc range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(startingLocation, arcRange);

        Creature closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (var collider in colliders)
        {
            if ((user is Player && collider.gameObject.layer == enemyLayer) || (user is Enemy && collider.gameObject.layer == playerLayer))
            {
                Creature creature = collider.GetComponent<Creature>();
             
                if ((creature != null && previousCreature != null && !creaturesAlreadyHit.Contains(creature)) || (creature != null && previousCreature == null))
                {
                    float distance = Vector2.Distance(startingLocation, creature.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = creature;
                    }
                }


            }
        }
        creaturesAlreadyHit.Add(closestTarget);
        return (closestTarget, creaturesAlreadyHit);
    }

}
