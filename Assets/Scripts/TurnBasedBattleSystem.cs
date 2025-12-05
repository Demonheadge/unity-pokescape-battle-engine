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
            turnOrder.Add(enemy.enemyData);
        }

        // Sort the turn order by speed in descending order
        turnOrder.Sort((a, b) => b.extra2Info.current_Speed.CompareTo(a.extra2Info.current_Speed));
    }

    private IEnumerator HandleTurn()
    {
        while (gameManager.variables.isInABattle)
        {
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
                moveSelected = true;
            }

            yield return null;
        }

        // Hide fight menu
        uiController.BattleUI_FightMenu.SetActive(false);

        // Execute the selected move
        ExecuteMove(playerMonster, gameManager.spawnedEnemies[gameManager.selectedTargetIndex].enemyData, selectedMove);

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
        TextMeshProUGUI[] texts = uiController.BattleUI_FightMenu.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            switch (text.name)
            {
                case "MOVE_SLOT_1":
                    text.text = extra1Info.move_1.ToString();
                    break;
                case "MOVE_SLOT_2":
                    text.text = extra1Info.move_2.ToString();
                    break;
                case "MOVE_SLOT_3":
                    text.text = extra1Info.move_3.ToString();
                    break;
                case "MOVE_SLOT_4":
                    text.text = extra1Info.move_4.ToString();
                    break;
            }
        }

        // Populate fight menu slots with move data
        //PopulateMoveSlot(uiController.FightMenu_Move_Slot_1, ConvertMoveToMoveInformation(extra1Info.move_1));
        //PopulateMoveSlot(uiController.FightMenu_Move_Slot_2, ConvertMoveToMoveInformation(extra1Info.move_2));
        //PopulateMoveSlot(uiController.FightMenu_Move_Slot_3, ConvertMoveToMoveInformation(extra1Info.move_3));
        //PopulateMoveSlot(uiController.FightMenu_Move_Slot_4, ConvertMoveToMoveInformation(extra1Info.move_4));
    }

    /*private void PopulateMoveSlot(GameObject slot, MoveInformation moveInfo)
    {
        Text slotText = slot.GetComponentInChildren<Text>();
        if (slotText != null && moveInfo != null)
        {
            slotText.text = moveInfo.name;
        }
        else
        {
            Debug.LogError("Move slot does not have a Text component or moveInfo is null!");
        }
    }*/

    private MoveInformation SelectRandomMove(Extra_1_Monster_Info extra1Info)
    {
        List<MoveInformation> moves = new List<MoveInformation>
        {
            ConvertMoveToMoveInformation(extra1Info.move_1),
            ConvertMoveToMoveInformation(extra1Info.move_2),
            ConvertMoveToMoveInformation(extra1Info.move_3),
            ConvertMoveToMoveInformation(extra1Info.move_4)
        };
        moves.RemoveAll(move => move == null); // Remove null moves
        return moves[Random.Range(0, moves.Count)];
    }

    private void ExecuteMove(SpawnedMonster attacker, SpawnedMonster target, MoveInformation moveInfo)
    {
        Debug.Log($"{attacker.speciesInfo.species} used {moveInfo.name}!");

        // Calculate damage based on move data and attacker/target stats
        int damage = moveInfo.damage;
        target.extra2Info.current_HP -= damage;

        Debug.Log($"{target.speciesInfo.species} took {damage} damage! Remaining HP: {target.extra2Info.current_HP}");

        // Check if the target is defeated
        if (target.extra2Info.current_HP <= 0)
        {
            Debug.Log($"{target.speciesInfo.species} fainted!");
            if (gameManager.spawnedEnemies.Exists(e => e.enemyData == target))
            {
                gameManager.RemoveEnemy(gameManager.spawnedEnemies.Find(e => e.enemyData == target));
            }
            else if (gameManager.playerParty.Contains(target))
            {
                gameManager.playerParty.Remove(target);
                gameManager.CheckIfEndBattle();
            }
        }
    }

    private MoveInformation ConvertMoveToMoveInformation(Move move)
    {
        // Create a new MoveInformation object based on the Move enum
        return new MoveInformation
        {
            move = move,
            name = move.ToString(), // Use the enum name as the move name
            type = MoveType.NONE, // Default type, update as needed
            effect = MoveEffect.None, // Default effect, update as needed
            damage = 0, // Default damage, update as needed
            accuracy = 100, // Default accuracy, update as needed
            effectSecondary = 0, // Default secondary effect, update as needed
            catagory = MoveCatagory.STATUS // Default category, update as needed
        };
    }
}