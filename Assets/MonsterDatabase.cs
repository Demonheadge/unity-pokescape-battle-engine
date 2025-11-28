// 28/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MonsterDatabase", menuName = "Game/Monster Database")]
public class MonsterDatabase : ScriptableObject
{
    public List<SpeciesInfo> monsters = new List<SpeciesInfo>();
}
