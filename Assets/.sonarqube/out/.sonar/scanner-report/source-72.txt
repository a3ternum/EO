using UnityEngine;
[CreateAssetMenu(fileName = "Elemental Damage Small", menuName = "SkillTree/Elemental Damage Small")]

public class ElementalDamageSmall : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Elemental Damage Small";
    }

    public override void ApplyEffect(Player player)
    {
        for (int i = 1; i < 4; i++)
        {
            player.playerStats.damageIncreases[i] += 0.1f;
        }
    }
}
