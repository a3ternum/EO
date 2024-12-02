using UnityEngine;

public class Bandit : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        chaseDistance = 6f;
        experienceValue = 100f;

        chaseScript.setSpeed(currentMovementSpeed);
    }

}
