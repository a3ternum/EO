using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "skillData", menuName = "Skill Data")]
public class SkillData : ScriptableObject
{
    public List<float> damagePerLevel;
    public List<float> castTimePerLevel;
    public List<float> attackSpeedPerLevel;
    public List<float> durationPerLevel;
    public List<float> tickRatePerLevel;
    public List<float> manaCostPerLevel;
    public List<float> rangePerLevel;
    public List<float> projectileSpeedPerLevel;
    public List<float> radiusPerLevel;
    public List<float> pierceCountPerLevel;

}