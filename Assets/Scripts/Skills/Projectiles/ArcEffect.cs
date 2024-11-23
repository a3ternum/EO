using System.Runtime.CompilerServices;
using UnityEngine;

public class ArcEffect : MonoBehaviour
{
    public Vector2 startLocation;
    public Vector2 targetLocation;
    private float duration = 0.3f; // duration of the arc
    private float elapsedTime = 0f; // time elapsed since arc was created
    private SpriteRenderer spriteRenderer; // sprite renderer for the arc


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        Debug.Log("Distance: " + distance);
        // Set the position of the ArcEffect to the midpoint between startLocation and targetLocation
        transform.position = (Vector2)(startLocation + targetLocation) / 2;
        Debug.Log("Position: " + transform.position);
        // Rotate the ArcEffect to face the targetLocation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log("Rotation: " + transform.rotation);
        // Scale the width of the ArcEffect to match the distance
        transform.localScale = new Vector3(distance, transform.localScale.y, transform.localScale.z);
        Debug.Log("Scale: " + transform.localScale);
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
