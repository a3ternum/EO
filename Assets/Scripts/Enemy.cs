using System;
using UnityEngine;

public class Enemy : Creature
{
    public enum State
    {
        CHASE,
        IDLE,
        KEEP_DISTANCE,
        FLEE
    }
    public State AIState;

    public float weaponDistance = 0.5f;
    public EnemyProjectile projectile;
    public Transform firepoint;

    
    
    public float attackRange = 10f; // attack range of the enemy
    public float chaseDistance = 8f; // enemy will chase until this distance is reached.
    public float minimumDistance = 5f; // enemy will keep this distance from the player
    public float chaseRange = 15f; // chase range of the enemy (from where the enemy will start chasing the player)
    public float timeInChase = 0f; // how long the enemy has been chasing the player in seconds
    public bool hasLOS; // has line of sight to the player

    protected AIChase chaseScript;
    protected Player player;
    protected float experienceValue = 100f;

    protected Boolean inAttackRange = false;

    [SerializeField]
    private bool isRangedMob = true;




    // I will use an enumeration to represent different chase states
    // Each of these states will have a different behavior
    // Chase, Flee, Patrol, Circle, etc


    protected override void Start()
    {
        

        AIState = State.IDLE;
        base.Start();
        player = FindFirstObjectByType<Player>();
        chaseScript = gameObject.AddComponent<AIChase>();
        chaseScript.setPlayer(FindFirstObjectByType<Player>());


        // place firepoint to the right of the character
        firepoint.position = this.transform.position + Vector3.right * weaponDistance;
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
       
        // get direction vector from the enemy to the player's position
        Vector3 direction = (player.transform.position - firepoint.position).normalized;

        // calculate the angle between the enemy and the player's position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // change firepoint position
        firepoint.position = this.transform.position + direction * weaponDistance;

        // rotate the firepoint to face character
        firepoint.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (isRangedMob)
        {
            EnemyProjectile enemyProjectile = Instantiate(projectile, firepoint.position, firepoint.rotation);
            enemyProjectile.setParentEnemy(this);
        }
        else // mob is melee
        {
            // use melee attack;
        }
    }

    public void setState(State state)
    {
        AIState = state;
    }

    protected void checkAttackRange()
    {

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            inAttackRange = true;
        }
        else
        {
            inAttackRange = false;
        }

    }



    protected override void Update()
    {
        base.Update();
        if (player != null)
        {
            checkAttackRange();

            if (Time.time >= nextAttackTime && inAttackRange)
            {
                enemyAttack();
                nextAttackTime = Time.time + 1f / attackSpeed;
            }
        }
    }
}
