using UnityEngine;

[CreateAssetMenu(fileName = "Strength Small", menuName = "SkillTree/Strength Small")]
public class StrengthSmall : NodeEffect
{
    private void Start()
    {
        effectName = "Strength Small";
    }
    public override void ApplyEffect(Player player)
    {
        player.playerStats.strength += 10;
    }

}
