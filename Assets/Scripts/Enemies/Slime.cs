using UnityEngine;

public class Slime : Enemy
{
    protected override void Start()
    {
        base.Start();

        experienceValue = 50f;

        chaseScript.setSpeed(currentMovementSpeed);
    }

}
