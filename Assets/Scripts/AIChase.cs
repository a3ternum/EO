using UnityEngine;

public class AIChase : MonoBehaviour
{
    public Player player;
    public float speed;

    private float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
