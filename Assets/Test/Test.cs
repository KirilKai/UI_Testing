// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;
// using UnityEngine.UIElements;
// using UnityEditor.UIElements;

// public class Test : EditorWindow
// {
//     VisualElement container;
//     [MenuItem("Testing/ Test Window")]
//     public static void ShowWindow()
//     {
//         Test window = GetWindow<Test>();
//         window.titleContent = new GUIContent("Test window");
//         window.minSize = new Vector2(500, 500);
//     }

//     public void CreateGUI()
//     {
//         container = rootVisualElement;
//         VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Test/test.uxml");
//         container.Add(visualTree.Instantiate());

//         StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Test/test.uss");
//         container.styleSheets.Add(styleSheet);
//     }
// }
