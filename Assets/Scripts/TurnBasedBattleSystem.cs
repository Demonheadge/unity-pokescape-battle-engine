// 5/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;


public class TurnBasedBattleSystem : MonoBehaviour
{
    public GameManager gameManager;
    public UI_Controller uiController;
    public MoveDatabase moveDatabase;
    

    private List<SpawnedMonster> turnOrder = new List<SpawnedMonster>();
    private int currentTurnIndex = 0;

    public void BeginTurns()
    {
        // Initialize the turn order based on speed stats
        InitializeTurnOrder();
        StartCoroutine(HandleTurn());
    }

    private void InitializeTurnOrder()
    {
        turnOrder.Clear();

        // Add player's monster to the turn order
        if (gameManager.playerParty.Count > 0)
        {
            turnOrder.Add(gameManager.playerParty[0]);
        }

        // Add enemy monsters to the turn order
        foreach (var enemy in gameManager.spawnedEnemies)
        {
            turnOrder.Add(enemy.monsterData);
        }

        // Sort the turn order by speed in descending order
        turnOrder.Sort((a, b) => b.extra2Info.current_Speed.CompareTo(a.extra2Info.current_Speed));
    }

    private IEnumerator HandleTurn()
    {
        while (gameManager.variables.isInABattle)
        {
            // Ensure turnOrder is not empty
            if (turnOrder.Count == 0)
            {
                Debug.LogError("Turn order is empty! Ending battle.");
                gameManager.variables.isInABattle = false;
                yield break; // Exit the coroutine
            }

            // Ensure currentTurnIndex is within bounds
            if (currentTurnIndex < 0 || currentTurnIndex >= turnOrder.Count)
            {
                Debug.LogError($"Invalid currentTurnIndex: {currentTurnIndex}. Resetting to 0.");
                currentTurnIndex = 0; // Reset to a valid index
            }

            SpawnedMonster currentMonster = turnOrder[currentTurnIndex];

            if (gameManager.playerParty.Contains(currentMonster))
            {
                // Player's turn
                yield return StartCoroutine(PlayerTurn(currentMonster));
            }
            else
            {
                // Enemy's turn
                yield return StartCoroutine(EnemyTurn(currentMonster));
            }

            // Move to the next turn
            currentTurnIndex = (currentTurnIndex + 1) % turnOrder.Count;
        }
    }

    private IEnumerator PlayerTurn(SpawnedMonster playerMonster)
    {
        Debug.Log("Player's turn!");

        uiController.variables.canPlayerInteract = true;
        Debug.Log("CanInteract: " + uiController.variables.canPlayerInteract);

        // Show fight menu
        uiController.BattleUI_FightMenu.SetActive(true);

        // Populate fight menu with player's monster moves
        PopulateFightMenu(playerMonster.extra1Info);

        //Highlights the target.
        gameManager.SetInitialTarget();

        // Wait for player input
        bool moveSelected = false;
        MoveInformation selectedMove = null;

        while (!moveSelected)
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                // Navigate up in the menu
                uiController.NavigateFightMenu(-1);
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                // Navigate down in the menu
                uiController.NavigateFightMenu(1);
            }
            else if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                // Confirm selection
                selectedMove = uiController.GetSelectedMove();
                
                // Check if the selected move is not Move.None
                if (selectedMove != null && selectedMove.move != Move.NONE)
                {
                    moveSelected = true;
                }
                else
                {
                    Debug.LogWarning("Invalid move selected. Please choose a valid move.");
                }
            }

            yield return null;
        }

        // Hide fight menu
        uiController.BattleUI_FightMenu.SetActive(false);

        // Execute the selected move
        ExecuteMove(playerMonster, gameManager.spawnedEnemies[gameManager.selectedTargetIndex].monsterData, selectedMove);

        yield return new WaitForSeconds(1f); // Wait for animation or effects
    }

    private IEnumerator EnemyTurn(SpawnedMonster enemyMonster)
    {
        Debug.Log($"{enemyMonster.speciesInfo.species}'s turn!");

        // Randomly select a move
        MoveInformation selectedMove = SelectRandomMove(enemyMonster.extra1Info);

        // Execute the selected move
        ExecuteMove(enemyMonster, gameManager.playerParty[0], selectedMove);

        yield return new WaitForSeconds(1f); // Wait for animation or effects
    }

    private void PopulateFightMenu(Extra_1_Monster_Info extra1Info)
    {
        // Assign move information to each fight menu slot
        AssignMoveToSlot(uiController.FightMenu_Move_Slot_1, GetMoveInformation(extra1Info.move_1));
        AssignMoveToSlot(uiController.FightMenu_Move_Slot_2, GetMoveInformation(extra1Info.move_2));
        AssignMoveToSlot(uiController.FightMenu_Move_Slot_3, GetMoveInformation(extra1Info.move_3));
        AssignMoveToSlot(uiController.FightMenu_Move_Slot_4, GetMoveInformation(extra1Info.move_4));
    }


    private MoveInformation SelectRandomMove(Extra_1_Monster_Info extra1Info)
    {
        List<MoveInformation> moves = new List<MoveInformation>
        {
            GetMoveInformation(extra1Info.move_1),
            GetMoveInformation(extra1Info.move_2),
            GetMoveInformation(extra1Info.move_3),
            GetMoveInformation(extra1Info.move_4)
        };
        // Remove null moves and moves with Move.None
        moves.RemoveAll(move => move == null || move.move == Move.NONE);

        if (moves.Count == 0)
        {
            Debug.LogError("No valid moves available for enemy!");
            return null;
        }
        return moves[Random.Range(0, moves.Count)];
    }

    private void ExecuteMove(SpawnedMonster attacker, SpawnedMonster target, MoveInformation moveInfo)
    {
        Debug.Log($"{attacker.speciesInfo.species} used {moveInfo.name}!");

        // Calculate damage based on move data and attacker/target stats
        int damage = moveInfo.damage;
        int previousHP = target.extra2Info.current_HP;
        target.extra2Info.current_HP -= damage;
        target.extra2Info.current_HP = Mathf.Clamp(target.extra2Info.current_HP, 0, target.extra2Info.max_HP);

        Debug.Log($"{target.speciesInfo.species} took {damage} damage! Remaining HP: {target.extra2Info.current_HP}");

        // Find the GameObject associated with the target SpawnedMonster
        GameObject targetGameObject = null;

        // Check in playerParty list
        foreach (var playerMonster in gameManager.playerParty)
        {
            if (playerMonster == target)
            {
                //targetGameObject = playerMonster.GetComponent<MonsterController>();
                break;
            }
        }

        // If not found in playerParty, check in spawnedEnemies list
        if (targetGameObject == null)
        {
            foreach (var enemy in gameManager.spawnedEnemies)
            {
                if (enemy.monsterData == target)
                {
                    targetGameObject = enemy.gameObject;
                    break;
                }
            }
        }

        if (targetGameObject == null)
        {
            Debug.LogError("Target GameObject not found for the given SpawnedMonster!");
            return;
        }

        // Get the MonsterController component from the target's GameObject
        MonsterController monsterController = targetGameObject.GetComponent<MonsterController>();
        if (monsterController != null && monsterController.Info_Bar_Updater != null)
        {
            // Call AnimateHealthBarAndText using the Info_Bar_Updater reference
            StartCoroutine(monsterController.Info_Bar_Updater.AnimateHealthBarAndText(target, previousHP, target.extra2Info.current_HP));
        }
        else
        {
            Debug.LogError("Info_Bar_Updater component is missing or MonsterController is not set up correctly!");
        }

        // Check if the target is defeated
        if (target.extra2Info.current_HP <= 0)
        {
            Debug.Log($"{target.speciesInfo.species} fainted!");
            if (gameManager.spawnedEnemies.Exists(e => e.monsterData == target))
            {
                gameManager.RemoveEnemy(gameManager.spawnedEnemies.Find(e => e.monsterData == target));
                gameManager.CheckIfEndBattle();
            }
            else if (gameManager.playerParty.Contains(target))
            {
                gameManager.playerParty.Remove(target);
                gameManager.CheckIfEndBattle();
            }
        }
    }


    private MoveInformation GetMoveInformation(Move move)
    {
        if (moveDatabase == null)
        {
            Debug.LogError("MoveDatabase is not assigned in TurnBasedBattleSystem!");
            return null;
        }

        if (moveDatabase.moves == null || moveDatabase.moves.Count == 0)
        {
            Debug.LogError("MoveDatabase does not contain any moves!");
            return null;
        }

        foreach (MoveInformation moveInfo in moveDatabase.moves)
        {
            if (moveInfo.move == move)
            {
                return moveInfo;
            }
        }

        Debug.LogError($"Move {move} not found in the MoveDatabase!");
        return null;
    }

    private void AssignMoveToSlot(GameObject slot, MoveInformation moveInfo)
    {
        // Assign the move information to the MoveSlot component
        MoveSlot moveSlot = slot.GetComponent<MoveSlot>();
        if (moveSlot != null)
        {
            moveSlot.moveInfo = moveInfo;

            // Update the TextMeshProUGUI text with the move name
            TextMeshProUGUI slotText = slot.GetComponentInChildren<TextMeshProUGUI>();
            if (slotText != null && moveInfo != null)
            {
                if (moveInfo.move != Move.NONE)
                {
                    slotText.text = moveInfo.name;
                }
                else
                {
                    slotText.text = "Unavailable"; // Display "Unavailable" for Move.None
                    moveSlot.moveInfo = null; // Prevent selection of this move
                }
            }
            else
            {
                Debug.LogError("Move slot does not have a TextMeshProUGUI component or moveInfo is null!");
            }
        }
        else
        {
            Debug.LogError("MoveSlot component is missing on the fight menu slot!");
        }
    }
}