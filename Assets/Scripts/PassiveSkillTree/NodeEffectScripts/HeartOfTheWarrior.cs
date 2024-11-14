using UnityEngine;
[CreateAssetMenu(fileName = "Heart Of The Warrior", menuName = "SkillTree/Heart Of The Warrior")]
public class HeartOfTheWarrior : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Heart of the Warrior";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.healthFlat += 25;
        player.playerStats.healthIncreases += 0.25f;
    }
}
