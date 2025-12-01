// 28/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpDatabase", menuName = "Databases/LevelUpDatabase")]
public class LevelUpDatabase : ScriptableObject
{
    public List<LevelUpInformation> levelUpData = new List<LevelUpInformation>();
}