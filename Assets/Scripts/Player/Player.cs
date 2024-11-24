using UnityEngine;
using System;

public class Player : Creature
{
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private PlayerExperience playerExperience;

    public PlayerStats playerStats;

    public float currentMaxMana;
    public float currentMana;
    public float currentManaRegen;

    public float currentStrength;
    public float currentIntelligence;
    public float currentDexterity;

    public ExperienceBar experienceBarComponent;
    public HealthOrbUI healthOrbComponent;
    public ManaOrbUI manaOrbComponent;


    protected override void Awake()
    {
        base.Awake();

        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerExperience = GetComponent<PlayerExperience>();

        
    }

    protected virtual void InitializePlayerStats()
    {
        InitializeCreatureStats();
        currentMaxMana = (playerStats.manaBase + playerStats.manaFlat) * (1 + playerStats.manaIncreases) * (1 + playerStats.manaMoreMultipliers);
        currentManaRegen = currentMaxMana * (playerStats.manaRegenIncreases) * (1 + playerStats.manaRegenMoreMultipliers)
        + (playerStats.manaRegenBase + playerStats.manaRegenFlat) * (1 + playerStats.manaRegenIncreases) * (1 + playerStats.manaRegenMoreMultipliers);
        currentMana = currentMaxMana;

    }
    protected virtual void InitializeExperienceBar()
    {
        // load experience bar from the resources folder
        ExperienceBar experienceBarComponentTemp = Resources.Load<ExperienceBar>("ExperienceBar");
        if (experienceBarComponentTemp == null)
        {
            Debug.LogError("ExperienceBar prefab not found in Resources folder!");
        }
        else
        {
            experienceBarComponent = Instantiate(experienceBarComponentTemp, transform.position, Quaternion.identity);
            experienceBarComponent.SetParent(this);
        }

        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null || canvas.renderMode != RenderMode.WorldSpace)
        {
            Debug.LogError("World Space Canvas not found. Make sure there's a canvas set to World Space.");
            return;
        }

        experienceBarComponent.transform.SetParent(canvas.transform, false);

    }

    protected virtual void InitializeHealthAndManaOrb()
    {
        // load health orb from the resources folder
        HealthOrbUI healthOrbComponentTemp = Resources.Load<HealthOrbUI>("HealthOrb");
        if (healthOrbComponentTemp == null)
        {
            Debug.LogError("HealthOrb prefab not found in Resources folder!");
        }
        // load mana orb from the resources folder
        ManaOrbUI manaOrbComponentTemp = Resources.Load<ManaOrbUI>("ManaOrb");
        if (manaOrbComponentTemp == null)
        {
            Debug.LogError("ManaOrb prefab not found in Resources folder!");
        }

        healthOrbComponent = Instantiate(healthOrbComponentTemp, transform.position, Quaternion.identity);
        healthOrbComponent.SetParent(this);

        manaOrbComponent = Instantiate(manaOrbComponentTemp, transform.position, Quaternion.identity);
        manaOrbComponent.SetParent(this);
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null || canvas.renderMode != RenderMode.WorldSpace)
        {
            Debug.LogError("World Space Canvas not found. Make sure there's a canvas set to World Space.");
            return;
        }

        healthOrbComponent.transform.SetParent(canvas.transform, false);
        manaOrbComponent.transform.SetParent(canvas.transform, false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        InitializePlayerStats();
        currentHealth = currentMaxHealth;

        InitializeHealthBar(); // initialize health bar
        InitializeExperienceBar(); // initialize experience bar
        InitializeHealthAndManaOrb(); // initialize health and mana orb
        playerCombat.SetActiveSkill(activeSkill);
    }

  

    public void gainExperience(float playerExperienceGained)
    {
        playerExperience.gainExperience(playerExperienceGained);
    }




    protected override void Die()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        RespawnMenu respawnMenu = canvas.GetComponentInChildren<RespawnMenu>(true);
        if (respawnMenu != null)
        {
            respawnMenu.ShowRespawnMenu();
        }
        else
        {
            Debug.LogError("RespawnMenu not found in the scene!");
        }

        healthBarComponent.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    protected override void UpdateStats()
    {
        base.UpdateStats();
        currentStrength = playerStats.strength * (1 + playerStats.strengthIncreases) * (1 + playerStats.strengthMoreMultipliers);
        currentIntelligence = playerStats.intelligence * (1 + playerStats.intelligenceIncreases) * (1 + playerStats.intelligenceMoreMultipliers);
        currentDexterity = playerStats.dexterity * (1 + playerStats.dexterityIncreases) * (1 + playerStats.dexterityMoreMultipliers);
        currentMaxHealth = (playerStats.healthBase + playerStats.healthFlat +
            currentStrength / 10 * 2) * (1 + playerStats.healthIncreases) * (1 + playerStats.healthMoreMultipliers);
        currentMaxMana = (playerStats.manaBase + playerStats.manaFlat) * (1 + playerStats.manaIncreases) * (1 + playerStats.manaMoreMultipliers);


    }

    public void EnablePlayerActions(bool enable)
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = enable;
        }
        if (playerCombat != null)
        {
            playerCombat.enabled = enable;
        }
    }

    protected override void OnApplicationQuit()
    {
        playerStats.resetPlayerData();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        playerMovement.Update();

        if (playerCombat.attackInput() & Time.time >= nextAttackTime) // maybe this should be in the playerCombat script
        {
            playerCombat.playerAttack();
            nextAttackTime = Time.time + 1f / currentAttackSpeed;
        }

        playerExperience.Update();
        UpdateStats();

        currentHealth += currentHealthRegen * Time.deltaTime;

        currentMana += currentManaRegen * Time.deltaTime;
        currentMana = Math.Min(currentMana, currentMaxMana);

    }

    void FixedUpdate()
    {
        playerMovement.movePlayer();
    }
   

    
    




    

}
