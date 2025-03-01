#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace NavMeshPlus.Extensions
{
    public static class NavMeshExtensionEditor
    {
        [UnityEditor.Callbacks.DidReloadScripts]
        public static void OnScriptReload()
        {
            var extensions = Resources.FindObjectsOfTypeAll(typeof(NavMeshExtension)) as NavMeshExtension[];
            foreach (var e in extensions)
                e.ConnectToVcam(true);
        }
    }
}
#endif