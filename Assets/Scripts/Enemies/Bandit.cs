using UnityEngine;

public class Bandit : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        
        healthBarComponent.setMaxHealth(currentHealth);
        healthBarComponent.setHealth(currentHealth);

        
        experienceValue = 100f;

        chaseScript.setSpeed(currentMovementSpeed);
    }

    public override void enemyAttack()
    {
        base.enemyAttack();

    }

    public override void TakeDamage(float[] damage, float time = 4)
    {
        base.TakeDamage(damage, time);
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
