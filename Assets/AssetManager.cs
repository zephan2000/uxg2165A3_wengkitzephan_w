using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor.AddressableAssets;
using UnityEngine.AddressableAssets;
// Raiyan
public static class AssetManager
{

    private static string imagePath = "Assets/ArtAssets/{0}";
    //private static string testpath = "Assets/ArtAssets/{0}";
    //private static string imagePath = "Assets/{0}";

    public static void LoadSprite(string spriteName, System.Action<Sprite> onLoaded)
    {
        Addressables.LoadAssetAsync<Sprite>(string.Format(imagePath, spriteName)).Completed += (loadedSprite) =>
        {
            onLoaded?.Invoke(loadedSprite.Result);
        };
    }
}
