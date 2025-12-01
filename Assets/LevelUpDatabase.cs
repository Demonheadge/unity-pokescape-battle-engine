// 28/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelUpDatabase", menuName = "Game/LevelUp Database")]
public class LevelUpDatabase : ScriptableObject
{
    public List<LevelUpInformation> levelUpData = new List<LevelUpInformation>();
}
