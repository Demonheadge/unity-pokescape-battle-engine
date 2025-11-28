// 26/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public enum MonsterType
{
    NONE,
    EARTH,
    FIRE,
    WATER
}


[CreateAssetMenu(fileName = "Monster", menuName = "Pokescape/Monster")]
public class MonsterInformation : ScriptableObject
{
    public int id;
    public string speciesName;
    public MonsterType type;
    public int health;
    public int attack;
    public int defense;
    public int speed;
    public int gender;
}
