using UnityEngine;

public class CleaveEffect : MonoBehaviour
{
    public float radius = 5f; // Radius of the arc
    public float angle = 90f; // Angle of the arc in degrees
    public int segments = 30; // Number of segments in the arc
    public float duration = 0.5f; // Duration of the effect
    public Material cleaveMaterial; // Material for the arc

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private float timer;

    void Start()
    {
        // Add MeshFilter and MeshRenderer components
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshRenderer.material = cleaveMaterial;

        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < duration)
        {
            GenerateArcMesh(timer / duration); // Update the arc over time
        }
        else
        {
            Destroy(gameObject); // Destroy the effect once completed
        }
    }

    void GenerateArcMesh(float progress)
    {
        Mesh mesh = new Mesh();

        float currentAngle = angle * progress; // Adjust the arc angle based on progress
        float angleStep = currentAngle / segments;

        Vector3[] vertices = new Vector3[segments + 2]; // +2 for center and last point
        int[] triangles = new int[segments * 3]; // 3 vertices per triangle

        vertices[0] = Vector3.zero; // Center of the arc
        for (int i = 0; i <= segments; i++)
        {
            float theta = Mathf.Deg2Rad * (-currentAngle / 2 + angleStep * i); // Calculate angle
            vertices[i + 1] = new Vector3(Mathf.Sin(theta), 0, Mathf.Cos(theta)) * radius; // Point on the arc
        }

        // Create triangles
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }
}