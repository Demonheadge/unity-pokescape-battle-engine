// 13/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class TargetingSystem : MonoBehaviour
{
    public GameManager gameManager;
    public List<GameObject> enemyTeam;
    public List<GameObject> playerTeam;
    private List<GameObject> allTargets;
    public int currentTargetIndex = 0;
    private GameObject currentAttackingMonster;

    void Start()
    {
        //InitializeTargets();
    }

    public void InitializeTargets()
    {
        // Populate enemyTeam with Monster_GameObjects from spawnedEnemyMonsters
        foreach (Monster enemyMonster in gameManager.spawnedEnemyMonsters)
        {
            if (enemyMonster != null && enemyMonster.Monster_GameObject != null)
            {
                enemyTeam.Add(enemyMonster.Monster_GameObject);
            }
        }
        // Populate playerTeam with Monster_GameObjects from spawnedPlayerMonsters
        foreach (Monster playerMonster in gameManager.spawnedPlayerMonsters)
        {
            if (playerMonster != null && playerMonster.Monster_GameObject != null)
            {
                playerTeam.Add(playerMonster.Monster_GameObject);
            }
        }
        Debug.Log($"Initialized {enemyTeam.Count} enemyteam contains.");
        Debug.Log($"Initialized {playerTeam.Count} playerTeam contains.");

        allTargets = new List<GameObject>();
        allTargets.AddRange(enemyTeam);
        allTargets.AddRange(playerTeam);
        Debug.Log($"Initialized {allTargets.Count} all targets.");
    }

    public GameObject GetCurrentTarget()
    {
        if (allTargets.Count == 0)
        {
            Debug.LogWarning("No targets available!");
            return null;
        }

        // Return the current target based on the index
        return allTargets[currentTargetIndex];
    }

    public void SelectNextTarget()
    {
        if (allTargets.Count == 0)
        {
            Debug.LogWarning("No targets available to select!");
            return;
        }

        int initialIndex = currentTargetIndex;

        do
        {
            // Increment the target index and loop back if it exceeds the list size
            currentTargetIndex = (currentTargetIndex + 1) % allTargets.Count;

            // Break the loop if we have cycled through all targets
            if (currentTargetIndex == initialIndex)
            {
                Debug.LogWarning("No valid target found!");
                return;
            }
        }
        while (allTargets[currentTargetIndex] == currentAttackingMonster);
        HighlightSelectedTarget(currentTargetIndex);

        Debug.Log($"Selected target: {allTargets[currentTargetIndex].name}");
    }

    public void SelectPreviousTarget()
    {
        if (allTargets.Count == 0)
        {
            Debug.LogWarning("No targets available to select!");
            return;
        }

        int initialIndex = currentTargetIndex;

        do
        {
            // Decrement the target index and loop back if it goes below zero
            currentTargetIndex = (currentTargetIndex - 1 + allTargets.Count) % allTargets.Count;

            // Break the loop if we have cycled through all targets
            if (currentTargetIndex == initialIndex)
            {
                Debug.LogWarning("No valid target found!");
                return;
            }
        }
        while (allTargets[currentTargetIndex] == currentAttackingMonster);
        HighlightSelectedTarget(currentTargetIndex);

        Debug.Log($"Selected target: {allTargets[currentTargetIndex].name}");
    }

    public void SetCurrentAttackingMonster(GameObject attackingMonster)
    {
        currentAttackingMonster = attackingMonster;
    }

    public void SetTeams(List<GameObject> enemies, List<GameObject> players)
    {
        enemyTeam = enemies;
        playerTeam = players;
        InitializeTargets();
    }




    public void HighlightSelectedTarget(int selectedIndex)
    {
        // Reset the color of all targets to white
        HighlightClearTarget();

        // Update the current target index
        currentTargetIndex = selectedIndex;

        // Get the current target using GetCurrentTarget
        GameObject selectedTargetGameObject = GetCurrentTarget();

        if (selectedTargetGameObject != null)
        {
            SpriteRenderer spriteRenderer = selectedTargetGameObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red; // Highlight the selected target in red
                //Debug.Log($"Highlighted Target: {selectedTargetGameObject.name} is now highlighted in red.");
            }
            else
            {
                Debug.LogError("SpriteRenderer component is missing on the selected target's GameObject!");
            }
        }
        else
        {
            Debug.LogError("Target GameObject not found for the selected index!");
        }
    }

    public void HighlightClearTarget()
    {
        // Reset the color of all targets to white
        foreach (var targetGameObject in allTargets)
        {
            if (targetGameObject != null)
            {
                SpriteRenderer spriteRenderer = targetGameObject.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = Color.white; // Reset color to default (white)
                }
                else
                {
                    Debug.LogError("SpriteRenderer component is missing on the target's GameObject!");
                }
            }
        }
    }
}
