using UnityEngine;

public class IceNovaObject : MonoBehaviour
{
    // This class handles with the ice nova effect that is spawned when the Ice Nova skill is activated
    public float duration; // how long it takes for ice nova object to disappear
    public float radius; // radius of the ice nova effect
    private float elapsedTime = 0f;

    private Animator animator;
    private float originalClipLength;

    private void Start()
    {
        // base radius of skill is 1 so we need to scale it down to the desired radius
        transform.localScale = new Vector3(transform.localScale.x * radius, transform.localScale.y * radius, 1);
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
        void DisableAnimation()
        {
            animator.enabled = false; // Disable the Animator component to stop rendering the animation
        }
    }

    private void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            Debug.Log("destroying ice nova object");
            Destroy(gameObject);
        }
    }


}
