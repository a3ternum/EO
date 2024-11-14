using UnityEngine;

[CreateAssetMenu(fileName = "NodeEffect", menuName = "SkillTree/NodeEffect")]
public class NodeEffect : ScriptableObject
{
    public string effectName;

    public virtual void ApplyEffect(Player player)
    {
        
    }
}