using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MeleeAttackArea : MeleeAttack
{
 
    protected virtual void DamageCreaturesInArea(Vector2 Position)
    {
        List<Creature> creaturesHit = AoECollider(Position);
        ApplyDamageAndEffects(creaturesHit);
    }

    protected virtual List<Creature> AoECollider(Vector2 mousePosition)
    {
        float tempRadius = radius * (1 + areaOfAttackIncrease);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(user.transform.position, tempRadius);
        List<Creature> targetsList = new List<Creature>();


        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.layer == enemyLayer && user is Player)
            {
                if (collider.TryGetComponent(out Creature enemy))
                {
                    targetsList.Add(enemy);
                }
            }
            else if (collider.gameObject.layer == playerLayer && user is Enemy)
            {
                if (collider.TryGetComponent(out Creature player))
                {
                    targetsList.Add(player);
                }
            }
        }
        
        return targetsList;
    }

    public override float[] CalculateDamage()
    {
        // its important to calculate the final damage combining both the skills attributes and the creatures attributes 
        // inside one method because they should interact multiplicatively
        // later we will add support gems that add more multipliers to the damage calculation

        float[] finalDamage = new float[5];
        // we need an array of 5 ones using 


        float[] baseDamage = ElementWiseMultiply(ElementWiseAdd(damage, user.creatureStats.damageFlat), ElementWiseAdd(Enumerable.Repeat(1.0f, 5).ToArray(), user.creatureStats.damageIncreases));
        finalDamage = ElementWiseMultiply(baseDamage, ElementWiseAdd(Enumerable.Repeat(1.0f, 5).ToArray(), user.creatureStats.damageMoreMultipliers));

        // area skill so incorporate users area damage increase
        finalDamage = ElementWiseMultiply(finalDamage, ElementWiseAdd(Enumerable.Repeat(1.0f, 5).ToArray(), Enumerable.Repeat(user.creatureStats.areaOfEffectDamageIncreases, 5).ToArray()));

        // this method will be used to calculate the damage of the skill
        // this method will become far more complex as we add more mechanics and add the players stats
        // note that final damage calculation is computed in the creature that receives the damage.
        return finalDamage;
    }

    // optional method for AoE skills that require a target to be selected



}
