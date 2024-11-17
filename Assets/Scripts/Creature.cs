using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
// this is a template class for all creatures in the game

public class Creature : MonoBehaviour
{
    public CreatureStats creatureStats;


    public float currentHealth;
    public float currentMaxHealth;
    public float currentHealthRegen;
    protected float currentArmour;
    protected float currentEvasion;
    protected float currentPhysicalDamageReduction;
    public float currentAccuracy;
    public float currentAttackSpeed;
    protected float currentCastSpeed;
    public float currentMovementSpeed;
    public int currentAdditionalProjectiles;
    protected float[] currentResistances;
    protected float currentEvadeChance;
    protected float currentBlockChance;

    protected float currentCriticalStrikeChance;
    protected float currentCriticalStrikeMultiplier;

    protected float currentIgniteChance;
    protected float currentChillChance;
    protected float currentFreezeChance;
    protected float currentShockChance;

    protected float currentAreaOfEffectIncrease;
    protected float currentAreaOfEffectDamageIncrease;

    protected bool isIgnited = false;
    protected bool isChilled = false;
    protected bool isFrozen = false;
    protected bool isShocked = false;

    protected float igniteDamage;
    protected float chillEffect;
    protected float freezeDuration;
    protected float shockEffect;

    

    public bool damagedRecently = false;

    protected const int maxEntropy = 100;
    protected int entropyValue;

    protected float nextAttackTime = 0f;

    protected GameObject healthBarPrefab;
    protected GameObject ourHealthBarObject;
    protected HealthBar healthBarComponent;

    private Canvas canvas;

    [SerializeField]
    protected Skill activeSkill;

    // Event to trigger stat updates
    public event Action OnStatsChanged;


    protected virtual void InitializeCreatureStats()
    {
        currentMaxHealth = (creatureStats.healthBase + creatureStats.healthFlat) * (1 + creatureStats.healthIncreases) * (1 + creatureStats.healthMoreMultipliers);
        currentHealthRegen = (creatureStats.healthRegenBase + creatureStats.healthRegenFlat) * (1 + creatureStats.healthRegenIncreases) * (1 + creatureStats.healthRegenMoreMultipliers)
            + currentMaxHealth * (creatureStats.healthRegenIncreases) * (1 + creatureStats.healthRegenMoreMultipliers);
        currentArmour = creatureStats.armourFlat * (1 + creatureStats.armourIncreases);
        currentEvasion = creatureStats.evasionFlat * (1 + creatureStats.evasionIncreases);
        currentPhysicalDamageReduction = creatureStats.physicalDamageReduction;
        currentAttackSpeed = (creatureStats.attackSpeedBase + creatureStats.attackSpeedFlat) * (1 + creatureStats.attackSpeedIncreases) * (1 + creatureStats.attackSpeedMoreMultipliers);
        currentCastSpeed = (creatureStats.castSpeedBase + creatureStats.castSpeedFlat) * (1 + creatureStats.castSpeedIncreases) * (1 + creatureStats.castSpeedMoreMultipliers);
        currentMovementSpeed = creatureStats.movementSpeedBase * (1 + creatureStats.movementSpeedIncreases);
        currentAdditionalProjectiles = creatureStats.additionalProjectiles;
        currentResistances = creatureStats.resistances;
        currentEvadeChance = (creatureStats.evadeChanceBase + creatureStats.evadeChanceFlat) * (1 + creatureStats.evadeChanceIncreases);
        currentBlockChance = (creatureStats.blockChanceBase + creatureStats.blockChanceFlat) * (1 + creatureStats.blockChanceIncreases);
        currentCriticalStrikeChance = (creatureStats.criticalStrikeChanceBase + creatureStats.criticalStrikeChanceFlat) * (1 + creatureStats.criticalStrikeChanceIncreases);
        currentCriticalStrikeMultiplier = (creatureStats.criticalStrikeMultiplierBase + creatureStats.criticalStrikeMultiplierFlat) * (1 + creatureStats.criticalStrikeMultiplierIncreases);
        currentAreaOfEffectIncrease = creatureStats.areaOfEffectIncreases;
        currentAreaOfEffectDamageIncrease = creatureStats.areaOfEffectDamageIncreases;

        currentIgniteChance = creatureStats.igniteChanceBase + creatureStats.igniteChanceFlat;
        currentChillChance = creatureStats.chillChanceBase + creatureStats.chillChanceFlat;
        currentFreezeChance = creatureStats.freezeChanceBase + creatureStats.freezeChanceFlat;
        currentShockChance = creatureStats.shockChanceBase + creatureStats.shockChanceFlat;
    }
    public void SetCanvas(Canvas canvas)
    {
        Debug.Log("setting canvas");
        this.canvas = canvas;
        if (ourHealthBarObject != null)
        {
            ourHealthBarObject.transform.SetParent(canvas.transform, false);
        }
    }

    protected virtual void InitializeHealthBar()
    {
        // instantiate the health bar prefab
        ourHealthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBarComponent = ourHealthBarObject.GetComponent<HealthBar>();

        // set the parent of the health bar to this creature
        healthBarComponent.setParent(this);

        healthBarComponent.setMaxHealth(currentHealth);

        canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null || canvas.renderMode != RenderMode.WorldSpace)
        {
            Debug.LogError("World Space Canvas not found. Make sure there's a canvas set to World Space.");
            return;
        }

        ourHealthBarObject.transform.SetParent(canvas.transform, false); // set the health bar object as a child of the canvas
        ourHealthBarObject.transform.position += new Vector3(0, 0.6f, 0); // set the position of the health bar above the creature

    }

    protected virtual void Awake()
    {
        if (activeSkill != null)
        {
            System.Type skillType = activeSkill.GetType();
            activeSkill = (Skill)gameObject.AddComponent(skillType);
            activeSkill.user = this;
        }
        else
        {
            Debug.LogError("active skill is not assigned");
        }

        // Load the basic arrow prefab from the Resources folder
        healthBarPrefab= Resources.Load<GameObject>("HealthBar");
        if (healthBarPrefab == null)
        {
            Debug.LogError("HealthBar prefab not found in Resources folder!");
        }

        entropyValue = UnityEngine.Random.Range(0, maxEntropy);

        // Subscribe to the event
        OnStatsChanged += UpdateStats;
    }

    protected virtual void Start()
    {
        InitializeCreatureStats(); // initializeCreature stats
        currentHealth = currentMaxHealth;
        InitializeHealthBar(); // initialize health bar
    }

 

    protected void UpdateHealthBar()
    {
        if (healthBarComponent != null)
        {
            healthBarComponent.setHealth(currentHealth); // update health value
            ourHealthBarObject.transform.position = transform.position + new Vector3(0, 0.6f, 0); // update health bar
        }
    }

    protected virtual void Die()
    {
        Destroy(ourHealthBarObject);
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float[] damage, Creature attacker, float time = 4)
    {
        bool isCrit = (UnityEngine.Random.Range(0, 100) < attacker.currentCriticalStrikeChance * 100) ? true : false;
        if (isCrit)
        {
            for (int i = 0; i < damage.Length; i++)
            {
                damage[i] = damage[i] * attacker.currentCriticalStrikeMultiplier;
            }
        }

        float[] damageAfterDefenses = CalculateDamageTaken(damage, attacker, isCrit);
        ComputeAilmentEffects(damageAfterDefenses, attacker, isCrit);

        float combinedDamage = damageAfterDefenses.Sum();
        if (currentHealth > 0)
        {
            currentHealth -= combinedDamage;
            OnStatsChanged?.Invoke(); // trigger the event to update stats
            damagedRecently = true;
            StartCoroutine(ResetDamagedRecently(time));
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
    protected virtual float[] CalculateDamageTaken(float[] damage, Creature attacker, bool isCrit)
    {
        if (DoWeBlock(currentBlockChance))
        {
            // perform on block actions
            return new float[5];
        }
        if (DoWeEvade(currentEvasion, currentEvadeChance, attacker.currentAccuracy))
        {

            // perform on evade actions
            return new float[5];
        }


        // this method will be the most convoluted method in the game
        // Here we will handle all damage calculation of incoming hits. 
        float[] finalDamageTaken = new float[5];

        // damage consists of 5 elements: physical, fire, ice, lightning, poison
        // each element will be mitigated by a difference source and the final damage count is the sum of each individual damages

        // take shock into account
        if (isShocked)
        {
            for (int i = 0; i < 4; i++)
            {
                damage[i] = damage[i] * (1 + shockEffect);
            }
        }
        // the bool isCrit can be used to deal with crit reduction logic!

        // physical damage is mitigated by armor. Calculate this first
        // the formula is DR(A, Draw) = A / (A + 5 * Draw)
        float DamageReduction = currentArmour / (currentArmour + 5 * damage[0]);
        DamageReduction = DamageReduction + currentPhysicalDamageReduction;
        finalDamageTaken[0] = damage[0] * (1 - DamageReduction);

        // fire, ice, lightning, poison damage is mitigated by resistances. Calculate this next

        for (int i = 1; i < 5; i++)
        {
            finalDamageTaken[i] = damage[i] * ((100 - currentResistances[i - 1]) / 100);
        }
        return finalDamageTaken;

        // there will be far more damage mitigation resources in the future. These will all have to be calculated here
        // the player class will override this method once ascendancy type nodes are introduces.


    }

    protected virtual IEnumerator ResetDamagedRecently(float time)
    {
        yield return new WaitForSeconds(time);
        damagedRecently = false;
        OnStatsChanged?.Invoke(); // trigger the event to update stats

    }

    protected virtual bool DoWeBlock(float blockChance)
    {
        if (UnityEngine.Random.Range(0, 100) < blockChance)
        {
            OnStatsChanged?.Invoke(); // trigger the event to update stats
            return true;
        }
        return false;
    }

    protected virtual bool DoWeEvade(float evasion, float evadeChance, float accuracy)
    {
        // check if creature evades attack entirely
        if (UnityEngine.Random.Range(0, 100) < evadeChance)
        {
            OnStatsChanged?.Invoke();
            return true;
        }


        float hitChance = accuracy / ((1.25f * accuracy) + (float)Math.Pow(evasion, 0.9f));

        float hitThreshold = hitChance * maxEntropy;
        if (entropyValue < hitThreshold)
        {
            entropyValue += (int)Math.Round(maxEntropy / hitChance);
            return true; // attack is evaded
        }
        else
        {
            entropyValue = Math.Max(0, entropyValue - maxEntropy);
            return false; // attack hits
        }

    }

    protected virtual void UpdateStats()
    {
        InitializeCreatureStats(); // check to see if rest of code is redundant
        //currentMaxHealth = (creatureStats.healthBase + creatureStats.healthFlat) * (1+creatureStats.healthIncreases) * (1 + creatureStats.healthMoreMultipliers);
        //currentHealthRegen = (creatureStats.healthRegenBase + creatureStats.healthRegenFlat) + currentMaxHealth * (creatureStats.healthRegenIncreases) * (creatureStats.healthRegenMoreMultipliers);
        //currentArmour = creatureStats.armourFlat * (1 + creatureStats.armourIncreases);
        //currentEvasion = creatureStats.evasionFlat * (1 + creatureStats.evasionIncreases);
        //currentPhysicalDamageReduction = creatureStats.physicalDamageReduction;
        //currentAttackSpeed = (creatureStats.attackSpeedBase + creatureStats.attackSpeedFlat) * (1 + creatureStats.attackSpeedIncreases) * (1 + creatureStats.attackSpeedMoreMultipliers);
        //currentCastSpeed = (creatureStats.castSpeedBase + creatureStats.castSpeedFlat) * (1 + creatureStats.castSpeedIncreases) * (1 + creatureStats.castSpeedMoreMultipliers);
        //currentMovementSpeed = creatureStats.movementSpeedBase * (1 + creatureStats.movementSpeedIncreases);
        //currentAdditionalProjectiles = creatureStats.additionalProjectiles;
        //currentResistances = creatureStats.resistances;
        //currentEvadeChance = (creatureStats.evadeChanceBase + creatureStats.evadeChanceFlat) * (1 + creatureStats.evadeChanceIncreases);
        //currentBlockChance = (creatureStats.blockChanceBase + creatureStats.blockChanceFlat) * (1 + creatureStats.blockChanceIncreases);
        //currentCriticalStrikeChance = (creatureStats.criticalStrikeChanceBase + creatureStats.criticalStrikeChanceFlat) * (1 + creatureStats.criticalStrikeChanceIncreases);
        //currentCriticalStrikeMultiplier = (creatureStats.criticalStrikeMultiplierBase + creatureStats.criticalStrikeMultiplierFlat) * (1 + creatureStats.criticalStrikeMultiplierIncreases);

        //currentIgniteChance = creatureStats.igniteChanceBase + creatureStats.igniteChanceFlat;
        //currentChillChance = creatureStats.chillChanceBase + creatureStats.chillChanceFlat;
        //currentFreezeChance = creatureStats.freezeChanceBase + creatureStats.freezeChanceFlat;
        //currentShockChance = creatureStats.shockChanceBase + creatureStats.shockChanceFlat;

        ApplyChillEffect();
        ApplyFreezeEffect();
    }


    protected float IgniteDamage(float fireDamage)
    {
        return fireDamage * 0.9f;
    }

    protected float ChillEffect(float coldDamage)
    {
        float chillEffect = 0.05f + (0.25f * (coldDamage / currentMaxHealth));
        return Mathf.Clamp(chillEffect, 0.05f, 0.3f);
    }
    protected float ChillDuration(float coldDamage)
    {
        // Example formula: duration is proportional to the cold damage
        float baseDuration = 2f; // base duration in seconds
        float duration = baseDuration * (coldDamage / currentMaxHealth);
        return Mathf.Clamp(duration, 1f, 5f); // clamp duration between 1 and 5 seconds
    }
    protected float FreezeDuration(float coldDamage)
    {
        float freezeDuration = 100 * (coldDamage / currentMaxHealth);
        return Mathf.Clamp(freezeDuration, 0.3f, 3f); // freeze is capped at 3 seconds and minimum 0.3 seconds
    }

    protected float ShockEffect(float lightningDamage)
    {
        return 0.15f + (0.35f * (lightningDamage / currentMaxHealth));
    }

    protected void ComputeAilmentEffects(float[] damage, Creature attacker, bool isCrit)
    {
        float[] damageAfterDefenses = CalculateDamageTaken(damage, attacker, isCrit);

        // loop through elemental damages to determine whether ignite, chill, freeze or shock is applied to player.
        if (damageAfterDefenses[1] > 0) // fire damage
        {
            if (UnityEngine.Random.Range(0, 100) < attacker.currentIgniteChance || isCrit) // apply ignite
            {
                float igniteDamage = IgniteDamage(damageAfterDefenses[1]); // ignite damage is proportional to fire damage
                StartCoroutine(ApplyIgnite(igniteDamage));
            }

        }
        if (damageAfterDefenses[2] > 0) // cold damage
        {
            float roll = UnityEngine.Random.Range(0, 100);
            if (roll < attacker.currentChillChance || isCrit) // apply chill
            {
                float chillEffect = ChillEffect(damageAfterDefenses[2]); // chill effect is proportional to cold damage
                float chillDuration = ChillDuration(damageAfterDefenses[2]); // duration is proportional to cold damage
                StartCoroutine(ApplyChill(chillEffect, chillDuration));

            }
            if (roll < attacker.currentFreezeChance || isCrit) // apply freeze
            {
                float freezeDuration = FreezeDuration(damageAfterDefenses[2]); // freeze duration is proportional to cold damage
                StartCoroutine(ApplyFreeze(freezeDuration));
            }
        }
        if (damageAfterDefenses[3] > 0)
        {
            if (UnityEngine.Random.Range(0, 100) < attacker.currentShockChance || isCrit) // apply shock
            {
                float shockEffect = ShockEffect(damageAfterDefenses[3]); // shock effect is proportional to lightning damage
                StartCoroutine(ApplyShock(shockEffect));
            }
        }

    }

    protected IEnumerator ApplyChill(float chillEffect, float duration = 4)
    {
        this.isChilled = true;
        this.chillEffect = chillEffect;
        yield return new WaitForSeconds(duration);
        this.isChilled = false;
        this.chillEffect = 0;
    }

    protected IEnumerator ApplyFreeze(float duration)
    {
        this.isFrozen = true;
        this.freezeDuration = duration;
        yield return new WaitForSeconds(duration);
        this.isFrozen = false;
        this.freezeDuration = 0; 
    }

    protected IEnumerator ApplyShock(float shockEffect, float duration = 4)
    {
        this.isShocked = true;
        this.shockEffect = shockEffect;
        yield return new WaitForSeconds(duration);
        this.isShocked = false;
        this.shockEffect = 0;
    }

    protected IEnumerator ApplyIgnite(float igniteDamage, float duration = 4)
    {
        this.isIgnited = true;
        this.igniteDamage = igniteDamage;
        float tickRate = 0.1f; // Apply damage every 0.1 seconds

        int numberOfTicks = Mathf.CeilToInt(duration / tickRate);
        float damagePerTick = igniteDamage / numberOfTicks;

        for (int i = 0; i < numberOfTicks; i++)
        {
            if (currentHealth > 0)
            {
                currentHealth -= damagePerTick;
                this.igniteDamage = this.igniteDamage - damagePerTick;
                if (currentHealth <= 0)
                {
                    Die();
                    break;
                }
            }
            yield return new WaitForSeconds(tickRate);
        }
        this.isIgnited = false;
    }

    protected void ApplyChillEffect()
    {
        if (isChilled)
        {
            currentMovementSpeed = currentMovementSpeed * (1 - chillEffect);
            currentAttackSpeed = currentAttackSpeed * (1 - chillEffect);
            currentCastSpeed = currentCastSpeed * (1 - chillEffect);
        }
    }
    protected void ApplyFreezeEffect()
    {
        if (isFrozen)
        {
     
            currentMovementSpeed = 0;
            currentAttackSpeed = 0;
            currentCastSpeed = 0;
        }
    }

    protected virtual void OnApplicationQuit()
    {  
       creatureStats.resetCreatureData();
    }



    protected virtual void Update()
    {
        // inside the creature update method we will be performing an insane amount of parameter updates
        // we are constantly checking for changes in the creature's stats and updating the corresponding stat accordingly.
        UpdateStats();

        // regen health based on health regen
        currentHealth += currentHealthRegen * Time.deltaTime;
        currentHealth = Math.Min(currentHealth, currentMaxHealth);
        UpdateHealthBar();
    }
}