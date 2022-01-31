using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Equipment))]
public class EquipmentEditor : UnityEditor.Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Equipment item = (Equipment)target;

        if (item == null || item.itemSprite == null)
            return null;

        Texture2D tex = new Texture2D(width, height);
        Texture2D iconTexture = new Texture2D((int)item.itemSprite.rect.width, (int)item.itemSprite.rect.height);
        Color[] actualItem = item.itemSprite.texture.GetPixels((int)item.itemSprite.rect.x,
            (int)item.itemSprite.rect.y,
            (int)item.itemSprite.rect.width,
            (int)item.itemSprite.rect.height);
        iconTexture.SetPixels(actualItem);

        EditorUtility.CopySerialized(item.itemSprite.texture, tex);
        return tex;
    }
}