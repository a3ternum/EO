using UnityEngine;
[CreateAssetMenu(fileName = "Discipline And Training", menuName = "SkillTree/Discipline And Training")]

public class DisciplineAndTraining : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Discipline and Training";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.healthFlat += 50;
        player.playerStats.healthIncreases += 0.1f;
        
        for (int i = 1; i < 4; i++)
        {
            player.playerStats.damageIncreases[i] += 0.25f;
            player.playerStats.damageIncreases[i] += 0.25f;
            player.playerStats.damageIncreases[i] += 0.25f;
        }
    }
}
