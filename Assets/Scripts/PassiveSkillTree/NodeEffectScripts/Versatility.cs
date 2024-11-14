using UnityEngine;
[CreateAssetMenu(fileName = "Versatility", menuName = "SkillTree/Versatility")]
public class Versatility : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Versatility";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.attackSpeedIncreases += 0.1f;
        player.playerStats.dexterity += 20;
        player.playerStats.intelligence += 20;
        player.playerStats.movementSpeedIncreases += 0.1f;
    }
}
