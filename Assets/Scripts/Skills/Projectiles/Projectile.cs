using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    protected Animator animator;

    protected float projectileSpeed;
    protected float radius;
    protected float duration;
    protected float tickRate;
    protected int pierceCount;


    protected float[] damage;
    protected Dictionary<Creature, float> lastHitTime;
    protected Vector2 direction;

    protected int enemyLayer;
    protected int terrainLayer;
    protected int playerLayer;
    protected Skill skill;

    protected float AoEIncrease;
    protected bool destroyOnHit = true;
    
    
    protected virtual void Awake()
    {
        lastHitTime = new Dictionary<Creature, float>();
    }

    public virtual void Initialize(Vector2 direction, Skill skillThatFiredProjectile)
    {
        this.skill = skillThatFiredProjectile;

        this.direction = direction;
        this.projectileSpeed = skill.projectileSpeed;
        this.AoEIncrease = skill.user.creatureStats.areaOfEffectIncreases;
        this.radius = skill.radius * (1 + AoEIncrease);
        this.duration = skill.duration;
        this.tickRate = skill.tickRate;
        this.pierceCount = skill.pierceCount;
        this.damage = skill.damage;
        this.enemyLayer = skill.enemyLayer;
        this.terrainLayer = skill.terrainLayer;
        this.playerLayer = skill.playerLayer;

        if (skill.tags.Contains("Area"))
        {
            // update scale of projectile object based on radius
            Collider2D collider = GetComponent<Collider2D>();
            float baseRadius = 1;
            float baseWidth = 0;
            float baseHeight = 0;
            float scale = 1;
            if (collider is CircleCollider2D circleCollider)
            {
                baseRadius = circleCollider.radius;
                circleCollider.radius = radius;
                scale = radius / baseRadius;
                transform.localScale = new Vector3(scale, scale, 1);
            }
            else if (collider is BoxCollider2D boxCollider)
            {
                boxCollider.size = new Vector2(boxCollider.size.x * radius, boxCollider.size.y * radius);

                baseWidth = boxCollider.size.x;
                baseHeight = boxCollider.size.y;

                transform.localScale = new Vector3(baseWidth, baseHeight, 1);
            }



        }
        StartCoroutine(MoveAndHandleCollisions());
    }

    protected virtual IEnumerator MoveAndHandleCollisions()
    {
        int currentPierceCount = pierceCount;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position += (Vector3)direction.normalized * projectileSpeed * Time.deltaTime;

            Collider2D[] hitColliders = GetHitColliders();
            List<Creature> targetsList = new List<Creature>();

            foreach (var hitCollider in hitColliders)
            {
                Creature creature = hitCollider.GetComponent<Creature>();
                if (creature != null)
                {
                    bool isEnemyTarget = (hitCollider.gameObject.layer == enemyLayer && skill.user is Player);
                    bool isPlayerTarget = (hitCollider.gameObject.layer == playerLayer && skill.user is Enemy);
                    //Debug.Log(isEnemyTarget || isPlayerTarget);
                    if (isEnemyTarget || isPlayerTarget)
                    {
                        if (!lastHitTime.ContainsKey(creature) || Time.time - lastHitTime[creature] >= tickRate)
                        {
                            // make sure target is on screen before adding it to the list
                            if (IsTargetOnScreen(creature))
                            {
                                targetsList.Add(creature);
                                lastHitTime[creature] = Time.time;

                                if (destroyOnHit && currentPierceCount <= 0)
                                {
                                    skill.ApplyDamageAndEffects(targetsList);
                                    Destroy(gameObject);
                                    yield break; // Exit the coroutine as the projectile is destroyed
                                }
                                else if (destroyOnHit && currentPierceCount > 0)
                                {
                                    currentPierceCount--;
                                }
                            }
                        }
                    }
                }
                else if (hitCollider.gameObject.layer == terrainLayer)
                {
                    // Destroy the projectile if it hits terrain
                    Destroy(gameObject);
                    yield break; // Exit the coroutine as the projectile is destroyed
                }
            }

            skill.ApplyDamageAndEffects(targetsList);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    protected Collider2D[] GetHitColliders()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider is CircleCollider2D circleCollider)
        {
            return Physics2D.OverlapCircleAll(transform.position, circleCollider.radius * (1 + skill.user.creatureStats.areaOfEffectIncreases));
        }
        else if (collider is BoxCollider2D boxCollider)
        {
            return Physics2D.OverlapBoxAll(transform.position, boxCollider.size * (1 + skill.user.creatureStats.areaOfEffectIncreases), 0f);
        }
        else
        {
            Debug.LogWarning("Unsupported collider type on projectile.");
            return new Collider2D[0];
        }
    }
    protected bool IsTargetOnScreen(Creature target)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}