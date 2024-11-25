using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    public NavMeshAgent agent; // Reference to the NavMeshAgent
    public Animator animator;  // Reference to the Animator

    void Update()
    {
        Vector3 velocity = agent.velocity; // Get the agent's velocity
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

        if (angle >= -45f && angle <= 45f) // Right
        {
            animator.Play("WalkRight");
        }
        else if (angle > 45f && angle < 135f) // Up
        {
            animator.Play("WalkUp");
        }
        else if (angle >= 135f || angle <= -135f) // Left
        {
            animator.Play("WalkLeft");
        }
        else if (angle < -45f && angle > -135f) // Down
        {
            animator.Play("WalkDown");
        }
    }
}