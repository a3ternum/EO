using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CleaveCircleMesh : MonoBehaviour
{
    public int segments = 50; // Number of segments
    public float radiusOuter; // Outer radius
    public float radiusInner; // Inner radius
    public float angle = 90f; // Angle of the arc in degrees
    public Creature user;
    public Skill skill;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
       

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingOrder = 3; // Set Order in Layer to 3
        meshRenderer.sortingLayerName = "Default"; // Optional: Set a specific sorting layer

        // outer radius depends on radius of the skill
        radiusOuter = skill.radius;
        radiusInner = 0.8f * radiusOuter; // inner radius is 80% of outer radius

        // radius should scale with area of effect increase
        float areaOfAttackIncrease = user.creatureStats.areaOfEffectIncreases;
        radiusOuter = radiusOuter * (1 + areaOfAttackIncrease);
        radiusInner = radiusInner * (1 + areaOfAttackIncrease);

        meshFilter.mesh = GenerateMesh();
    }

    Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        int vertexCount = segments * 2;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(segments - 1) * 6]; // 6 indices per quad

        float angleStep = angle / (segments - 1);
        for (int i = 0; i < segments; i++)
        {
            float currentAngle = Mathf.Deg2Rad * angleStep * i;

            // Outer vertex
            vertices[i] = new Vector3(Mathf.Cos(currentAngle) * radiusOuter, Mathf.Sin(currentAngle) * radiusOuter, 0f);

            // Inner vertex
            vertices[i + segments] = new Vector3(Mathf.Cos(currentAngle) * radiusInner, Mathf.Sin(currentAngle) * radiusInner, 0f);

            if (i < segments - 1)
            {
                // First triangle
                int startIndex = i * 6;
                triangles[startIndex] = i;
                triangles[startIndex + 1] = i + segments;
                triangles[startIndex + 2] = i + 1;

                // Second triangle
                triangles[startIndex + 3] = i + 1;
                triangles[startIndex + 4] = i + segments;
                triangles[startIndex + 5] = i + segments + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    public void UpdatePositionAndRotation(Vector2 position , Vector2 direction)
    {
        transform.position = position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - this.angle / 2);
    }

}