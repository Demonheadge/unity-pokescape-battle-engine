// 28/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class MoveInformation
{
    public Move move;
    public string name;
    public MoveType type;
    public MoveEffect effect;
    public int damage;
    public int accuracy;
    public int effectSecondary;
    public MoveCatagory catagory;
}

public enum MoveType
{
    NONE,
    AIR,
    MIND,
    WATER,
    EARTH,
    FIRE,
    BODY,
    COSMIC,
    CHAOS,
    ASTRAL,
    NATURE,
    LAW,
    DEATH,
    BLOOD,
    SOUL,
}

public enum MoveCatagory
{
    STATUS,
    MELEE,
    MAGIC,
    RANGE,
}

public enum Move
{
    NONE,
    TACKLE,
    WIND_STRIKE,
    WIND_BLAST,
    WIND_WAVE,
    WIND_SURGE,
    EARTH_STRIKE,
    EARTH_BLAST,
    EARTH_WAVE,
    EARTH_SURGE,
    WATER_STRIKE,
    WATER_BLAST,
    WATER_WAVE,
    WATER_SURGE,
    FIRE_STRIKE,
    FIRE_BLAST,
    FIRE_WAVE,
    FIRE_SURGE,
    ICE_RUSH,
    ICE_BURST,
    ICE_BLITZ,
    ICE_BARRAGE,
    BLOOD_RUSH,
    BLOOD_BURST,
    BLOOD_BLITZ,
    BLOOD_BARRAGE,
    SMOKE_RUSH,
    SMOKE_BURST,
    SMOKE_BLITZ,
    SMOKE_BARRAGE,
    SHADOW_RUSH,
    SHADOW_BURST,
    SHADOW_BLITZ,
    SHADOW_BARRAGE,
}

public enum MoveEffect
{
    None,
    EffectSetSpeedReduction,
    EffectSetBurn
}
