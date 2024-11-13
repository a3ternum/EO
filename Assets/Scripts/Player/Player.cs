using UnityEngine;

public class Player : Creature
{
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private PlayerExperience playerExperience;

    public PlayerStats playerStats;

    protected override void Awake()
    {
        base.Awake();

        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerExperience = GetComponent<PlayerExperience>();

        
    }

    protected virtual void InitializePlayerStats()
    {
        currentHealth = playerStats.baseHealth;
        currentArmour = playerStats.armourBase * playerStats.armourIncreases;
        currentEvasion = playerStats.evasionBase * playerStats.evasionIncreases;
        currentPhysicalDamageReduction = playerStats.physicalDamageReduction;
        currentAttackSpeed = playerStats.baseAttackSpeed;
        currentCastSpeed = playerStats.baseCastSpeed;
        currentMovementSpeed = playerStats.baseMovementSpeed;
        currentAdditionalProjectiles = playerStats.additionalProjectiles;
        currentResistances = playerStats.resistances;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        InitializePlayerStats();

        InitializeHealthBar(); // initialize health bar

        playerCombat.SetActiveSkill(activeSkill);
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
    }

    public void gainExperience(float playerExperienceGained)
    {
        playerExperience.gainExperience(playerExperienceGained);
    }




    public override void TakeDamage(float[] damage, float time = 4)
    {
        base.TakeDamage(damage, time);
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


    void FixedUpdate()
    {
        playerMovement.movePlayer();
    }
}
