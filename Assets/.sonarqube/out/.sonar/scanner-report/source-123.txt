using UnityEngine;

[CreateAssetMenu(fileName = "HealthIncreaseSmall", menuName = "SkillTree/HealthIncreaseSmall")]
public class HealthIncreaseSmall : NodeEffect
{
    private void Start()
    {
        effectName = "Health Increase Small";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.healthIncreases += 0.04f;
    }
}

