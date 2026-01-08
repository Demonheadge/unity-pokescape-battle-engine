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
    public GameObject currentAttackingMonster;
    public GameObject currentDefendingMonster;
    

    void Start()
    {
        //InitializeTargets();
    }

    public void InitializeTargets()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is null! Cannot initialize targets.");
            return;
        }

        if (gameManager.spawnedEnemyMonsters == null || gameManager.spawnedPlayerMonsters == null)
        {
            Debug.LogError("Spawned monsters lists are null! Ensure they are initialized in GameManager.");
            return;
        }

        enemyTeam.Clear();
        playerTeam.Clear();
        allTargets = new List<GameObject>();

        foreach (Monster enemyMonster in gameManager.spawnedEnemyMonsters)
        {
            if (enemyMonster != null && enemyMonster.Monster_GameObject != null)
            {
                enemyTeam.Add(enemyMonster.Monster_GameObject);
            }
        }

        foreach (Monster playerMonster in gameManager.spawnedPlayerMonsters)
        {
            if (playerMonster != null && playerMonster.Monster_GameObject != null)
            {
                playerTeam.Add(playerMonster.Monster_GameObject);
            }
        }

        allTargets.AddRange(enemyTeam);
        allTargets.AddRange(playerTeam);

        Debug.Log($"Initialized {enemyTeam.Count} enemy team targets.");
        Debug.Log($"Initialized {playerTeam.Count} player team targets.");
        Debug.Log($"Initialized {allTargets.Count} total targets.");
    }

    public GameObject GetCurrentTarget()
    {
        if (allTargets == null || allTargets.Count == 0)
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
    public void SetCurrentDefendingMonster(GameObject defendingMonster)
    {
        currentDefendingMonster = defendingMonster;
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
                Debug.Log($"Highlighted Target: {selectedTargetGameObject.name} is now highlighted in red.");
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
        // Check if allTargets is null or empty
        if (allTargets == null || allTargets.Count == 0)
        {
            Debug.LogWarning("No targets available to clear highlights!");
            return;
        }

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
            else
            {
                Debug.LogWarning("Target GameObject is null!");
            }
        }
    }
}
