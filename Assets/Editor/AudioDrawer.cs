using UnityEngine;
using UnityEditor;

// Custom property drawer for [Audio] that shows a volume slider and player/stop buttons on one line

[CustomPropertyDrawer(typeof(AudioAttribute))]
public class AudioDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty PropClip = property.FindPropertyRelative("clip");
        SerializedProperty PropVolume = property.FindPropertyRelative("volume");

        // Naming convention I've used is music and sfx prefixes, remove this to make the display name shorter
        string Name = property.displayName;
        Name = Name.Replace("Sfx ", "").Replace("Music ", "");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(Name, GUILayout.Width(EditorGUIUtility.labelWidth - 40));
        EditorGUILayout.PropertyField(PropClip, GUIContent.none, GUILayout.Width(80));

        // Volume slider
        PropVolume.floatValue = EditorGUILayout.Slider(PropVolume.floatValue, 0, 2);

        // Preview audio clip
        if (GUILayout.Button(">", GUILayout.Width(18), GUILayout.Height(16))) 
        {
            AudioManager.Instance.SourceSFX.Stop();
            AudioManager.Instance.SourceSFX.PlayOneShot(PropClip.objectReferenceValue as AudioClip, PropVolume.floatValue);
        }
        // Stop audio preview
        if (GUILayout.Button("x", GUILayout.Width(18), GUILayout.Height(16))) 
        {
            AudioManager.Instance.SourceSFX.Stop();
        }

        EditorGUILayout.EndHorizontal();

        property.serializedObject.ApplyModifiedProperties();
    }

    // This sets the correct height for some reason, otherwise it is too wide
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 0;
    }
}