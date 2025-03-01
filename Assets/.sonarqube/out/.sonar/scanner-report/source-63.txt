using UnityEngine;
[CreateAssetMenu(fileName = "Attack Speed Small", menuName = "SkillTree/Attack Speed Small")]

public class AttackSpeedSmall : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Attack Speed Small";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.attackSpeedIncreases += 0.04f;
    }
}
