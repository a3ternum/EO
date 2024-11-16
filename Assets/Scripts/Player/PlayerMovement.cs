using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerTransform;
    public float movespeed = 5f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool lookingRight = true;
    private Animator animator;
    private GameManager gameManager;  // Reference to GameManager

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
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
        if (!enabled)
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
            playerTransform.rotation = Quaternion.Euler(0, 180, 0);
            lookingRight = false;
        }
        if (movement.x > 0 && !lookingRight)
        {
            // flip the sprite to the right
            playerTransform.rotation = Quaternion.Euler(0, 0, 0);
            lookingRight = true;
        }

        // update animation booleans based on player movement
        bool isRunning = movement.x != 0 || movement.y != 0; // Small threshold to avoid jittering
        animator.SetBool("isRunning", isRunning);
    }

    public void movePlayer()
    {
        //  move the character using rigidbody
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
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
