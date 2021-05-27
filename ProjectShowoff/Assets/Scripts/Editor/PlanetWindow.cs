using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlanetWindow : EditorWindow
{
    private static Object sample;

    [MenuItem("Game Tools/Planet Creator")]

    public static void ShowWindow()
    {
        GetWindow<PlanetWindow>(false, "Planet Creator", true);
        GameObject center = new GameObject("Planet Center", typeof(Transform), typeof(PlanetCreationScript));
        sample = center;
    }

    private void OnGUI()
    {
        EditorGUILayout.Toggle("planet scale random?", false);
        EditorGUILayout.IntField("planet count", 3);

        //TODO get bounds from point position as a center, and a box collider's bounds (to implement in the actual script)
        sample = EditorGUILayout.ObjectField("Planet Center: ", sample, typeof(Object), true);
    }
}
