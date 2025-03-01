using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Player player;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool lookingRight = true;
    private Animator animator;
    private GameManager gameManager;  // Reference to GameManager

    public int terrainLayer; // Layer of the terrain


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        terrainLayer = LayerMask.NameToLayer("Terrain");
    }

    private void CheckRecall()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (gameManager != null && gameManager.isInMap)
            {
                gameManager.ReturnToHideout();
            }
        }    
    }

        public void setMovement()
    {
        if (!enabled || !player.canMove)
        {
            movement.x = 0; movement.y = 0;
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        // Normalize the vector to prevent faster diagonal movement
        movement.Normalize();

        // flip the sprite based on movement direction
        if (movement.x < 0 && lookingRight)
        {
            // flip the sprite to the left
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
            lookingRight = false;
        }
        if (movement.x > 0 && !lookingRight)
        {
            // flip the sprite to the right
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            lookingRight = true;
        }

        // update animation booleans based on player movement
        bool isRunning = movement.x != 0 || movement.y != 0; // Small threshold to avoid jittering
        animator.SetBool("isRunning", isRunning);
    }

    public void movePlayer()
    {
        Vector2 direction = new Vector2(movement.x, movement.y);
        

        Vector2 velocity = movement * player.currentMovementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + velocity);

    }




    public void Update()
    {
        
        setMovement();
        CheckRecall();
        if (!enabled)
        {
            return; // Do not update if the player movement compoenent is disabled (due to being in skillTree)
        }

        
    }
}
