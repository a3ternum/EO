using UnityEngine;

[CreateAssetMenu(fileName = "PhysicalDamageIncreaseSmall", menuName = "SkillTree/PhysicalDamageIncreaseSmall")]
public class PhysicalDamageIncreaseSmall : NodeEffect
{
    private void Start()
    {
        effectName = "Physical Damage Increase Small";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.damageIncreases[0] += 0.04f;
    }
}
