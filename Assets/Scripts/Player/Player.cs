using UnityEngine;

public class Player : Creature
{
    private PlayerMovement movement;
    private PlayerCombat combat;
    private PlayerExperience experience;

    public PlayerData playerData;

    [SerializeField]
    private Skill activeSkill = null;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
        experience = GetComponent<PlayerExperience>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        health = playerData.health;
        damage = playerData.damage;
        attackSpeed = playerData.attackSpeed;
        nextAttackTime = 0;


        activeSkill = gameObject.AddComponent<HeavyStrike>();
        activeSkill.user = this;
        combat.SetActiveSkill((Attack)activeSkill);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        movement.Update();

        if (combat.attackInput() & Time.time >= nextAttackTime) // maybe this should be in the combat script
        {
            Debug.Log("player position is" + transform.position);
            combat.playerAttack();
            nextAttackTime = Time.time + 1f / attackSpeed;
        }

        experience.Update();
    }

    public void gainExperience(float experienceGained)
    {
        experience.gainExperience(experienceGained);
    }




    public override void TakeDamage(float damage, float time = 4)
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
        movement.movePlayer();
    }
}
