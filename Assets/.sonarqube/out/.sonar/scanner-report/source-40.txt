using UnityEngine;

[CreateAssetMenu(fileName = "Stabbing Frenzy", menuName = "SkillTree/Stabbing Frenzy")]
public class StabbingFrenzy : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Stabbing Frenzy";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.attackSpeedIncreases += 0.16f;
        player.playerStats.dexterity += 20;
        player.playerStats.evasionFlat += 100;
    }
}
