

using System;
using UnityEngine;

public enum MON_DATA
{
    MON_DATA_PERSONAL_ID,
    MON_DATA_NICKNAME,
    MON_DATA_SPECIES,
    MON_DATA_MOVE1,
    MON_DATA_MOVE2,
    MON_DATA_MOVE3,
    MON_DATA_MOVE4,
    MON_DATA_EXP,
    MON_DATA_STATUS,
    MON_DATA_LEVEL,
    MON_DATA_HP,
    MON_DATA_MAX_HP,
    MON_DATA_SPEED,
    MON_DATA_ATK_MELEE,
    MON_DATA_ATK_MAGIC,
    MON_DATA_ATK_RANGED,
    MON_DATA_DEF_MELEE,
    MON_DATA_DEF_MAGIC,
    MON_DATA_DEF_RANGED,
    MON_DATA_MET_LEVEL,
};

struct Monster_Substruct_Data
{
    Species species;
    Items heldItem;
    int experience;
    int friendship;
}; 

struct Monster_Substruct_Moves
{
    Move MOVE_1;
    Move MOVE_2;
    Move MOVE_3;
    Move MOVE_4;
}

struct Monster_Substruct_Skills
{
    int skill_attack;
    int skill_defense;
    int skill_strength;
    int skill_magic;
    int skill_ranged;
    int skill_necromancy;
    int skill_prayer;
    int skill_summoning;
    int skill_hitpoints;
    int skill_slayer;
    int skill_agility;
    int skill_mining;
    int skill_smithing;
    int skill_fishing;
    int skill_woodcutting;
    int skill_cooking;
    int skill_fletching;
    int skill_crafting;
    int skill_firemaking;
    int skill_runecrafting;
    int skill_dungeoneering;
    int skill_sailing;
    int skill_herblore;
    int skill_farming;
    int skill_construction;
    int skill_divination;
    int skill_hunter;
    int skill_archaeology;
    int skill_thieving;
    int skill_invention;
}

class Monster_Substruct_Combined
{
    public Monster_Substruct_Data MonsterSubstructData;
    public Monster_Substruct_Moves MonsterSubstructMoves;
    public Monster_Substruct_Skills MonsterSubstructSkills;
};

struct BoxMonster_Substruct
{
    int personal_ID;
    string nickname;
    bool hasSpecies;
    bool isEgg;

    public Monster_Substruct_Combined MonsterSubstructCombined;
};

struct Monster_Substruct
{
    public BoxMonster_Substruct boxInfo;
    Status status;
    int level;
    int hp;
    int maxHP;
    int speed;
    int atk_melee;
    int atk_magic;
    int atk_ranged;
    int def_melee;
    int def_magic;
    int def_ranged;
};

struct MonsterStorageSystem
{
    int currentBox;
    //struct BoxPokemon boxes[TOTAL_BOXES_COUNT][IN_BOX_COUNT];
    //string boxNames[TOTAL_BOXES_COUNT][BOX_NAME_LENGTH + 1];
};

struct BattleMonster    //Data that is used within a battle.
{
    int personal_ID;
    string nickname;
    Species species;
    Status status;
    int level;
    int hp;
    int maxHP;
    int speed;
    int atk_melee;
    int atk_magic;
    int atk_ranged;
    int def_melee;
    int def_magic;
    int def_ranged;
    Move MOVE_1;
    Move MOVE_2;
    Move MOVE_3;
    Move MOVE_4;
    Type type1;
    Type type2;
    int friendship;
    Items item;
    int experience;
    int skill_attack;
    int skill_defense;
    int skill_strength;
    int skill_magic;
    int skill_ranged;
    int skill_necromancy;
    int skill_prayer;
    int skill_summoning;
    int skill_hitpoints;
    int skill_slayer;
    int skill_agility;
    int skill_mining;
    int skill_smithing;
    int skill_fishing;
    int skill_woodcutting;
    int skill_cooking;
    int skill_fletching;
    int skill_crafting;
    int skill_firemaking;
    int skill_runecrafting;
    int skill_dungeoneering;
    int skill_sailing;
    int skill_herblore;
    int skill_farming;
    int skill_construction;
    int skill_divination;
    int skill_hunter;
    int skill_archaeology;
    int skill_thieving;
    int skill_invention;
};

struct Evolution
{
    EvolutionMethods method;
    int param;
    Species targetSpecies;
};


struct SpeciesInfomation
{
//Base Species data
    int base_hp;
    int base_speed;
    int base_atk_melee;
    int base_atk_magic;
    int base_atk_ranged;
    int base_def_melee;
    int base_def_magic;
    int base_def_ranged;
    Type type1;
    Type type2;
    int catchRate;
    int genderRatio;
    int base_friendship;
// Bestiary data
    Species speciesName;
    int Bestiary_ID_Number;
    int height;
    int weight;
    string monster_description;
// Graphical Data
    bool Flip_Sprite;
    Sprite front_sprite;
    Sprite back_sprite;
    Sprite partyicon;
    //Pal shiny_palette;
    int frontPicYOffset; // The number of pixels between the drawn pixel area and the bottom edge of the sprite.
    int backPicYOffset; // The number of pixels between the drawn pixel area and the bottom edge.
    int enemyMonElevation; // This determines how much higher above the usual position the enemy Pokémon is during battle. Species that float or fly have nonzero values.
// Flags
    bool isRare;                    //Always set to false.
    bool isAlternateForm;           //Used to assist with Bestiary_ID_Number.
    bool isStoneOfJasEvolution;     //MegaEvolution
// Move Data
    //const struct LevelUpMove *levelUpLearnset;
    //const u16 *teachableLearnset;
    //const struct Evolution *evolutions;
    //const u16 *formSpeciesIdTable;
    //const struct FormChange *formChangeTable;
};

struct BattleMoveInfo
{
    Move move;
    string name;
    MoveType type;
    MoveEffect effect;
    int power;
    int accuracy;
    int effectSecondary;
    MoveCatagory catagory;
    Target_Move target;
    int priority;
// Flags
    bool twoTurnMove;
    bool makesContact;
}


public enum Target_Move
{
    MOVE_TARGET_SELECTED,
    MOVE_TARGET_RANDOM,
    MOVE_TARGET_ALL_OPPONENTS_SIDE,
    MOVE_TARGET_ALL_ALLY_SIDE,
    MOVE_TARGET_USER,
    MOVE_TARGET_ALL,
}

struct LevelUpMove  //What move is given upon level up.
{
    Move move;
    int level;
};