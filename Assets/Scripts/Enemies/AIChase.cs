using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIChase : MonoBehaviour
{
    public Player player;

    private float currentChaseRange;
    private float distance;
    private bool inChase;

    private Enemy thisEnemy;
    private NavMeshAgent agent;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        thisEnemy = GetComponent<Enemy>();
      
        
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component missing from this game object!");
            Destroy(this); return;
        }
        // Optional: Set agent properties for 2D navigation
        agent.updateRotation = false;  // Avoid rotating on the Z-axis
        agent.updateUpAxis = false;    // Allow 2D top-down navigation


        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing from this game object!");
        }
    }

    public void setSpeed(float newSpeed)
    {
        if (agent != null)
        {
            agent.speed = newSpeed;
        }
    }
    
    public void setPlayer(Player newPlayer)
    {
        player = newPlayer;
    }

    private void DetermineEnemyState()
    {
        if (!inChase)
        {
            currentChaseRange = thisEnemy.chaseRange;
            thisEnemy.AIState = Enemy.State.IDLE; // maybe redundant
            
            if (thisEnemy.damagedRecently) // If enemy takes damage, it will chase the player
            {
                currentChaseRange = thisEnemy.chaseRangeIfDamagedRecently;
                thisEnemy.AIState = Enemy.State.CHASE;
                inChase = true; // here the enemy switches to chase mode
                AggroPack();
            }
            else if (distance <= currentChaseRange && distance >= thisEnemy.chaseDistance)
            {
                thisEnemy.AIState = Enemy.State.CHASE;
                inChase = true; //  here the enemy switches to chase mode
                AggroPack();
            }

            else if (distance > currentChaseRange || player == null)
            {
                thisEnemy.AIState = Enemy.State.IDLE;
                inChase = false; // here the enemy stays in idle mode
            }
            else if (distance < thisEnemy.minimumDistance)
            {
                thisEnemy.AIState = Enemy.State.FLEE;
            }
            else
            {
                thisEnemy.AIState = Enemy.State.KEEP_DISTANCE;
            }

        }
        else if (inChase)
        {
            currentChaseRange = thisEnemy.chaseRangeIfDamagedRecently;
            StartCoroutine(ResetInChase());
            
            if (thisEnemy.damagedRecently) // If enemy takes damage, it will chase the player
            {
                currentChaseRange = thisEnemy.chaseRangeIfDamagedRecently; // maybe redundant
                thisEnemy.AIState = Enemy.State.CHASE; // maybe redundant
                inChase = true; // here the enemy stays in chase mode
            }
            else if (distance <= currentChaseRange && distance >= thisEnemy.chaseDistance)
            {
                thisEnemy.AIState = Enemy.State.CHASE; // maybe redundant
                inChase = true; //  here the enemy stays in chase mode
            }

            else if (distance > currentChaseRange || player == null)
            {
                thisEnemy.AIState = Enemy.State.IDLE;
                inChase = false; // here the enemy switches back to idle mode
            }
            else if (distance < thisEnemy.minimumDistance)
            {
                thisEnemy.AIState = Enemy.State.FLEE;
            }
            else
            {
                thisEnemy.AIState = Enemy.State.KEEP_DISTANCE;
            }
        }
    }

    private void AggroPack() // The enemy will aggro all surrounding enemies based on the packAggroRadius
    {
        // Find all enemies in the packAggroRadius
        // spawn circle collider
        // check for enemies in the circle collider
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, thisEnemy.packAggroRadius);
        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.gameObject.GetComponent<Enemy>();
            if (enemy != null && enemy != thisEnemy)
            {
                hitCollider.gameObject.GetComponent<Enemy>().AIState = Enemy.State.CHASE;
                hitCollider.gameObject.GetComponent<AIChase>().inChase = true;
            }
        }
    }


    private IEnumerator ResetInChase(float time = 4)
    {
        yield return new WaitForSeconds(time);
        inChase = false;
    }

    // Update is called once per frame
    void Update()
    {
        setSpeed(thisEnemy.speed); // set speed of the agent (mob)


        Vector3 currentPosition = transform.position;
        agent.transform.position = new Vector3(currentPosition.x, currentPosition.y, 0);

        Quaternion rotation = transform.rotation;
        rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        transform.rotation = rotation;

        if (player != null)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
        }
        DetermineEnemyState();

        if (player != null && thisEnemy.AIState == Enemy.State.CHASE) // mob chases enemy.
        {
            agent.SetDestination(player.transform.position);
        }

        if (player != null && thisEnemy.AIState == Enemy.State.IDLE) // mob stops moving, include script to make it wander around itself
        {
            agent.SetDestination(transform.position); 
        }

        if (player != null && thisEnemy.AIState == Enemy.State.FLEE) // make mob run away from player
        {
            agent.SetDestination(-player.transform.position); 
        }

        if (player != null && thisEnemy.AIState == Enemy.State.KEEP_DISTANCE) 
            // mob stands his ground and keeps attacking. 
        {   
                agent.SetDestination(transform.position);   
        }
        
    }

}
