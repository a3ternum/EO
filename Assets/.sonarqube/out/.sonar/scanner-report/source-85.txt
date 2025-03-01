using UnityEngine;
[CreateAssetMenu(fileName = "Master Of the Arena", menuName = "SkillTree/Master Of the Arena")]
public class ArtOfTheGladiator : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Master Of the Arena";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.damageIncreases[0] += 0.2f;
        player.playerStats.strength += 20;
        player.playerStats.healthRegenIncreases += 0.01f;
    }
}
