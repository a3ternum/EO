using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    public NavMeshAgent agent; // Reference to the NavMeshAgent
    public Animator animator;  // Reference to the Animator

    private int walkingHash;
    private int walkRightHash;
    private int walkUpHash;
    private int walkLeftHash;
    private int walkDownHash;

    private void Start()
    {
        // Get the NavMeshAgent and Animator components
        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponentInParent<Animator>();

        // Cache the hash IDs for the animation states
        walkingHash = Animator.StringToHash("Walking");
        walkRightHash = Animator.StringToHash("WalkRight");
        walkUpHash = Animator.StringToHash("WalkUp");
        walkLeftHash = Animator.StringToHash("WalkLeft");
        walkDownHash = Animator.StringToHash("WalkDown");
    }


    void Update()
    {
        Vector3 velocity = agent.velocity; // Get the agent's velocity
        if (animator == null) return;

        if (velocity.magnitude > 0.1f) // If moving
        {
            // Determine direction
            float angle = Mathf.Atan2(velocity.z, velocity.x) * Mathf.Rad2Deg;

            // Call a function to set animations based on angle
            UpdateAnimation(angle);
        }
        else
        {
            // Stop animations when not moving
            animator.SetBool("isWalking", false);
        }
    }

    void UpdateAnimation(float angle)
    {
        animator.SetBool("isWalking", true);

        if (animator.HasState(0, walkingHash))
        {
            animator.Play(walkingHash);
        }
        else
        {
            if (angle >= -45f && angle <= 45f) // Right
            {
                if (animator.HasState(0, walkRightHash))
                {
                    animator.Play(walkRightHash);
                }
                else
                {
                    Debug.LogWarning("Animator does not have a 'WalkRight' state.");
                }
            }
            else if (angle > 45f && angle < 135f) // Up
            {
                if (animator.HasState(0, walkUpHash))
                {
                    animator.Play(walkUpHash);
                }
                else
                {
                    Debug.LogWarning("Animator does not have a 'WalkUp' state.");
                }
            }
            else if (angle >= 135f || angle <= -135f) // Left
            {
                if (animator.HasState(0, walkLeftHash))
                {
                    animator.Play(walkLeftHash);
                }
                else
                {
                    Debug.LogWarning("Animator does not have a 'WalkLeft' state.");
                }
            }
            else if (angle < -45f && angle > -135f) // Down
            {
                if (animator.HasState(0, walkDownHash))
                {
                    animator.Play(walkDownHash);
                }
                else
                {
                    Debug.LogWarning("Animator does not have a 'WalkDown' state.");
                }
            }
        }
    }
}