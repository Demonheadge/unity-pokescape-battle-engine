// 13/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleRules", menuName = "Battle/BattleRules")]
public class BattleRules : ScriptableObject
{
    public enum BattleType { SingleBattle, DoubleBattle, MultiBattle }
    public BattleType battleType;

    public int maxMonstersPerTeam;
    public bool canSpawnMoreMonstersMidBattle;
    public int wildMonstersEncountered;
    public int MAX_TRAINER_ITEMS;   //Amount of items a trainer can use trainer a battle.
    public int MAX_PARTY_SIZE;
}
