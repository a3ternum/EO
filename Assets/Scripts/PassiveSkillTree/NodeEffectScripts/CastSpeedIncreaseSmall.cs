using UnityEngine;
[CreateAssetMenu(fileName = "Cast Speed Increase Small", menuName = "SkillTree/Cast Speed Increase Small")]

public class CastSpeedIncreaseSmall : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        effectName = "Cast Speed Increase Small";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.castSpeedIncreases += 0.08f;
    }
}
