using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "skillData", menuName = "Skill Data")]
public class SkillData : ScriptableObject
{
    public List<float[]> damagePerLevel; // damage of skill
    public List<float> castSpeedPerLevel; // base cast speed of skill
    public List<float> attackSpeedPerLevel; // base attack speed of skill
    public List<float> durationPerLevel; // how long the skill lasts
    public List<float> tickRatePerLevel; // how often the skill deals damage
    public List<float> manaCostPerLevel; // mana cost of skill
    public List<float> strikeRangePerLevel; // strikeRange of skill
    public List<float> projectileSpeedPerLevel; // how fast the projectile moves
    public List<float> radiusPerLevel; // area of effect radius of skill
    public List<float> pierceCountPerLevel; // how many enemies the skill can pierce
    public List<float> rangePerLevel; // how far the skill can be activated from

}