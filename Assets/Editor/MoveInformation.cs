// 25/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public enum MoveType
{
    Air,
    Mind,
    Water,
    Earth,
    Fire,
    Body,
    Cosmic,
    Chaos,
    Astral,
    Nature,
    Law,
    Death,
    Blood,
    Soul,
    Wrath,
    Time
}

public enum MoveCatagory
{
    STATUS,
    MELEE,
    MAGIC,
    RANGE,
}

public enum MoveEffect
{
    None,
    EffectSetSpeedReduction,
    EffectSetBurn
}

[CreateAssetMenu(fileName = "Move", menuName = "Pokescape/Move")]
public class MoveInformation : ScriptableObject
{
    public string moveName;
    public MoveType type;
    public MoveEffect effect;
    public int effect_secondary;
    public int damage;
    public int accuracy;
    public MoveCatagory catagory;
}
