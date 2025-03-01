using UnityEngine;
[CreateAssetMenu(fileName = "Health And Mana Small", menuName = "SkillTree/Health And Mana Small")]

public class HealthAndManaIncreaseSmall : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Health and Mana Increase Small";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.healthIncreases += 0.08f;
        player.playerStats.manaIncreases += 0.12f;
    }
}
