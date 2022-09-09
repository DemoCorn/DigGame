using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
[CustomEditor(typeof(Drop))]
public class DropEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        Drop drop = (Drop)target;
        SerializedObject soObject = new SerializedObject(target);
        SerializedProperty list;

        drop.smartDrop = EditorGUILayout.Toggle("Use Smart Drop", drop.smartDrop);
        drop.dropBlueprints = EditorGUILayout.Toggle("Drop Blueprints", drop.dropBlueprints);

        serializedObject.ApplyModifiedProperties();

        if (drop.smartDrop)
        {
            if (drop.dropBlueprints)
            {
                list = soObject.FindProperty("blueprintDropByLevel");
            }
            else
            {
                list = soObject.FindProperty("itemDropByLevel");
            }

            EditorGUILayout.PropertyField(list, true);
        }
        else
        {
            if (drop.dropBlueprints)
            {
                drop.blueprintDrops = (BlueprintDropTable)EditorGUILayout.ObjectField("Blueprint Drops", drop.blueprintDrops, typeof(BlueprintDropTable), true);

                //drop.blueprintDrops = seObject.FindProperty("blueprintDrops");
                //drop.blueprintDrops = (BlueprintDropTable)list;
            }
            else
            {
                drop.itemDrops = (ItemDropTable)EditorGUILayout.ObjectField("Item Drops", drop.itemDrops, typeof(ItemDropTable), true);
            }
        }

    }
}
*/