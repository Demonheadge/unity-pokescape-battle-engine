// 28/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MoveDatabase", menuName = "Game/Move Database")]
public class MoveDatabase : ScriptableObject
{
    public List<MoveInformation> moves = new List<MoveInformation>();
}
