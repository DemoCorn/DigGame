using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Blueprint))]
public class BlueprintEditor : UnityEditor.Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Blueprint item = (Blueprint)target;

        if (item == null || item.result.item.itemSprite == null)
            return null;

        Texture2D tex = new Texture2D(width, height);
        Texture2D iconTexture = new Texture2D((int)item.result.item.itemSprite.rect.width, (int)item.result.item.itemSprite.rect.height);
        Color[] actualItem = item.result.item.itemSprite.texture.GetPixels((int)item.result.item.itemSprite.rect.x,
            (int)item.result.item.itemSprite.rect.y,
            (int)item.result.item.itemSprite.rect.width,
            (int)item.result.item.itemSprite.rect.height);
        iconTexture.SetPixels(actualItem);
        EditorUtility.CopySerialized(item.result.item.itemSprite.texture, tex);

        return tex;
    }
}