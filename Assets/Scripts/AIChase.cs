using UnityEngine;
using UnityEngine.AI;

public class AIChase : MonoBehaviour
{
    public Player player;
    public float speed;

    private float distance;

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
        speed = newSpeed;
    }
    
    public void setPlayer(Player newPlayer)
    {
        player = newPlayer;
    }

    private void DetermineEnemyState()
    {

        if (distance <= thisEnemy.chaseRange && distance >= thisEnemy.chaseDistance)
        {
            thisEnemy.AIState = Enemy.State.CHASE;
        }
        else if (distance > thisEnemy.chaseRange)
        {
            thisEnemy.AIState = Enemy.State.IDLE;
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


    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        agent.transform.position = new Vector3(currentPosition.x, currentPosition.y, 0);

        Quaternion rotation = transform.rotation;
        rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        transform.rotation = rotation;

        distance = Vector3.Distance(player.transform.position, transform.position);

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
