using NavMeshPlus.Components.Editors;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

//***********************************************************************************
// Contributed by author jl-randazzo github.com/jl-randazzo
//***********************************************************************************
namespace NavMeshPlus.Extensions.Editors
{
    [CustomPropertyDrawer(typeof(NavMeshAreaAttribute))]
    public class NavMeshAreaAttributePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            NavMeshComponentsGUIUtility.AreaPopup(position, label.text, property);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 20;
    }
}
