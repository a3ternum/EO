//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif
//[CustomEditor(typeof(AbstractMapGenerator), true)]
//public class RandomMapGeneratorEditor : Editor
//{
//    AbstractMapGenerator generator;

//    private void Awake()
//    {
//        generator = (AbstractMapGenerator)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        if (GUILayout.Button("Create Map"))
//        {
//            generator.GenerateMap();
//        }
//    }
//}
