using UnityEngine;

public class CircleAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material circleMaterial; // Assign the material in the Inspector
    public float duration = 2.0f; // Time to close the circle
    private float timeElapsed = 0f;   // Time elapsed during animation


    private float timer = 0.0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            circleMaterial = spriteRenderer.material;
        }
        else
        {
            Debug.LogError("SpriteRenderer is null");
        }
    }

    void Update()
    {
        // Update the elapsed time
        if (circleMaterial != null && timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(timeElapsed / duration);
            circleMaterial.SetFloat("_Radius", 1 - progress);
        }
        if (timeElapsed >= duration)
        {
            Destroy(gameObject);
        }
    }
}

