using UnityEngine;

public class Golem1 : Enemy
{
    protected override void Start()
    {
        base.Start();
        experienceValue = 200f;
    }
}
