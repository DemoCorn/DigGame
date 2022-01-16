using System.IO;
using UnityEngine;
using UnityEditor;


/*
[CustomEditor(typeof(Item))]
public class ItemEditor : UnityEditor.Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Item item = (Item)target;

        if (item == null || item.itemSprite == null)
            return null;

        Texture2D tex = new Texture2D(width, height);
        EditorUtility.CopySerialized(item.itemSprite.texture, tex);

        return tex;
    }
}
*/