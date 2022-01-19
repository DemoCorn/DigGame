using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : UnityEditor.Editor
{
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        Item item = (Item)target;

        if (item == null || item.itemSprite == null)
            return null;

        Texture2D tex = new Texture2D(width, height);
        Texture2D iconTexture = new Texture2D((int)item.itemSprite.rect.width, (int)item.itemSprite.rect.height);
        Color[] actualItem = item.itemSprite.texture.GetPixels((int)item.itemSprite.rect.x,
            (int)item.itemSprite.rect.y,
            (int)item.itemSprite.rect.width,
            (int)item.itemSprite.rect.height);
        iconTexture.SetPixels(actualItem);

        EditorUtility.CopySerialized(iconTexture, tex);

        return tex;
    }
}