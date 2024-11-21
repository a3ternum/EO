using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "playerStats", menuName = "PlayerStats/Data")]
public class PlayerStats : CreatureStats
{
    public float experience;
    public float totalExperience;
    public int level;
    public int availableSkillPoints;
    public int totalSkillPoints;

    public float manaBase;
    public float manaFlat;
    public float manaIncreases; // percentage mana increases
    public float manaMoreMultipliers; // percentage mana more multipliers

    public float manaRegenBase;
    public float manaRegenFlat;
    public float manaRegenIncreases;
    public float manaRegenMoreMultipliers;

    public int intelligence;
    public int strength;
    public int dexterity;

    public float intelligenceIncreases;
    public float strengthIncreases;
    public float dexterityIncreases;

    public float intelligenceMoreMultipliers;
    public float strengthMoreMultipliers;
    public float dexterityMoreMultipliers;

  


    // remainder of the players stats are inherited from CreatureStats



    public void resetPlayerData() // reset player to base stats
    {
        base.resetCreatureData();
        experience = 0;
        totalExperience = 0;
        level = 0;
        availableSkillPoints = 0;
        totalSkillPoints = 0;

        manaBase = 100;
        manaFlat = 0;
        manaIncreases = 0;
        manaMoreMultipliers = 0;

        manaRegenBase = 5;
        manaRegenFlat = 0;
        manaRegenIncreases = 0;
        manaRegenMoreMultipliers = 0;

        intelligence = 10;
        strength = 10;
        dexterity = 10;
        intelligenceIncreases = 0;
        strengthIncreases = 0;
        dexterityIncreases = 0;

        manaBase = 200;
        manaFlat = 0;
        manaIncreases = 0;
        manaMoreMultipliers = 0;

        movementSpeedBase = 5;
    }

    
}
