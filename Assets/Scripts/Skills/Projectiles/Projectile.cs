using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float duration;
    public float tickRate;
    public float damage;
    private Dictionary<Enemy, float> lastHitTime;
    private Vector2 direction;

    private int enemyLayer;
    private int terrainLayer;
    private int playerLayer;

    private void Awake()
    {
        lastHitTime = new Dictionary<Enemy, float>();
    }

    public void Initialize(Vector2 direction, float speed, float duration, float tickRate, float damage, int enemyLayer, int terrainLayer, int playerLayer)
    {
        this.direction = direction;
        this.speed = speed;
        this.duration = duration;
        this.tickRate = tickRate;
        this.damage = damage;
        this.enemyLayer = enemyLayer;
        this.terrainLayer = terrainLayer;
        this.playerLayer = playerLayer;
        StartCoroutine(MoveAndHandleCollisions());
    }

    private IEnumerator MoveAndHandleCollisions()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
            List<Enemy> targetsList = new List<Enemy>();

            foreach (var hitCollider in hitColliders)
            {
               
                if (hitCollider.gameObject.layer == enemyLayer)
                {
                    Enemy enemy = hitCollider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        if (!lastHitTime.ContainsKey(enemy) || Time.time - lastHitTime[enemy] >= tickRate)
                        {
                            targetsList.Add(enemy);
                            lastHitTime[enemy] = Time.time;
                        }
                    }
                }
                else if (hitCollider.gameObject.layer == terrainLayer)
                {
                    // Destroy the projectile if it hits terrain
                    Destroy(gameObject);
                    yield return null;
                }
                else if (hitCollider.gameObject.layer == playerLayer)
                {
                    // Do nothing
                }
            }

            ApplyDamageAndEffects(targetsList);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void ApplyDamageAndEffects(List<Enemy> targets)
    {
        if (targets != null && targets.Count > 0)
        {
            foreach (var target in targets)
            {
                target.TakeDamage(damage);
                // Trigger hit effect if needed
            }
        }
    }
}