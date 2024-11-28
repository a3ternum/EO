#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace NavMeshPlus.Components
{
    [CustomEditor(typeof(NavMeshSurface))]
    public class NavMeshSurfaceEditorPart : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var surface = (NavMeshSurface)target;

            if (GUILayout.Button("Bake NavMesh"))
            {
                surface.BuildNavMesh();
            }
        }

        [InitializeOnLoadMethod]
        static void OnEditorLoad()
        {
            EditorSceneManager.sceneOpened += (scene, mode) =>
            {
                foreach (var surface in Object.FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None))
                {
                    surface.AddData();
                }
            };
        }
    }
}
#endif