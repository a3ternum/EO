using UnityEngine;

public class IceNovaObject : MonoBehaviour
{
    // This class handles with the ice nova effect that is spawned when the Ice Nova skill is activated
    public float duration = 1f; // how long it takes for ice nova object to disappear
    public float radius = 0.5f; // radius of the ice nova effect
    private float elapsedTime = 0f;

    private void Start()
    {
        // base radius of skill is 1 so we need to scale it down to the desired radius
        transform.localScale = new Vector3(radius, radius, 1);
        elapsedTime = 0f;
    }

    private void Update()
    {
        Debug.Log("elapsed time is " + elapsedTime + " while duration is " + duration);
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            Debug.Log("Ice Nova Object Destroyed");
            Destroy(gameObject);
        }
    }


}
