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
        speed = 10f;
        experienceValue = 100f;

        chaseScript.setSpeed(speed);
    }

    public override void enemyAttack()
    {
        base.enemyAttack();

    }

    public override void TakeDamage(float damage, float time = 4)
    {
        base.TakeDamage(damage, time);
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
