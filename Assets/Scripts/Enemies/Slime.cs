using UnityEngine;

public class Slime : Enemy
{
    protected override void Start()
    {
        base.Start();

        healthBarComponent.setMaxHealth(currentHealth);
        healthBarComponent.setHealth(currentHealth);

        
        experienceValue = 50f;

        chaseScript.setSpeed(currentMovementSpeed);
    }

    protected void Update()
    {
        base.Update();
    }
}
