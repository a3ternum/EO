using UnityEngine;

public class AnimatorStartupFix : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
        Invoke("EnableAnimator", 1f);  // Wait 1 seconds before enabling the animator
    }

    void EnableAnimator()
    {
        animator.enabled = true;
    }
}