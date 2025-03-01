using UnityEngine;

public class AreaVisual : MonoBehaviour
{
    /// <summary>
    /// This class handles circular area effects that are spawned when a skill is activated
    /// </summary>

    public float duration; // how long it takes for ice nova object to disappear
    public float radius; // radius of the area effect
    private float elapsedTime = 0f;

    private Animator animator;
    private float originalClipLength;

    protected void Start()
    {
        // get base radius of the area effect through its circle collider
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        float baseRadius = circleCollider.radius;

        float scale = radius / baseRadius;

        transform.localScale = new Vector3(scale, scale, 1);
        elapsedTime = 0f;
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            // Assuming the animator has one animation state
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Retrieve the length of the animation clip
            AnimationClip clip = animator.runtimeAnimatorController.animationClips[0];
            originalClipLength = clip.length;

            // Scale the animation playback speed to match the animationDuration
            animator.speed = originalClipLength / duration;

            // Start the animation
            animator.Play(stateInfo.fullPathHash, -1, 0f);

            // Schedule to stop after the animationDuration
            Invoke(nameof(DisableAnimation), duration);
        }
        
    }
    protected void DisableAnimation()
    {
        animator.enabled = false; // Disable the Animator component to stop rendering the animation
    }
    protected void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
