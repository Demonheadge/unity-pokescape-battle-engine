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

        // Add player's monsters to the turn order
        foreach (var playerMonster in gameManager.spawnedPlayerMonsters)
        {
            turnOrder.Add(playerMonster);
        }

        // Add enemy monsters to the turn order
        foreach (var enemyMonster in gameManager.spawnedEnemyMonsters)
        {
            turnOrder.Add(enemyMonster);
        }

        // Sort the turn order by speed in descending order
        turnOrder.Sort((a, b) => b.extra2Info.current_Speed.CompareTo(a.extra2Info.current_Speed));

        // Debugging: Print the turn order with positions
        Debug.Log("Turn Order:");
        for (int i = 0; i < turnOrder.Count; i++)
        {
            var entity = turnOrder[i];
            Debug.Log($"{i + 1}: {entity.speciesInfo.name} speed: {entity.extra2Info.current_Speed}");
        }
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

        //OPTION MENU CHOOSE AN OPTION
        yield return StartCoroutine(UI_OptionMenu(playerMonster));
        
        
    }

    
    private IEnumerator UI_OptionMenu(SpawnedMonster playerMonster)
    {
        Debug.Log("Choose an option.");
        uiController.variables.canPlayerInteract = true;
        uiController.BattleUI_OptionMenu.SetActive(true);   // Show Option menu

        // List of options in the menu
        List<string> options = new List<string> { "Fight", "Bag", "Team", "Flee" };
        int currentSelectionIndex = 0;

        // Highlight the first option by default
        uiController.HighlightOption(options[currentSelectionIndex], uiController.BattleUI_OptionMenu);

        // Wait for player input
        bool optionSelected = false;

        while (!optionSelected)
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                // Navigate up in the menu
                currentSelectionIndex = (currentSelectionIndex - 1 + options.Count) % options.Count;
                uiController.HighlightOption(options[currentSelectionIndex], uiController.BattleUI_OptionMenu);
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                // Navigate down in the menu
                currentSelectionIndex = (currentSelectionIndex + 1) % options.Count;
                uiController.HighlightOption(options[currentSelectionIndex], uiController.BattleUI_OptionMenu);
            }
            else if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                // Confirm selection
                optionSelected = true;
                // Hide the option menu and disable player interaction
                uiController.BattleUI_OptionMenu.SetActive(false);
                uiController.variables.canPlayerInteract = false;
            }

            yield return null;
        }

        

        // Execute the selected option
        switch (options[currentSelectionIndex])
        {
            case "Fight":
                yield return StartCoroutine(UI_FightMenu(playerMonster));
                //StartCoroutine(FIGHT_MENU_SELECTED(playerMonster));
                yield break;
            case "Bag":
                Debug.Log("Bag option selected.");
                // Implement Bag functionality here
                break;
            case "Team":
                Debug.Log("Team option selected.");
                // Implement Team functionality here
                break;
            case "Flee":
                Debug.Log("Flee option selected.");
                //gameManager.FleeBattle();
                yield break;
            default:
                Debug.LogWarning("Invalid option selected.");
                break;
        }

        yield break;
    }




    private IEnumerator UI_FightMenu(SpawnedMonster playerMonster)
    {
        Debug.Log("Choose a move.");
        uiController.variables.canPlayerInteract = true;
        uiController.BattleUI_FightMenu.SetActive(true);   // Show Option menu

        // List of options in the menu
        List<string> options = new List<string> { "MOVE_SLOT_1", "MOVE_SLOT_2", "MOVE_SLOT_3", "MOVE_SLOT_4" };
        int currentSelectionIndex = 0;

        PopulateFightMenu(playerMonster.extra1Info);
        gameManager.SetInitialTarget(); //Highlights the target.

        // Highlight the first option by default
        uiController.HighlightOption(options[currentSelectionIndex], uiController.BattleUI_FightMenu);

        // Wait for player input
        bool optionSelected = false;
        MoveInformation selectedMove = null;

        while (!optionSelected)
        {
            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                // Navigate up in the menu
                currentSelectionIndex = (currentSelectionIndex - 1 + options.Count) % options.Count;
                uiController.HighlightOption(options[currentSelectionIndex], uiController.BattleUI_FightMenu);
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                // Navigate down in the menu
                currentSelectionIndex = (currentSelectionIndex + 1) % options.Count;
                uiController.HighlightOption(options[currentSelectionIndex], uiController.BattleUI_FightMenu);
            }
            else if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                // Confirm selection
                selectedMove = uiController.GetSelectedMove();
                
                // Check if the selected move is not Move.None
                if (selectedMove != null && selectedMove.move != Move.NONE)
                {
                    optionSelected = true;  // Confirm selection
                }
                else
                {
                    Debug.LogWarning("Invalid move selected. Please choose a valid move.");
                }
            }
            else if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                uiController.BattleUI_FightMenu.SetActive(false);
                yield break;
            }

            yield return null;
        }

        uiController.BattleUI_FightMenu.SetActive(false);
        uiController.variables.canPlayerInteract = false;
        ExecuteMove(playerMonster, gameManager.selectedTargetIndex, selectedMove);
        yield break;
    }





















    /*private IEnumerator FIGHT_MENU_SELECTED(SpawnedMonster playerMonster)
    {
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

        // Execute the selected move using the combined list and selected target index
        ExecuteMove(playerMonster, gameManager.selectedTargetIndex, selectedMove);

        yield return new WaitForSeconds(1f); // Wait for animation or effects
    }*/

    





    /*private IEnumerator EnemyTurn(SpawnedMonster enemyMonster)
    {
        Debug.Log($"{enemyMonster.speciesInfo.species}'s turn!");

        // Randomly select a move
        MoveInformation selectedMove = SelectRandomMove(enemyMonster.extra1Info);

        // Execute the selected move
        ExecuteMove(enemyMonster, gameManager.playerParty[0], selectedMove);

        yield return new WaitForSeconds(1f); // Wait for animation or effects
    }*/

    private IEnumerator EnemyTurn(SpawnedMonster enemyMonster)
    {
        Debug.Log($"{enemyMonster.speciesInfo.species}'s turn!");

        // Wait for a short delay to simulate thinking time
        yield return new WaitForSeconds(1f);

        // Ensure there are valid targets in the spawnedPlayerMonsters list
        if (gameManager.spawnedPlayerMonsters.Count > 0)
        {
            // Randomly select a target from the spawnedPlayerMonsters list
            int randomIndex = Random.Range(0, gameManager.spawnedPlayerMonsters.Count);
            SpawnedMonster targetMonster = gameManager.spawnedPlayerMonsters[randomIndex];

            // Randomly select a move
            MoveInformation selectedMove = SelectRandomMove(enemyMonster.extra1Info);

            // Execute the move on the randomly selected target
            ExecuteMove(enemyMonster, randomIndex, selectedMove);

            Debug.Log($"{enemyMonster.speciesInfo.species} used {selectedMove.name} on {targetMonster.speciesInfo.species}!");
        }
        else
        {
            Debug.LogWarning("No player monsters available to attack!");
        }

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

    private void ExecuteMove(SpawnedMonster playerMonster, int selectedTargetIndex, MoveInformation selectedMove)
    {
        // Get the combined list of targets
        List<SpawnedMonster> combinedTargets = gameManager.GetCombinedTargetList();

        // Validate the selected target index
        if (selectedTargetIndex >= 0 && selectedTargetIndex < combinedTargets.Count)
        {
            // Get the target monster data from the combined list
            SpawnedMonster targetMonster = combinedTargets[selectedTargetIndex];

            // Execute the move on the selected target
            Debug.Log($"{playerMonster.speciesInfo.species} used {selectedMove.name} on {targetMonster.speciesInfo.species}!");

            int damage = selectedMove.damage;
            int previousHP = targetMonster.extra2Info.current_HP;
            targetMonster.extra2Info.current_HP -= damage;
            targetMonster.extra2Info.current_HP = Mathf.Clamp(targetMonster.extra2Info.current_HP, 0, targetMonster.extra2Info.max_HP);

            Debug.Log($"{targetMonster.speciesInfo.species} took {damage} damage! Remaining HP: {targetMonster.extra2Info.current_HP}");

            // Check if the target is defeated
            if (targetMonster.extra2Info.current_HP <= 0)
            {
                Debug.Log($"{targetMonster.speciesInfo.species} fainted!");

                // Remove the defeated monster from the respective list
                if (gameManager.spawnedEnemyMonsters.Contains(targetMonster))
                {
                    gameManager.spawnedEnemyMonsters.Remove(targetMonster);
                }
                else if (gameManager.spawnedPlayerMonsters.Contains(targetMonster))
                {
                    gameManager.spawnedPlayerMonsters.Remove(targetMonster);
                }

                // Check if the battle should end
                gameManager.CheckIfEndBattle();
            }
        }
        else
        {
            Debug.LogWarning("Invalid target index! No move executed.");
        }
    }

    /*private void ExecuteMove(SpawnedMonster attacker, SpawnedMonster target, MoveInformation moveInfo)
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
    }*/


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