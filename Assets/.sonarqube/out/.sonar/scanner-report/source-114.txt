using NavMeshPlus.Components;
using UnityEngine;



public class NavMeshManager : MonoBehaviour
{
    [SerializeField]
    private GameObject navMeshSurfaceObject;

    private NavMeshSurface navMeshSurface;


    public void GenerateSurface()
    {
        navMeshSurface = navMeshSurfaceObject.GetComponent<NavMeshSurface>();
        if (navMeshSurface == null)
        {
            Debug.LogError("NavMeshSurface not found");
            return;
        }
        navMeshSurface.BuildNavMesh();
    }

}
