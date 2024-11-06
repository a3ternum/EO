using System;
using UnityEngine;

public class Enemy : Creature
{
    public float weaponDistance = 0.5f;
    public GameObject projectile;
    public Transform firePoint;

    protected AIChase chaseScript;
    protected Player player;
    protected float experienceValue = 100f;

    protected Boolean inAttackRange = true;
    protected Transform playerTransform;
    
    
    

    // I will use an enumeration to represent different chase states
    // Each of these states will have a different behavior
    // Chase, Flee, Patrol, Circle, etc


    protected override void Start()
    {
        base.Start();
        player = FindFirstObjectByType<Player>();
        chaseScript = gameObject.AddComponent<AIChase>();
        chaseScript.setPlayer(FindFirstObjectByType<Player>());

        playerTransform = FindFirstObjectByType<Player>().transform;

        // place firepoint to the right of the character
        firePoint.position = this.transform.position + Vector3.right * weaponDistance;
    }

    public override void takeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;

            if (health <= 0)
            {
                die();
                player.gainExperience(experienceValue);
            }
        }
    }

    public virtual void enemyAttack()
    { 
            // rotate firePoint to face the player
            Vector3 playerPosition = playerTransform.position;

            // get direction vector from the enemy to the player's position
            Vector3 direction = (playerPosition - firePoint.position).normalized;

            // calculate the angle between the enemy and the player's position
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // change firepoint position
            firePoint.position = this.transform.position + direction * weaponDistance;

            // rotate the weapon to have pummel face character
            firePoint.transform.rotation = Quaternion.Euler(0, 0, angle);

            Instantiate(projectile, firePoint.position, firePoint.rotation);                 
    }

    protected override void Update()
    {
        base.Update();

        if (Time.time >= nextAttackTime && inAttackRange)
        {
            enemyAttack();
            nextAttackTime = Time.time + 1f / attackSpeed;
        }
        
    }

    protected virtual void checkAttackRange()
    {
        // check if player is in attack range
        if (Vector3.Distance(transform.position, playerTransform.position) < 30f)
        {
            inAttackRange = true;
        }
        else
        {
            inAttackRange = false;
        }
    }

}
