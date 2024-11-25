using UnityEngine;
[CreateAssetMenu(fileName = "Golem3Stats", menuName = "CreatureStats/Golem3Stats")]

public class Golem3Stats : CreatureStats
{
    public override void ResetCreatureData()
    {
        armourFlat = 100;
        evasionFlat = 0;

        armourIncreases = 0;
        evasionIncreases = 0;

        evadeChanceBase = 0;
        evadeChanceFlat = 0;
        evadeChanceIncreases = 0;
        blockChanceBase = 0;
        blockChanceFlat = 0;
        blockChanceIncreases = 0;

        resistances = new float[] { 35, 0, 0, 0 };
        maximumResistances = new float[] { 75, 75, 75, 75 };
        physicalDamageReduction = 0;

        healthBase = 200;
        healthFlat = 0;
        healthIncreases = 0;
        healthMoreMultipliers = 0;

        healthRegenBase = 0;
        healthRegenFlat = 0;
        healthRegenIncreases = 0;
        healthRegenMoreMultipliers = 0;

        damageFlat = new float[] { 20, 0, 0, 0, 0 };
        damageIncreases = new float[5];
        damageMoreMultipliers = new float[5];

        accuracyBase = 50;
        accuracyFlat = 0;
        accuracyIncreases = 0;
        accuracyMoreMultipliers = 0;

        attackSpeedBase = 0.7f;
        attackSpeedFlat = 0;
        attackSpeedIncreases = 0;
        attackSpeedMoreMultipliers = 0;

        castSpeedBase = 1;
        castSpeedFlat = 0;
        castSpeedIncreases = 0;
        castSpeedMoreMultipliers = 0;

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

        movementSpeedBase = 4;
        movementSpeedIncreases = 0;

        additionalProjectiles = 0;
        projectileSpeedIncreases = 0;

        projectileDamageIncreases = 0;

        areaOfEffectIncreases = 0.5f;
        areaOfEffectDamageIncreases = 0;

        strikeRangeFlat = 2;
    }
}
