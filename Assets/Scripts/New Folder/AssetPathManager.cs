// 14/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public static class AssetPathManager
{
    // Sprite paths
    public static readonly Dictionary<string, string> SpritePaths = new Dictionary<string, string>
    {
        { "OZAN", "Resources/trainers/ozan.png" },
        { "RAPTOR", "Resources/trainers/raptor.png" },
        { "DEMONHEADGE", "Resources/trainers/demonheadge.png" }
    };

    // Music paths
    public static readonly Dictionary<string, string> MusicPaths = new Dictionary<string, string>
    {
        { "MUS_TITLESCREEN", "music/mus-ps-vs-tzhaar" },
        { "MUS_BANK", "music/MUS-PS-WISE-OLD-MAN-THEME" },
        { "MUS_VS_WILD_F2P", "music/MUS-PS-WISE-OLD-MAN-THEME" }
    };

    public static string GetSpritePath(string key)
    {
        if (SpritePaths.ContainsKey(key))
        {
            return SpritePaths[key];
        }
        return null;
    }

    public static string GetMusicPath(string key)
    {
        if (MusicPaths.ContainsKey(key))
        {
            return MusicPaths[key];
        }
        return null;
    }
}
