using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Drop))]
public class DropEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        Drop drop = (Drop)target;
        SerializedObject seObject = new SerializedObject(target);
        SerializedProperty list;

        drop.dropBlueprints = EditorGUILayout.Toggle("Drop Blueprints", drop.dropBlueprints);
        drop.heapDrop = EditorGUILayout.Toggle("Drops are more than 100%", drop.heapDrop);
        serializedObject.ApplyModifiedProperties();

        if(drop.dropBlueprints)
        {
            list = seObject.FindProperty("blueprintDrops");
        }
        else
        {
            list = seObject.FindProperty("itemDrops");
        }

        EditorGUILayout.PropertyField(list, true);

    }
}
