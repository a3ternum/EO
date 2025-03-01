using UnityEngine;

[CreateAssetMenu(fileName = "Health And Armour Small", menuName = "SkillTree/Health And Armour Small")]
public class HealthAndArmourSmall : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Health and Armour Small";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.healthIncreases += 0.06f;
        player.playerStats.armourIncreases += 0.06f;
    }
}
