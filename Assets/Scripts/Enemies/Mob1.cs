using UnityEngine;

public class Mob1 : Enemy
{
    protected override void Start()
    {
        base.Start();

        experienceValue = 50f;
        Debug.Log("setting attack range to " + activeSkill.strikeRange + " + " + creatureStats.strikeRangeFlat);
        attackRange = activeSkill.strikeRange + creatureStats.strikeRangeFlat;
    }

}
