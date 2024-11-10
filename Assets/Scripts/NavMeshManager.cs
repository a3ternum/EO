using NavMeshPlus.Components;
using UnityEngine;



public class NavMeshManager : MonoBehaviour
{
    [SerializeField]
    private GameObject navMeshSurfaceObject;

    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        Debug.Log("assigning navMeshSurface");
        navMeshSurface = navMeshSurfaceObject.GetComponent<NavMeshSurface>();
        if (navMeshSurface == null)
        {
            Debug.LogError("NavMeshSurface not found");
        }
        else
        {
            Debug.Log("NavMeshSurface is:" + navMeshSurface);
        }
    }

    public void GenerateSurface()
    {
        navMeshSurface.BuildNavMesh();
    }

}
