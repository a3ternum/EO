using UnityEngine;

public class Bandit : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        health = 50;
        healthBarComponent.setMaxHealth(health);
        healthBarComponent.setHealth(health);

        damage = 10;
        attackSpeed = 0.5f;
        speed = 1f;
        experienceValue = 100f;

        chaseScript.setSpeed(speed);
    }

    public override void enemyAttack()
    {
        base.enemyAttack();

    }

    public override void takeDamage(float damage)
    {
        base.takeDamage(damage);
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
