using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PlayModeReadOnlyAttribute))]
public class PlayModeReadOnlyPropertyDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUI.GetPropertyHeight(property, label, true);
	}

	// Draw a disabled property field
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// while GUI.enabled is false, all attributes are drawn but not editable
		// we dont want them to be editable in play mode so we set enabled accordingly
		GUI.enabled = !Application.isPlaying;
		// just draw the default property field
		EditorGUI.PropertyField(position, property, label, true);
		GUI.enabled = true;
	}
}