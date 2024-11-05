using UnityEngine;

public class Player : Creature
{
    private playerMovement movement;
    private playerCombat combat;

    void Awake()
    {
        movement = GetComponent<playerMovement>();
        combat = GetComponent<playerCombat>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        health = 100;
        damage = 10;
        attackSpeed = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        movement.setMovement();
        combat.playerAttack();
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
