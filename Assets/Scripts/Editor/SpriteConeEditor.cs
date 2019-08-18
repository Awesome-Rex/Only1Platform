using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteCone))]
public class SpriteConeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpriteCone original = (SpriteCone)target;

        original.degrees = EditorGUILayout.FloatField("Degrees", original.degrees);
        original.radius = EditorGUILayout.FloatField("Radius", original.radius);
        EditorGUILayout.ObjectField("Cone Object", original.cone, typeof(GameObject), true);
    }
}
