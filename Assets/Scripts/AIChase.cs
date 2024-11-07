using UnityEngine;
using UnityEngine.AI;

public class AIChase : MonoBehaviour
{
    enum State
    {
        CHASE,
        IDLE,
        KEEP_DISTANCE,
        FLEE
    }

    public Player player;
    public float speed;

    private float distance;

    private NavMeshAgent agent;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
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


    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        agent.transform.position = new Vector3(currentPosition.x, currentPosition.y, 0);

        transform.rotation = Quaternion.identity;

        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }

}
