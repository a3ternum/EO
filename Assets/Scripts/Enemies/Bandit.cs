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


}
