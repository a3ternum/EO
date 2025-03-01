using UnityEngine;

[CreateAssetMenu(fileName = "Fury Bolts", menuName = "SkillTree/Fury Bolts")]
public class FuryBolts : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Fury Bolts";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.projectileDamageIncreases += 0.25f;
        player.playerStats.movementSpeedIncreases += 0.08f;
        player.playerStats.evasionFlat += 150;
    }
}
