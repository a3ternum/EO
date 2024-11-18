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
    public float currentStrength;
    public float currentIntelligence;
    public float currentDexterity;

    protected GameObject experienceBarPrefab;
    protected GameObject ourExperienceBarObject;
    public ExperienceBar experienceBarComponent;

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

    }
    protected virtual void InitializeExperienceBar()
    {
        // load experience bar from the resources folder
        experienceBarPrefab = Resources.Load<GameObject>("ExperienceBar");
        if (experienceBarPrefab == null)
        {
            Debug.LogError("ExperienceBar prefab not found in Resources folder!");
        }

        ourExperienceBarObject = Instantiate(experienceBarPrefab, transform.position, Quaternion.identity);
        experienceBarComponent = ourExperienceBarObject.GetComponent<ExperienceBar>();

        experienceBarComponent.setParent((Player)this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        InitializePlayerStats();
        currentHealth = currentMaxHealth;

        InitializeHealthBar(); // initialize health bar
        InitializeExperienceBar(); // initialize experience bar
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

        base.Die();
    }

    protected override void UpdateStats()
    {
        base.UpdateStats();
        currentStrength = playerStats.strength * (1 + playerStats.strengthIncreases) * (1 + playerStats.strengthMoreMultipliers);
        currentIntelligence = playerStats.intelligence * (1 + playerStats.intelligenceIncreases) * (1 + playerStats.intelligenceMoreMultipliers);
        currentDexterity = playerStats.dexterity * (1 + playerStats.dexterityIncreases) * (1 + playerStats.dexterityMoreMultipliers);
        currentMaxHealth = (playerStats.healthBase + playerStats.healthFlat +
            currentStrength / 10 * 2) * (1 + playerStats.healthIncreases) * (1 + playerStats.healthMoreMultipliers);
        currentMaxMana = (playerStats.manaBase + playerStats.manaFlat) * playerStats.manaIncreases * playerStats.manaMoreMultipliers;


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
        currentMana = Math.Min(currentMana, currentMaxMana);

    }

    void FixedUpdate()
    {
        playerMovement.movePlayer();
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

    
    




    

}
