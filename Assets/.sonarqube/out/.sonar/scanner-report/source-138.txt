using UnityEngine;
[CreateAssetMenu(fileName = "Area Of Attack Increase Small", menuName = "Area Of Attack Increase Small")]
public class AreaOfAttackIncreaseSmall : NodeEffect
{
    public void Start()
    {
        effectName = "Area of Attack Increase Small";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.areaOfEffectIncreases += 0.08f;
    }
}
