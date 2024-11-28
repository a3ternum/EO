using System.Runtime.CompilerServices;
using UnityEngine;

public class ArcEffect : MonoBehaviour
{
    private Animator animator;

    public Vector2 startLocation;
    public Vector2 targetLocation;
    private float duration = 0.3f; // duration of the arc
    private float elapsedTime = 0f; // time elapsed since arc was created
    private SpriteRenderer spriteRenderer; // sprite renderer for the arc


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on ArcEffect");
        }
        // change the scale of the arc based on the length
        // Calculate the direction and distance between startLocation and targetLocation
        Vector2 direction = targetLocation - startLocation;
        float distance = direction.magnitude;

        // Set the position of the ArcEffect to the midpoint between startLocation and targetLocation
        transform.position = (Vector2)(startLocation + targetLocation) / 2;

        // Rotate the ArcEffect to face the targetLocation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Scale the width of the ArcEffect to match the distance
        transform.localScale = new Vector3(distance, transform.localScale.y, transform.localScale.z);
    }

    public void Update()
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
