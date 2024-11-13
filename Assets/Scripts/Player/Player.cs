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

    protected override void Awake()
    {
        base.Awake();

        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerExperience = GetComponent<PlayerExperience>();

        
    }

    protected virtual void InitializePlayerStats()
    {
        currentMaxHealth = (playerStats.healthBase + playerStats.healthFlat +
            (playerStats.strength * playerStats.strengthIncreases) / 10 * 2) *
            playerStats.healthIncreases * playerStats.healthMoreMultipliers;
        currentHealthRegen = (playerStats.healthRegenBase + playerStats.healthRegenFlat) * playerStats.healthRegenIncreases * playerStats.healthRegenMoreMultipliers;
        currentArmour = playerStats.armourBase * playerStats.armourIncreases;
        currentEvasion = playerStats.evasionBase * playerStats.evasionIncreases;
        currentPhysicalDamageReduction = playerStats.physicalDamageReduction;
        currentAttackSpeed = playerStats.attackSpeedBase;
        currentCastSpeed = playerStats.castSpeedBase;
        currentMovementSpeed = playerStats.movementSpeedBase;
        currentAdditionalProjectiles = playerStats.additionalProjectiles;
        currentResistances = playerStats.resistances;
        currentEvadeChance = (playerStats.evadeChanceBase + playerStats.evadeChanceFlat)*playerStats.evadeChanceIncreases;
        currentBlockChance = (playerStats.blockChanceBase + playerStats.blockChanceBase)*playerStats.blockChanceIncreases;
        currentMaxMana = (playerStats.manaBase + playerStats.manaFlat) * playerStats.manaIncreases * playerStats.manaMoreMultipliers;

        currentHealth = currentMaxHealth;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        InitializePlayerStats();

        InitializeHealthBar(); // initialize health bar

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

    protected void UpdateStats()
    {
        currentMaxHealth = (playerStats.healthBase + playerStats.healthFlat + 
            (playerStats.strength * playerStats.strengthIncreases)/10 * 2) * 
            playerStats.healthIncreases * playerStats.healthMoreMultipliers;

        currentHealthRegen = (playerStats.healthRegenBase + playerStats.healthRegenFlat) * playerStats.healthRegenIncreases * playerStats.healthRegenMoreMultipliers;
        currentArmour = playerStats.armourBase * playerStats.armourIncreases;
        currentEvasion = playerStats.evasionBase * playerStats.evasionIncreases;
        currentPhysicalDamageReduction = playerStats.physicalDamageReduction;
        currentAttackSpeed = playerStats.attackSpeedBase;
        currentCastSpeed = playerStats.castSpeedBase;
        currentMovementSpeed = playerStats.movementSpeedBase * playerStats.movementSpeedIncreases;
        currentAdditionalProjectiles = playerStats.additionalProjectiles;
        currentResistances = playerStats.resistances;
        currentEvadeChance = (playerStats.evadeChanceBase + playerStats.evadeChanceFlat) * playerStats.evadeChanceIncreases;
        currentBlockChance = (playerStats.blockChanceBase + playerStats.blockChanceBase) * playerStats.blockChanceIncreases;
        currentMaxMana = (playerStats.manaBase + playerStats.manaFlat) * playerStats.manaIncreases * playerStats.manaMoreMultipliers;
        currentCriticalStrikeChance = (playerStats.criticalStrikeChanceBase + playerStats.criticalStrikeChanceFlat) * playerStats.criticalStrikeChanceIncreases;
        currentCriticalStrikeMultiplier = (playerStats.criticalStrikeMultiplierBase + playerStats.criticalStrikeMultiplierFlat) * playerStats.criticalStrikeMultiplierIncreases;


        currentIgniteChance = playerStats.igniteChanceBase + playerStats.igniteChanceFlat;
        currentChillChance = playerStats.chillChanceBase + playerStats.chillChanceFlat;
        currentFreezeChance = playerStats.freezeChanceBase + playerStats.freezeChanceFlat;
        currentShockChance = playerStats.shockChanceBase + playerStats.shockChanceFlat;


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
}
