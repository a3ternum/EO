using UnityEngine;

public class Player : Creature
{
    private playerMovement movement;
    private playerCombat combat;
    private playerExperience experience;
    void Awake()
    {
        movement = GetComponent<playerMovement>();
        combat = GetComponent<playerCombat>();
        experience = GetComponent<playerExperience>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        health = 100;
        damage = 10;
        attackSpeed = 1;
        nextAttackTime = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        movement.setMovement();

        if (combat.attackInput() & Time.time >= nextAttackTime)
        {
            combat.playerAttack();
            nextAttackTime = Time.time + 1f / attackSpeed;
        }

        experience.Update();
    }

    public void gainExperience(float experienceGained)
    {
        experience.gainExperience(experienceGained);
    }




    public override void takeDamage(float damage)
    {
        base.takeDamage(damage);
    }

    void FixedUpdate()
    {
        movement.movePlayer();
    }
}
