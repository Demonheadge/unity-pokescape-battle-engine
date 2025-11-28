// 26/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SpeciesInfo
{
    public Species species;
    public string name;
    public MonsterType type;
    public int baseHP;
    public int baseAttack;
    public int baseDefense;
    public int baseSpeed;
    public Sprite front_sprite;
    public Sprite back_sprite;
    public Sprite partyicon;
}

public class Extra_1_Monster_Info
{
    public int experience;
    public int HP_IV;
    public int Attack_IV;
    public int Defense_IV;
    public int Speed_IV;
    public Move move_1;
    public Move move_2;
    public Move move_3;
    public Move move_4;
}

public class Extra_2_Monster_Info
{
    public int level;
    public int max_HP;
    public int current_HP;
    public int current_Attack;
    public int current_Defense;
    public int current_Speed;
}

public enum MonsterType
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

public enum Species
{
    NONE,
    GUTHLING,
    GUTHBIRD,
    GUTHRAPTOR,
    ZAMLING,
    ZAMBIRD,
    ZAMOHAWK,
    SARALING,
    SARABIRD,
    SARAOWL,
}


/*
[CreateAssetMenu(fileName = "Monster", menuName = "Pokescape/Monster")]
public class SpeciesInfo : ScriptableObject
{
    public int baseHP;
    public int baseAttack;
    public int baseDefense;
    public int baseSpeed;
    public MonsterType type1;
    public MonsterType type2;
}
*/
/*
public enum SpeciesInfo
{
    MONSTER_DATA_SPECIES,
    MONSTER_DATA_NICKNAME,
    MONSTER_DATA_MOVE1,
    MONSTER_DATA_MOVE2,
    MONSTER_DATA_MOVE3,
    MONSTER_DATA_MOVE4,
    MONSTER_DATA_EXP,
    MONSTER_DATA_LEVEL,
    MONSTER_DATA_HP,
    MONSTER_DATA_MAX_HP,
    MONSTER_DATA_ATK,
    MONSTER_DATA_DEF,
    MONSTER_DATA_SPEED,
}*/