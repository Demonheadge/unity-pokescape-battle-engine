// 29/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class BattleEngine : MonoBehaviour
{
    public MonsterDatabase monsterDatabase;
    public MoveDatabase moveDatabase;

    private SpeciesInfo playerMonster;
    private SpeciesInfo enemyMonster;

    public void StartBattle(Species playerSpecies, Species enemySpecies)
    {
        playerMonster = GetMonsterInfo(playerSpecies);
        enemyMonster = GetMonsterInfo(enemySpecies);

        Debug.Log($"Battle started between {playerMonster.name} and {enemyMonster.name}!");
    }

    public void PlayerAttack(Move playerMove)
    {
        MoveInformation moveInfo = GetMoveInfo(playerMove);
        int damage = CalculateDamage(playerMonster, enemyMonster, moveInfo);
        enemyMonster.baseHP -= damage;

        Debug.Log($"{playerMonster.name} used {moveInfo.name}! It dealt {damage} damage to {enemyMonster.name}.");
        CheckBattleOutcome();
    }

    public void EnemyAttack(Move enemyMove)
    {
        MoveInformation moveInfo = GetMoveInfo(enemyMove);
        int damage = CalculateDamage(enemyMonster, playerMonster, moveInfo);
        playerMonster.baseHP -= damage;

        Debug.Log($"{enemyMonster.name} used {moveInfo.name}! It dealt {damage} damage to {playerMonster.name}.");
        CheckBattleOutcome();
    }

    private SpeciesInfo GetMonsterInfo(Species species)
    {
        foreach (var monster in monsterDatabase.monsters)
        {
            if (monster.species == species)
            {
                return monster;
            }
        }
        return null;
    }

    private MoveInformation GetMoveInfo(Move move)
    {
        foreach (var moveInfo in moveDatabase.moves)
        {
            if (moveInfo.move == move)
            {
                return moveInfo;
            }
        }
        return null;
    }

    private int CalculateDamage(SpeciesInfo attacker, SpeciesInfo defender, MoveInformation move)
    {
        if (move == null || move.type == MoveType.NONE)
        {
            return 0;
        }

        int damage = move.damage + attacker.baseAttack_Melee - defender.baseDefense_Melee;
        return Mathf.Max(damage, 0); // Ensure damage is not negative
    }

    private void CheckBattleOutcome()
    {
        if (playerMonster.baseHP <= 0)
        {
            Debug.Log($"{playerMonster.name} has fainted! {enemyMonster.name} wins!");
        }
        else if (enemyMonster.baseHP <= 0)
        {
            Debug.Log($"{enemyMonster.name} has fainted! {playerMonster.name} wins!");
        }
    }
}
