using UnityEngine;
[CreateAssetMenu(fileName = "Dexterity Small", menuName = "SkillTree/Dexterity Small")]
public class DexteritySmall : NodeEffect

{
    private void Start()
    {
        effectName = "Dexterity Small";
    }
    public override void ApplyEffect(Player player)
    {
        player.playerStats.dexterity += 10;
    }

}
