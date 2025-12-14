// 28/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TEST_MonsterDatabase", menuName = "Game/TEST Monster Database")]
public class TEST_MonsterDatabase : ScriptableObject
{
    public List<Monster.SpeciesInfo> monsters = new List<Monster.SpeciesInfo>();
}
