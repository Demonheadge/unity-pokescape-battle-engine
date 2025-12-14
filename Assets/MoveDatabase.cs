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

    // Add the ApplyMoveEffects method
    public void ApplyMoveEffects(ExtendedSpawnedMonster attacker, ExtendedSpawnedMonster target, MoveInformation move)
    {
        if (attacker == null || target == null || move == null)
        {
            Debug.LogError("Invalid parameters passed to ApplyMoveEffects.");
            return;
        }

        // Example: Apply damage to the target
        int damage = move.damage; // Assuming MoveInformation has a 'damage' property
        target.TakeDamage(damage);

        Debug.Log($"{attacker.speciesInfo.species} used {move.name} on {target.speciesInfo.species}, dealing {damage} damage.");
    }
}