using UnityEngine;
using UnityEngine.UI;
public class SingleTargetIndicatorCircle : MonoBehaviour
{
    public float animationDuration = 2f; // Duration to shrink
    public float initialScale; // Initial radius (1)
    private float timer; // Timer to track animation progress


    void Start()
    {
        // Set initial radius (assuming circle starts at size 1)
        initialScale = 0.2f;

    }

    void Update()
    {
        // If timer is less than the animation duration, shrink the circle
        if (timer < animationDuration)
        {
            timer += Time.deltaTime; // Increase the timer by the frame time

            // Calculate the new radius based on time
            float scale = Mathf.Lerp(initialScale, 0, timer / animationDuration);

            // rescale the circle based on the new radius
            transform.localScale = new Vector3(scale, scale, 1);
        }
        else
        {
            DestroyCircle(); // Destroy the circle once the animation is complete
        }
    }

    // Call this function to start the shrinking animation
    public void StartShrinking()
    {
        timer = 0f; // Reset timer
        gameObject.SetActive(true); // Ensure the circle is visible
    }

    // Call this to hide the circle once the animation is complete
    public void HideCircle() // can be used to 
    {
        gameObject.SetActive(false);
    }

    public void DestroyCircle()
    {
        Destroy(gameObject);
    }

}