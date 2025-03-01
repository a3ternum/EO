using UnityEngine;
[CreateAssetMenu(fileName = "Born To Fight", menuName = "SkillTree/Born To Fight")]
public class BornToFight : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Born to Fight";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.damageIncreases[0] += 0.25f;
        player.playerStats.strength += 20;
        player.playerStats.armourFlat += 100;
    }
}
