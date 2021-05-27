using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetCreationScript))]
public class SamplePlanet : Editor
{
    private SerializedProperty sampleProperty;
    private void OnEnable()
    {
        sampleProperty = serializedObject.FindProperty("isPlanetScaleRandom");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // if (sampleProperty.boolValue)
        // {
        //     EditorGUILayout.FloatField(serializedObject.FindProperty("minPlanetScale").floatValue);
        //     EditorGUILayout.FloatField(serializedObject.FindProperty("maxPlanetScale").floatValue);
        // }
        // if (GUILayout.Button("Generate"))
        // {
        //     if (serializedObject.targetObject is PlanetCreationScript planetCreationScript)
        //     {
        //         planetCreationScript.CreatePlanets();
        //     }
        // }
    }
}
