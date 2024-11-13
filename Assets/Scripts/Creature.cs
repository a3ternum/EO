using System.Collections;
using System;
using UnityEngine;
using System.Linq;
// this is a template class for all creatures in the game

public class Creature : MonoBehaviour
{
    public CreatureStats creatureStats;

  
    public float currentHealth;
    public float currentMana;
    protected float currentArmour;
    protected float currentEvasion;
    protected float currentPhysicalDamageReduction;
    public float currentAccuracy;
    public float currentAttackSpeed;
    protected float currentCastSpeed;
    public float currentMovementSpeed;
    public int currentAdditionalProjectiles;
    protected float[] currentResistances;

    public bool damagedRecently = false;
    protected int entropyValue = 0;
    protected int maxEntropyValue;

    protected float nextAttackTime = 0f;

    public GameObject healthBarPrefab;
    protected HealthBar healthBarComponent;
    private GameObject healthBarObject;
    private Canvas canvas;

    [SerializeField]
    protected Skill activeSkill;

   
    protected virtual float[] CalculateDamageTaken(float[] damage, float accuracy)
    {
        if (EvasionHitOutcome(currentEvasion, accuracy))
        {


            float[] finalDamageTaken = new float[5];

            // this method will be the most convoluted method in the game
            // Here we will handle all damage calculation of incoming hits. 

            // damage consists of 5 elements: physical, fire, ice, lightning, poison
            // each element will be mitigated by a difference source and the final damage count is the sum of each individual damages

            // physical damage is mitigated by armor. Calculate this first
            // the formula is DR(A, Draw) = A / (A + 5 * Draw)

            float DamageReduction = currentArmour / (currentArmour + 5 * damage[0]);
            DamageReduction = DamageReduction + currentPhysicalDamageReduction;
            finalDamageTaken[0] = damage[0] * (1 - DamageReduction);

            // fire, ice, lightning, poison damage is mitigated by resistances. Calculate this next

            for (int i = 1; i < 5; i++)
            {
                finalDamageTaken[i] = damage[i] * ((100 - currentResistances[i-1]) / 100);
            }
            return finalDamageTaken;

            // there will be far more damage mitigation resources in the future. These will all have to be calculated here

        }

        return new float[5];
    }


    protected virtual void InitializeCreatureStats()
    {
        currentHealth = creatureStats.baseHealth;
        currentArmour = creatureStats.armourBase * creatureStats.armourIncreases;
        currentEvasion = creatureStats.evasionBase * creatureStats.evasionIncreases;
        currentPhysicalDamageReduction = creatureStats.physicalDamageReduction;
        currentAttackSpeed = creatureStats.baseAttackSpeed;
        currentCastSpeed = creatureStats.baseCastSpeed;
        currentMovementSpeed = creatureStats.baseMovementSpeed;
        currentAdditionalProjectiles = creatureStats.additionalProjectiles;
        currentResistances = creatureStats.resistances;
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
    }




    protected virtual void Start()
    {
        InitializeCreatureStats(); // initializeCreature stats

        InitializeHealthBar(); // initialize health bar

    }
    


    protected void UpdateHealthBar()
    {
        if (healthBarComponent != null)
        {
            healthBarComponent.setHealth(currentHealth); // update health value

            healthBarObject.transform.position = transform.position + new Vector3(0, 0.6f, 0); // move healthbar to creature position

        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        Destroy(healthBarObject);
        Destroy(healthBarComponent);
    }

    public virtual void TakeDamage(float[] damage, float time = 4)
    {
        // implement evasion method to determine if enemy even gets hit
        float[] damageAfterDefenses = CalculateDamageTaken(damage);
        float combinedDamage = damageAfterDefenses.Sum();
        Debug.Log("Combined damage after defenses is " + combinedDamage);
        if (currentHealth > 0)
        {
            currentHealth -= combinedDamage;
            damagedRecently = true;
            StartCoroutine(ResetDamagedRecently(time));
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
    protected virtual IEnumerator ResetDamagedRecently(float time)
    {
        yield return new WaitForSeconds(time);
        damagedRecently = false;
        
    }

    protected virtual void Update()
    {
        UpdateHealthBar();
    }

   
    protected virtual void InitializeHealthBar()
    {
        // instantiate the health bar prefab
        healthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);

        healthBarComponent = healthBarObject.GetComponent<HealthBar>();
        // set the parent of the health bar to this creature
        healthBarComponent.setParent(this);

        healthBarComponent.setMaxHealth(currentHealth);

        canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null || canvas.renderMode != RenderMode.WorldSpace)
        {
            Debug.LogError("World Space Canvas not found. Make sure there's a canvas set to World Space.");
            return;
        }

        healthBarObject.transform.SetParent(canvas.transform, false); // set the health bar object as a child of the canvas
        healthBarObject.transform.position += new Vector3(0, 0.6f, 0); // set the position of the health bar above the creature

    }

    protected virtual bool EvasionHitOutcome(float evasion, float accuracy)
    {
        float chanceToEvade = (float)(1 - (accuracy * 1.25f) / (accuracy + Math.Pow(evasion * 0.2f, 0.9f)));

        return true;
    }
}
