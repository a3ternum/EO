using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "playerData", menuName = "Player Data/Stats")]
public class PlayerData : ScriptableObject
{
    public float experience;
    public float totalExperience;
    public int level;
    public int availableSkillPoints;
    public int totalSkillPoints;

    // these might not be stats that we want to save across states
    // but rather we have base values that are altered by things such as equipment, buffs and passive skills
    // this way we can always reset the player to his base stats and calculate the final stats from there
    public float health;
    public float damage;
    public float attackSpeed;
    public float speed;

    public void resetPlayerData()
    {
        experience = 0;
        totalExperience = 0;
        level = 0;
        availableSkillPoints = 0;
        totalSkillPoints = 0;

        health = 100;
        damage = 10;
        attackSpeed = 2;
        speed = 1;
    }
}
