using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
[CustomPropertyDrawer(typeof(Transition), true)]
public class TransitionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(position, new GUIContent("Stuff"));

        //EditorGUI.PropertyField(position, property.FindPropertyRelative("_transitionType"), true);
        //property.serializedObject.targetObject.GetType().GetField("transitionType").GetValue(typeof(string)) = property.FindPropertyRelative("_transitionType").stringValue;
        //Tools.SetValue(property.serializedObject.targetObject, property.FindPropertyRelative("_transitionType").stringValue);
        //((Transition)(property)).transitionType = property.FindPropertyRelative("_transitionType");
        /*switch (property.FindPropertyRelative("_transitionType").stringValue)
        {
            case "clear":
                //property.SetValue(new ClearTransition());
                ClearTransition temp = new ClearTransition();
                property.FindPropertyRelative("this").objectReferenceInstanceIDValue = (new ClearTransition());
                break;
            case "time":
                //property.SetValue(new TimeTransition());
                //property.objectReferenceValue = new TimeTransition();
                break;
            case "score":
                //property.SetValue(new ScoreTransition());
                //property.objectReferenceValue = new ScoreTransition();
                break;
        }*/
        /*if (property.FindPropertyRelative("_transitionType").stringValue.Length % 2 == 0)
        {
            property.FindPropertyRelative("hasTransitioned").boolValue = false;
        }
        else
        {
            property.FindPropertyRelative("hasTransitioned").boolValue = true;
        }*/


        //EditorGUI.PropertyField(new Rect(position.x, position.y + base.GetPropertyHeight(property, label), position.width, position.height)/*new Rect(position.position, new Vector2(position.width, (position.height / 2f)))*/, property.FindPropertyRelative("hasTransitioned"), true);
        //EditorGUILayout.PropertyField(property.FindPropertyRelative("_transitionType"), new GUIContent("Class Type"), true);
        //EditorGUILayout.PropertyField(property.FindPropertyRelative("hasTransitioned"), new GUIContent("Has Transitioned?"), true);
        /*


        if (GUI.Button(position, "Time")) {
            Debug.Log("Pressed!");
        }

        EditorGUI.EndProperty();
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label)/* * 2f;
    }
}
*/

/*public class PropertyDrawerUtility
{
    public static T GetActualObjectForSerializedProperty<T>(FieldInfo fieldInfo, SerializedProperty property) where T : class
    {
        var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
        if (obj == null) { return null; }

        T actualObject = null;
        if (obj.GetType().IsArray)
        {
            var index = Convert.ToInt32(new string(property.propertyPath.Where(c => char.IsDigit(c)).ToArray()));
            actualObject = ((T[])obj)[index];
        }
        else
        {
            actualObject = obj as T;
        }
        return actualObject;
    }
}*/

[CustomPropertyDrawer(typeof(SpawnRound), true)]
public class TransitionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);

        EditorGUI.BeginProperty(position, label, property);

        if (GUI.Button(new Rect(position.x, position.y + EditorGUI.GetPropertyHeight(property), position.width, base.GetPropertyHeight(property, label)), "Time"))
        {
            Debug.Log("Pressed!");

            Debug.Log(property.FindPropertyRelative("transitions").arraySize);
            property.FindPropertyRelative("transitions").InsertArrayElementAtIndex(property.FindPropertyRelative("transitions").arraySize);
            //Transition temp = property.FindPropertyRelative("transitions").GetArrayElementAtIndex(property.FindPropertyRelative("transitions").arraySize - 1).objectReferenceValue as System.Object as Transition;
            //temp = new TimeTransition();

            (property.FindPropertyRelative("transitions").GetArrayElementAtIndex(property.FindPropertyRelative("transitions").arraySize - 1).objectReferenceValue as System.Object as Transition) = new TimeTransition();

        }
        if (GUI.Button(new Rect(position.x, position.y + EditorGUI.GetPropertyHeight(property) + base.GetPropertyHeight(property, label), position.width, base.GetPropertyHeight(property, label)), "Clear"))
        {
            Debug.Log("Pressed!");

            //property.pro
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        //return base.GetPropertyHeight(property, label)/* * 2f*/;
        //return EditorGUI.GetPropertyHeight(property) + (base.GetPropertyHeight(property, label) * 1f);
        return base.GetPropertyHeight(property, label) * (property.CountInProperty() + 4f);
    }
}