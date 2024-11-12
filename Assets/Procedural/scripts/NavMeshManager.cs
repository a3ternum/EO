using NavMeshPlus.Components;
using UnityEngine;



public class NavMeshManager : MonoBehaviour
{
    [SerializeField]
    private GameObject navMeshSurfaceObject;

    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        navMeshSurface = navMeshSurfaceObject.GetComponent<NavMeshSurface>();
        if (navMeshSurface == null)
        {
            Debug.LogError("NavMeshSurface not found");
        }
        else
        {

        }
    }

    public void GenerateSurface()
    {
        navMeshSurface.BuildNavMesh();
    }

}
