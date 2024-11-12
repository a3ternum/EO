using UnityEngine;

public class Slime : Enemy
{
    protected override void Start()
    {
        base.Start();

        health = 25;
        healthBarComponent.setMaxHealth(health);
        healthBarComponent.setHealth(health);

        damage = 5;
        attackSpeed = 0.5f;
        speed = 10f;
        experienceValue = 50f;

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
