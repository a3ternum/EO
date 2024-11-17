using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
[CreateAssetMenu(fileName = "CreatureStats", menuName = "CreatureStats/Data")]
public class CreatureStats : ScriptableObject
{
    /// <summary>
    /// All increases are taken as a percentage increase to be used multiplicatively.
    /// More modifiers are also a percentage that are used multiplicatively with increased modifiers.
    /// </summary>


    public float armourFlat; // flat armour value
    public float evasionFlat; // flat evasion value

    public float armourIncreases; // array to hold 'increased armour' multipliers
    public float evasionIncreases; // array to hold 'increased evasion' multipliers
    public float physicalDamageReduction; // percentage physical damage reduction.
                                          // To be applied after physical damage reduction from armour is calculated

    public float evadeChanceBase;
    public float evadeChanceFlat;
    public float evadeChanceIncreases;
    public float blockChanceBase;
    public float blockChanceFlat;
    public float blockChanceIncreases;

    public float[] resistances; // array to hold  (fire, cold, lightning, chaos)
    public float[] maximumResistances;

    public float healthBase;
    public float healthFlat;
    public float healthIncreases; // percentage health increases
    public float healthMoreMultipliers; // percentage health more multipliers

    public float healthRegenBase; // base health regen
    public float healthRegenFlat; // additional flat health regen
    public float healthRegenIncreases; // percentage health regen increases
    public float healthRegenMoreMultipliers; // percentage health regen more multipliers




    public float[] damageFlat = new float[5]; // array to hold flat damage increases
    public float[] damageIncreases; // array to hold 'increased damage' multipliers
    public float[] damageMoreMultipliers; // array to hold 'more damage' multipliers

    public float accuracyBase;
    public float accuracyFlat;
    public float accuracyIncreases;
    public float accuracyMoreMultipliers;

    public float attackSpeedBase;
    public float attackSpeedFlat;
    public float attackSpeedIncreases;
    public float attackSpeedMoreMultipliers;

    public float castSpeedBase;
    public float castSpeedFlat;
    public float castSpeedIncreases;
    public float castSpeedMoreMultipliers;

    public float criticalStrikeChanceBase;
    public float criticalStrikeChanceFlat;
    public float criticalStrikeChanceIncreases;

    public float criticalStrikeMultiplierBase;
    public float criticalStrikeMultiplierFlat;
    public float criticalStrikeMultiplierIncreases;

    public float igniteChanceBase;
    public float igniteChanceFlat;

    public float chillChanceBase;
    public float chillChanceFlat;

    public float freezeChanceBase;
    public float freezeChanceFlat;

    public float shockChanceBase;
    public float shockChanceFlat;

    public float movementSpeedBase;
    public float movementSpeedIncreases;

    public int additionalProjectiles;
    public float projectileSpeedIncreases;

    public float projectileDamageIncreases;

    public float areaOfEffectIncreases;
    public float areaOfEffectDamageIncreases;


    public void Start()
    {
        if (damageFlat.Length == 0)
        {
            damageFlat = new float[5];
        }
        if (damageIncreases.Length == 0)
        {
            damageIncreases = Enumerable.Repeat(1f, 5).ToArray();
        }
        if (damageMoreMultipliers.Length == 0)
        {
            damageMoreMultipliers = Enumerable.Repeat(1f, 5).ToArray();
        }

        if (resistances.Length == 0)
        {
            resistances = new float[4];
        }
        if (maximumResistances.Length == 0)
        {
            maximumResistances = Enumerable.Repeat(75f, 5).ToArray();
        }
        

    }
    public void resetCreatureData() // reset player to base stats
    {
        armourFlat = 0;
        evasionFlat = 0;

        armourIncreases = 0;
        evasionIncreases = 0;

        evadeChanceBase = 0;
        evadeChanceFlat = 0;
        evadeChanceIncreases = 0;
        blockChanceBase = 0;
        blockChanceFlat = 0;
        blockChanceIncreases = 0;

        resistances = new float[4];
        maximumResistances = new float[4];
        physicalDamageReduction = 0;

        healthBase = 100;
        healthFlat = 0;
        healthIncreases = 0;
        healthMoreMultipliers = 0;

        healthRegenBase = 0;
        healthRegenFlat = 0;
        healthRegenIncreases = 0;
        healthRegenMoreMultipliers = 0;

        damageFlat = new float[5];
        damageIncreases = new float[5]; 
        damageMoreMultipliers = new float[5]; 

        accuracyBase = 50;
        accuracyFlat = 0;
        accuracyIncreases = 0;
        accuracyMoreMultipliers = 0;

        attackSpeedBase = 1;
        attackSpeedFlat = 0;
        attackSpeedIncreases = 0;
        attackSpeedMoreMultipliers = 0;

        castSpeedBase = 1;
        castSpeedFlat = 0;
        castSpeedIncreases = 1;
        castSpeedMoreMultipliers = 1;

        criticalStrikeChanceBase = 0.05f;
        criticalStrikeChanceFlat = 0;
        criticalStrikeChanceIncreases = 0;
        criticalStrikeMultiplierBase = 1;
        criticalStrikeMultiplierFlat = 0;
        criticalStrikeMultiplierIncreases = 0;

        igniteChanceBase = 0;
        igniteChanceFlat = 0;
        chillChanceBase = 0;
        chillChanceFlat = 0;
        freezeChanceBase = 0;
        freezeChanceFlat = 0;
        shockChanceBase = 0;
        shockChanceFlat = 0;

        movementSpeedBase = 1;
        movementSpeedIncreases = 0;

        additionalProjectiles = 0;
        projectileSpeedIncreases = 0;

        projectileDamageIncreases = 0;

        areaOfEffectIncreases = 0;
        areaOfEffectDamageIncreases = 0;
    }
}
