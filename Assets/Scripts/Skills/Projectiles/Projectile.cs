using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    private Creature user;
    private float speed;
    private float duration;
    private float tickRate;
    private float[] damage;
    private Dictionary<Creature, float> lastHitTime;
    private Vector2 direction;

    private int enemyLayer;
    private int terrainLayer;
    private int playerLayer;
    private Skill skill;

    protected bool destroyOnHit = true;
    
    
    protected virtual void Awake()
    {
        lastHitTime = new Dictionary<Creature, float>();
    }

    public void Initialize(Vector2 direction, float speed, float duration, float tickRate, float[] damage, int enemyLayer, int terrainLayer, int playerLayer, Skill skillThatFiredProjectile)
    {
        this.direction = direction;
        this.speed = speed;
        this.duration = duration;
        this.tickRate = tickRate;
        this.damage = damage;
        this.enemyLayer = enemyLayer;
        this.terrainLayer = terrainLayer;
        this.playerLayer = playerLayer;
        this.skill = skillThatFiredProjectile;
        this.user = skill.user;
        StartCoroutine(MoveAndHandleCollisions());
    }

    private IEnumerator MoveAndHandleCollisions()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;

            Collider2D[] hitColliders = GetHitColliders();
            List<Creature> targetsList = new List<Creature>();

            foreach (var hitCollider in hitColliders)
            {
                Creature creature = hitCollider.GetComponent<Creature>();
                if (creature != null)
                {
                    bool isEnemyTarget = (hitCollider.gameObject.layer == enemyLayer && user is Player);
                    bool isPlayerTarget = (hitCollider.gameObject.layer == playerLayer && user is Enemy);
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

                                if (destroyOnHit)
                                {

                                    skill.ApplyDamageAndEffects(targetsList);
                                    Destroy(gameObject);
                                    yield break; // Exit the coroutine as the projectile is destroyed
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
    private Collider2D[] GetHitColliders()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider is CircleCollider2D circleCollider)
        {
            return Physics2D.OverlapCircleAll(transform.position, circleCollider.radius);
        }
        else if (collider is BoxCollider2D boxCollider)
        {
            return Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0f);
        }
        else
        {
            Debug.LogWarning("Unsupported collider type on projectile.");
            return new Collider2D[0];
        }
    }
    private bool IsTargetOnScreen(Creature target)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}