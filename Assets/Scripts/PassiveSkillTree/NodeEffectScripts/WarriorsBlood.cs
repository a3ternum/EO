using UnityEngine;
[CreateAssetMenu(fileName = "Warrior's Blood", menuName = "SkillTree/Warrior's Blood")]
public class WarriorsBlood : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Warrior's Blood";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.healthFlat += 30;
        player.playerStats.healthRegenIncreases += 0.02f;
    }
}
