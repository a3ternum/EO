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


    public float armourBase; // flat armour value
    public float evasionBase; // flat evasion value

    public float armourIncreases; // array to hold 'increased armour' multipliers
    public float evasionIncreases; // array to hold 'increased evasion' multipliers
    public float physicalDamageReduction; // percentage physical damage reduction.
                                          // To be applied after physical damage reduction from armour is calculated

    public float[] resistances; // array to hold  (fire, cold, lightning, chaos)
    public float[] maximumResistances;

    public float healthBase;
    public float healthFlatIncreases;
    public float healthIncreases; // percentage health increases
    public float healthMoreMultipliers; // percentage health more multipliers

    public float manaBase;
    public float manaFlatIncreases;
    public float manaIncreases; // percentage mana increases
    public float manaMoreMultipliers; // percentage mana more multipliers

    public float[] damageFlat = new float[5]; // array to hold flat damage increases
    public float[] damageIncreases; // array to hold 'increased damage' multipliers
    public float[] damageMoreMultipliers; // array to hold 'more damage' multipliers

    public float accuracyBase;
    public float accuracyFlat;
    public float accuracyIncreases;
    public float accuracyMoreMultipliers;

    public float attackSpeedBase;
    public float attackSpeedIncreases;
    public float attackSpeedMoreMultipliers;

    public float castSpeedBase;
    public float castSpeedIncreases;
    public float castSpeedMoreMultipliers;

    public float criticalStrikeChanceBase;
    public float criticalStrikeChanceIncreases;
    public float criticalStrikeMultiplier;


    public float baseMovementSpeed;

    public int additionalProjectiles;
    public float areaOfEffectIncreases;
    public float projectileSpeedIncreases;
    public float movementSpeedIncreases;


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
        armourBase = 0;
        evasionBase = 0;

        armourIncreases = 1f;
        evasionIncreases = 1f;

        resistances = new float[4];
        maximumResistances = new float[4];
        physicalDamageReduction = 0;

        healthBase = 100;
        healthFlatIncreases = 0;
        healthIncreases = 1;
        healthMoreMultipliers = 1;

        manaBase = 100;
        manaFlatIncreases = 0;
        manaFlatIncreases = 1;
        manaMoreMultipliers = 1;

        damageFlat = new float[5];
        damageIncreases = Enumerable.Repeat(1f, 5).ToArray(); ;
        damageMoreMultipliers = Enumerable.Repeat(1f, 5).ToArray(); ;

        accuracyBase = 50;
        accuracyFlat = 0;
        accuracyIncreases = 1;
        accuracyMoreMultipliers = 1;

        attackSpeedBase = 1;
        attackSpeedIncreases = 1;
        attackSpeedMoreMultipliers = 1;

        castSpeedBase = 1;
        castSpeedIncreases = 1;
        castSpeedMoreMultipliers = 1;

        criticalStrikeChancebase = 0.05f;
        criticalStrikeChanceIncreases = 1;
        criticalStrikeMultiplier = 1;

        movementSpeedBase = 1;

        additionalProjectiles = 0;
        areaOfEffectIncreases = 1;
        projectileSpeedIncreases = 1;
        movementSpeedIncreases = 1;
}
}
