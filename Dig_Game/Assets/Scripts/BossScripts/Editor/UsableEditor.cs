using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Usable))]
public class UsableEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();
        /*
        Usable item = (Usable)target;
        item.effect = EditorGUILayout.ObjectField(item.effect, typeof(UsableEffect), false) as UsableEffect;
        */
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Usable item = (Usable)target;

        if (item == null || item.itemSprite == null)
            return null;

        Texture2D tex = new Texture2D(width, height);
        EditorUtility.CopySerialized(item.itemSprite.texture, tex);

        return tex;
    }
}
