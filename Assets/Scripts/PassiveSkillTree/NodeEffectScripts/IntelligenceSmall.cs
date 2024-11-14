using UnityEngine;
[CreateAssetMenu(fileName = "Intelligence Small", menuName = "SkillTree/Intelligence Small")]
public class IntelligenceSmall : NodeEffect
{
    private void Start()
    {
        effectName = "Intelligence Small";
    }
    public override void ApplyEffect(Player player)
    {
        player.playerStats.intelligence += 10;
    }

}
