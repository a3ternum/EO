using UnityEngine;
[CreateAssetMenu(fileName = "Bravery", menuName = "SkillTree/Bravery")]
public class Bravery : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Bravery";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.armourIncreases += 0.24f;
        player.playerStats.evasionIncreases += 0.24f;

        player.playerStats.healthIncreases += 0.1f;
    }
}
