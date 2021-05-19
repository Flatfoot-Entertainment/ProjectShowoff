using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

[CustomEditor(typeof(ModularConveyor)), CanEditMultipleObjects]
public class ModularConveyorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Conveyor")){
            ((ModularConveyor)target).CreateConveyor();
        }
    }
}
