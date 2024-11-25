using UnityEngine;

public class Mob1 : Enemy
{
    protected override void Start()
    {
        base.Start();

        experienceValue = 50f;
    }

}
