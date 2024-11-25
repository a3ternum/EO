using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

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

    private EnemyAnimator enemyAnimator;

    public float packAggroRadius = 5f;

    public float attackRange; // attack range of the enemy
    public float chaseDistance = 8f; // enemy will chase until this distance is reached.
    public float minimumDistance = 0f; // enemy will keep this distance from the player
    public float chaseRange = 12f; // chase range of the enemy (from where the enemy will start chasing the player)
    public float chaseRangeIfDamagedRecently = 30f; // chase range of the enemy if he is attacked (from where the enemy will start chasing the player)
    public float timeInChase = 0f; // how long the enemy has been chasing the player in seconds
    public bool hasLOS; // has line of sight to the player



    protected AIChase chaseScript;
    protected Player player;
    protected float experienceValue = 100f;

    protected Boolean inAttackRange = false;




    // I will use an enumeration to represent different chase states
    // Each of these states will have a different behavior
    // Chase, Flee, Patrol, Circle, etc

    private IEnumerator FindPlayerWithDelay()
    {
        yield return new WaitForEndOfFrame(); // Small delay to allow instantiation
        player = FindFirstObjectByType<Player>();
    }

    protected override void Start()
    {
     


        AIState = State.IDLE;
        base.Start();
        StartCoroutine(FindPlayerWithDelay());
        chaseScript = gameObject.AddComponent<AIChase>();
        chaseScript.setPlayer(FindFirstObjectByType<Player>());
        chaseScript.setSpeed(currentMovementSpeed);

        // create enemy animator component
        enemyAnimator = gameObject.AddComponent<EnemyAnimator>();
        enemyAnimator.agent = chaseScript.agent;
        enemyAnimator.animator = GetComponent<Animator>();

        attackRange = activeSkill.strikeRange + creatureStats.strikeRangeFlat;
    }

    protected override void Die()
    {
        base.Die();
        player.gainExperience(experienceValue);
    }

    protected override IEnumerator ResetDamagedRecently(float time)
    {
        yield return new WaitForSeconds(time);
        damagedRecently = false;

    }

    // rewrite completely to use skills instead of random attack.
    public virtual void enemyAttack()
    {
        if (activeSkill != null)
        {
            activeSkill.ActivateSkill();
        }
    }

    public void setState(State state)
    {
        AIState = state;
    }

    protected void checkAttackRange()
    {
        Debug.Log("attack range: " + attackRange);
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
                nextAttackTime = Time.time + 1f / currentAttackSpeed;
            }
        }
    }
}
