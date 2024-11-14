using UnityEngine;
[CreateAssetMenu(fileName = "Sanctity", menuName = "SkillTree/Sanctity")]

public class Sanctity : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Sanctity";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.armourIncreases += 0.4f;
        player.playerStats.healthRegenIncreases += 0.01f;
        player.playerStats.strength += 10;
        player.playerStats.intelligence += 10;
    }
}
