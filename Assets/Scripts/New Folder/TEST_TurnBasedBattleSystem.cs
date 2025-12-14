// 14/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using System.Collections;


public class TEST_TurnBasedBattleSystem : MonoBehaviour
{
    public GameManager gameManager;
    public UI_Controller uiController;
    public TargetingSystem targetingSystem;

    public List<Monster> turnOrder; // List of monsters sorted by speed for turn order
    private int currentTurnIndex = 0; // Index of the current monster's turn



    public void BeginTurns()
    {
        gameManager.music_Manager.PlayMusic(gameManager.music_Manager.selectedBattleMusic); //Play Battle music.
        // Initialize the turn order based on speed stats
        
        InitializeTurnOrder();
        targetingSystem.InitializeTargets();
        StartCoroutine(HandleTurns());
    }

    public void InitializeTurnOrder()
    {
        //Removes previous battle history if there is any.
        turnOrder.Clear();

        // Combine all monsters on the field into a single list
        List<Monster> allMonsters = new List<Monster>();
        allMonsters.AddRange(gameManager.spawnedEnemyMonsters);
        allMonsters.AddRange(gameManager.spawnedPlayerMonsters);

        // Sort monsters by current_speed in descending order (higher speed goes first)
        turnOrder = allMonsters.OrderByDescending(monster => monster.monsterStatistics.current_Speed).ToList();

        Debug.Log("Turn order initialized:");
        foreach (var monster in turnOrder)
        {
            Debug.Log($"{monster.monsterSpeciesInfo.SPECIES} - Speed: {monster.monsterStatistics.current_Speed}");
        }
    }


    private IEnumerator HandleTurns()
    {
        while (gameManager.variables.isInABattle)
        {
            if (turnOrder.Count == 0)
            {
                Debug.LogError("ERROR, Turn order is empty! Ending battle.");
                gameManager.variables.isInABattle = false;
                gameManager.EndBattle();
                yield break;
            }

            // Get the current monster whose turn it is
            Monster currentMonster = turnOrder[currentTurnIndex];

            // Set the current attacking monster in the targeting system
            targetingSystem.SetCurrentAttackingMonster(currentMonster.Monster_GameObject);

            // Check if the current monster is an enemy or player
            if (gameManager.spawnedEnemyMonsters.Contains(currentMonster))
            {
                //EnemyAttack(currentMonster);
                yield return StartCoroutine(EnemyTurn(currentMonster));
            }
            else if (gameManager.spawnedPlayerMonsters.Contains(currentMonster))
            {
                //PlayerAttack(currentMonster);
                yield return StartCoroutine(PlayerTurn(currentMonster));
            }

            // Move to the next turn
            currentTurnIndex++;
            if (currentTurnIndex >= turnOrder.Count)
            {
                // Reset turn index for the next round
                currentTurnIndex = 0;
                Debug.Log("New round begins!");
            }
        }
    }




    private IEnumerator PlayerTurn(Monster playerMonster)
    {
        Debug.Log("Player's turn!");

        //OPTION MENU CHOOSE AN OPTION
        yield return StartCoroutine(UI_OptionMenu(playerMonster));
    }

    private IEnumerator EnemyTurn(Monster attackingMonster)
    {
        Debug.Log($"{attackingMonster.monsterSpeciesInfo.SPECIES}'s turn!");

        //Enemy Chooses a Target
        Monster targetMonster = EnemySelectsATarget(attackingMonster);

        // Randomly select a move
        MoveInformation selectedMove = SelectRandomMove(attackingMonster.monsterMoves);

        //Attacks the target
        Execute_Move(attackingMonster, targetMonster, selectedMove);

        // Check if the target is defeated
        CheckIfTargetIsDefeated(targetMonster);

        yield return new WaitForSeconds(1f); // Wait for animation or effects
    }

    private Monster EnemySelectsATarget(Monster attackingMonster)
    {
        // Randomly select a target from the player team
        if (gameManager.spawnedPlayerMonsters.Count == 0)
        {
            Debug.LogWarning("No player monsters available to attack!");
            return null;
        }

        Monster targetMonster = gameManager.spawnedPlayerMonsters[UnityEngine.Random.Range(0, gameManager.spawnedPlayerMonsters.Count)];

        return targetMonster;
    }

    private void Execute_Move(Monster attackingMonster, Monster targetMonster, MoveInformation selectedMove)
    {
        //TODO: redo calcs for move damage instead.
        // Perform the attack (example: reduce target's HP)
        int damage = CalculateDamage(attackingMonster, targetMonster);
        targetMonster.monsterStatistics.current_HP -= damage;

        Debug.Log($"{attackingMonster.monsterSpeciesInfo.SPECIES} used {selectedMove.name} on {targetMonster.monsterSpeciesInfo.SPECIES} for {damage} damage!");
    }

    private void CheckIfTargetIsDefeated(Monster targetMonster)
    {
        if (targetMonster.monsterStatistics.current_HP <= 0)
        {
            Debug.Log($"{targetMonster.monsterSpeciesInfo.SPECIES} has been defeated!");

            // Check if the current monster is an enemy or player
            if (gameManager.spawnedEnemyMonsters.Contains(targetMonster))
            {
                gameManager.spawnedEnemyMonsters.Remove(targetMonster);
            }
            else if (gameManager.spawnedPlayerMonsters.Contains(targetMonster))
            {
                gameManager.spawnedPlayerMonsters.Remove(targetMonster);
            }

            Destroy(targetMonster.Monster_GameObject);
        }

        //check if all team side monsters are defeated, if so end battle.
    }





    





































    








//TODO: REMOVE
    private int CalculateDamage(Monster attacker, Monster target)
    {
        // Example damage calculation based on attack and defense stats
        int attackPower = attacker.monsterStatistics.current_Attack_Melee; // Example: using melee attack
        int defensePower = target.monsterStatistics.current_Defense_Melee; // Example: using melee defense

        int damage = Mathf.Max(1, attackPower - defensePower); // Ensure damage is at least 1
        return damage;
    }










    private IEnumerator UI_OptionMenu(Monster playerMonster)
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

    private IEnumerator UI_FightMenu(Monster playerMonster)
    {
        Debug.Log("Choose a move.");
        uiController.variables.canPlayerInteract = true;
        uiController.BattleUI_FightMenu.SetActive(true);   // Show Option menu
        
        // List of options in the menu
        List<string> options = new List<string> { "MOVE_SLOT_1", "MOVE_SLOT_2", "MOVE_SLOT_3", "MOVE_SLOT_4" };
        int currentSelectionIndex = 0;

        uiController.PopulateFightMenu(playerMonster.monsterMoves);
        targetingSystem.HighlightSelectedTarget(targetingSystem.currentTargetIndex); //Highlights the last selected target.

        // Highlight the first option by default
        
        uiController.HighlightOption(options[currentSelectionIndex], uiController.BattleUI_FightMenu);
        uiController.HighlightLastMove(playerMonster);

        // Wait for player input
        bool optionSelected = false;
        MoveInformation selectedMove = null;

        while (!optionSelected)
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
            else if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                // Confirm selection
                selectedMove = uiController.GetSelectedMove();
                
                // Check if the selected move is not Move.None
                if (selectedMove != null && selectedMove.move != Move.NONE)
                {
                    optionSelected = true;  // Confirm selection
                    // Remember the last move index for the current monster
                    uiController.RememberLastMove(playerMonster, uiController.selectedMoveIndex);

                    //Debug.LogWarning($"{selectedMove.move}");
                }
                else
                {
                    Debug.LogWarning("Invalid move selected. Please choose a valid move.");
                }
            }
            else if (Keyboard.current.sKey.wasPressedThisFrame )//&& gameManager.variables.canPlayerInteract)
            {
                gameManager.targetingSystem.SelectNextTarget();
            }
            else if (Keyboard.current.aKey.wasPressedThisFrame )//&& gameManager.variables.canPlayerInteract)
            {
                gameManager.targetingSystem.SelectPreviousTarget();
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

        //Player Attacks the target
        PlayerAttacks(playerMonster, selectedMove);

        yield break;
    }

    private void PlayerAttacks(Monster attackingMonster, MoveInformation selectedMove)
    {
        targetingSystem.HighlightClearTarget();
        // Get the current target from the targeting system
        GameObject targetObject = targetingSystem.GetCurrentTarget();

        if (targetObject == null)
        {
            Debug.LogWarning("No target selected for player attack!");
            return;
        }

        // Get the Test_Monster_Controller of the target
        Test_Monster_Controller targetController = targetObject.GetComponent<Test_Monster_Controller>();

        if (targetController != null)
        {
            Monster targetMonster = targetController.monsterData;

            //Attacks the target
            Execute_Move(attackingMonster, targetMonster, selectedMove);

            // Check if the target is defeated
            CheckIfTargetIsDefeated(targetMonster);
        }
    }


    private MoveInformation SelectRandomMove(Monster.Moves monsterMoves)
    {
        List<MoveInformation> moves = new List<MoveInformation>
        {
            uiController.GetMoveInformation(monsterMoves.MOVE_1),
            uiController.GetMoveInformation(monsterMoves.MOVE_2),
            uiController.GetMoveInformation(monsterMoves.MOVE_3),
            uiController.GetMoveInformation(monsterMoves.MOVE_4)
        };
        // Remove null moves and moves with Move.None
        moves.RemoveAll(move => move == null || move.move == Move.NONE);

        if (moves.Count == 0)
        {
            Debug.LogError("No valid moves available for enemy!");
            return null;
        }
        return moves[UnityEngine.Random.Range(0, moves.Count)];
    }

}
