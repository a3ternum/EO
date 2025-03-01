using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileDamageSmall", menuName = "SkillTree/ProjectileDamageSmall")]
public class ProjectileDamageSmall : NodeEffect
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       effectName = "Projectile Damage Small";
    }

    public override void ApplyEffect(Player player)
    {
        player.playerStats.projectileDamageIncreases += 0.12f;
    }
}
