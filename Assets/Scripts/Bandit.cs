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
    }

    public void takeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;

            if (health <= 0)
            {
                die();
            }
        }

    }

    public void enemyAttack()
    {


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
