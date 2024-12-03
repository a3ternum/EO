using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class CleaveCircleMeshAnimated : MonoBehaviour
{
    public int segments = 50; // Number of segments
    public float radiusOuter; // Outer radius
    public float radiusInner; // Inner radius
    public float angle = 90f; // Angle of the arc in degrees
    public Creature user;
    public Cleave skill;

    //public Material meshMaterial;
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        //meshRenderer.material = meshMaterial;
        meshRenderer.sortingOrder = 3; // Set Order in Layer to 3
        meshRenderer.sortingLayerName = "Default"; // Optional: Set a specific sorting layer

        // outer radius depends on radius of the skill
        radiusOuter = skill.radius;
        radiusInner = 0.8f * radiusOuter; // inner radius is 80% of outer radius

        // radius should scale with area of effect increase
        float areaOfAttackIncrease = user.creatureStats.areaOfEffectIncreases;
        radiusOuter = radiusOuter * (1 + areaOfAttackIncrease);
        radiusInner = radiusInner * (1 + areaOfAttackIncrease);

        StartCoroutine(AnimateCleave());
    }

    Mesh GeneratePartialMesh(int currentSegments)
    {
        Mesh mesh = new Mesh();

        int vertexCount = currentSegments * 2;
        Vector3[] vertices = new Vector3[vertexCount];
        Vector2[] uv = new Vector2[vertexCount]; // add UV array
        int[] triangles = new int[(currentSegments - 1) * 6]; // 6 indices per quad

        float angleStep = angle / (segments - 1);
        for (int i = 0; i < currentSegments; i++)
        {
            float currentAngle = Mathf.Deg2Rad * angleStep * i;

            // Outer vertex
            vertices[i] = new Vector3(Mathf.Cos(currentAngle) * radiusOuter, Mathf.Sin(currentAngle) * radiusOuter, 0f);
            uv[i] = new Vector2((float)i / (segments - 1), 1f); // set UV for outer vertex

            // Inner vertex
            vertices[i + currentSegments] = new Vector3(Mathf.Cos(currentAngle) * radiusInner, Mathf.Sin(currentAngle) * radiusInner, 0f);
            uv[i + currentSegments] = new Vector2((float)i / (segments - 1), 0f); // set UV for inner vertex

            if (i < currentSegments - 1)
            {
                // First triangle
                int startIndex = i * 6;
                triangles[startIndex] = i;
                triangles[startIndex + 1] = i + currentSegments;
                triangles[startIndex + 2] = i + 1;

                // Second triangle
                triangles[startIndex + 3] = i + 1;
                triangles[startIndex + 4] = i + currentSegments;
                triangles[startIndex + 5] = i + currentSegments + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv; // set UV array
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    public void UpdatePositionAndRotation(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - this.angle / 2);
    }

    IEnumerator AnimateCleave()
    {
        float animationTime = 1 / (skill.attackSpeed * user.currentAttackSpeed * 2); // the two is to account for the fact that we delete the animation 2x faster
        float timePerSegments = animationTime / segments;
        for (int i = 1; i <= segments; i++)
        {
            // Temporarily adjust the number of segments rendered
            Mesh partialMesh = GeneratePartialMesh(i);
            GetComponent<MeshFilter>().mesh = partialMesh;

            yield return new WaitForSeconds(timePerSegments); // Adjust speed here
        }
    }
}