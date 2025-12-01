// 28/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LevelUpInformation
{
    public int ID; // Monster ID
    public Species name; // Monster species name
    public List<MoveLevelPair> moves = new List<MoveLevelPair>(); // List of moves with levels
}

[System.Serializable]
public class MoveLevelPair
{
    public int level; // Level at which the move is learned
    public Move move; // Move type
}