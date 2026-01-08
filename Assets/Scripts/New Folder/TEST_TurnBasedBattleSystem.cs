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

    public string currentAnimation;



    public void BeginTurns()
    {
        gameManager.music_Manager.PlayMusic(gameManager.music_Manager.selectedBattleMusic); // Play Battle music.

        if (targetingSystem != null)
        {
            targetingSystem.gameManager = gameManager; // Assign gameManager reference
            targetingSystem.InitializeTargets();
        }
        else
        {
            Debug.LogError("TargetingSystem is not assigned!");
            return;
        }

        StartCoroutine(HandleMonsterDecisions());
    }

    

    private IEnumerator HandleMonsterDecisions()
    {
        // Reset all monsters' HasChosenAction to false for the next turn
        foreach (var monster in turnOrder)
        {
            Test_Monster_Controller monsterController = monster.Monster_GameObject.GetComponent<Test_Monster_Controller>();
            monsterController.SetHasChosenAction(false);
        }

        Debug.Log("Gathering inputs/decisions for all monsters...");

        // Gather decisions for player's monsters
        foreach (var playerMonster in gameManager.spawnedPlayerMonsters)
        {
            yield return StartCoroutine(GetPlayerMonsterDecision(playerMonster));
        }

        // Gather decisions for enemy monsters
        foreach (var enemyMonster in gameManager.spawnedEnemyMonsters)
        {
            yield return StartCoroutine(GetEnemyMonsterDecision(enemyMonster));
        }

        // Step 2: Initialize the turn order based on speed stats
        InitializeTurnOrder();

        // Step 3: Initialize targeting system
        targetingSystem.InitializeTargets();

        // Step 4: Execute turns
        StartCoroutine(HandleTurns());
    }

    private IEnumerator GetPlayerMonsterDecision(Monster playerMonster)
    {
        Debug.Log($"Waiting for player to choose action for {playerMonster.monsterSpeciesInfo.SPECIES}...");

        // Assign the current attacking monster in the targeting system
        targetingSystem.SetCurrentAttackingMonster(playerMonster.Monster_GameObject);

        // Wait for player input (e.g., selecting a move, swapping, etc.)
        Test_Monster_Controller monsterController = playerMonster.Monster_GameObject.GetComponent<Test_Monster_Controller>();
        while (!monsterController.HasChosenAction)
        {
            yield return StartCoroutine(PlayerTurn(playerMonster));
            yield return null; // Wait until the player has chosen an action
        }

        Debug.Log($"{playerMonster.monsterSpeciesInfo.SPECIES} has chosen its action.");
    }

    private IEnumerator GetEnemyMonsterDecision(Monster enemyMonster)
    {
        Debug.Log($"Enemy is deciding action for {enemyMonster.monsterSpeciesInfo.SPECIES}...");

        // Assign the current attacking monster in the targeting system
        targetingSystem.SetCurrentAttackingMonster(enemyMonster.Monster_GameObject);

        // Simulate enemy decision-making (e.g., AI logic for choosing a move)
        yield return new WaitForSeconds(1f); // Simulate delay for enemy decision-making
        Test_Monster_Controller monsterController = enemyMonster.Monster_GameObject.GetComponent<Test_Monster_Controller>();
        monsterController.ChooseAction(enemyMonster); // Call a method to set the enemy's action

        Debug.Log($"{enemyMonster.monsterSpeciesInfo.SPECIES} has chosen its action.");
    }



    public void InitializeTurnOrder()
    {
        turnOrder.Clear();

        // Combine all monsters on the field into a single list
        List<Monster> allMonsters = new List<Monster>();
        allMonsters.AddRange(gameManager.spawnedEnemyMonsters);
        allMonsters.AddRange(gameManager.spawnedPlayerMonsters);

        // Sort monsters by current_speed in descending order (higher speed goes first)
        turnOrder = allMonsters
            .Where(monster => monster.monsterStatistics.current_HP > 0) // Only include monsters that are alive
            .OrderByDescending(monster => monster.monsterStatistics.current_Speed)
            .ToList();

        Debug.Log("Turn order initialized:");
        foreach (var monster in turnOrder)
        {
            Debug.Log($"{monster.monsterSpeciesInfo.SPECIES} - Speed: {monster.monsterStatistics.current_Speed}");
        }
    }

    private IEnumerator HandleTurns()
    {
        Debug.Log("Executing turns...");

        foreach (var monster in turnOrder)
        {
            // Skip monsters that are defeated
            if (monster.monsterStatistics.current_HP <= 0)
            {
                continue;
            }

            // Assign the current attacking monster in the targeting system
            targetingSystem.SetCurrentAttackingMonster(monster.Monster_GameObject);

            // Execute the monster's chosen action
            yield return StartCoroutine(ExecuteMonsterAction(monster));
        }

        Debug.Log("All turns executed. Next turn!");
        StartCoroutine(HandleMonsterDecisions());
    }

    private IEnumerator ExecuteMonsterAction(Monster monster)
    {
        Debug.Log($"Executing action for {monster.monsterSpeciesInfo.SPECIES}...");
        

        Test_Monster_Controller monsterController = monster.Monster_GameObject.GetComponent<Test_Monster_Controller>();
        // Check if the monster is swapping out
        if (monsterController.IsSwappingOut)
        {
            Debug.Log($"{monster.monsterSpeciesInfo.SPECIES} is swapping out...");
            Monster nextMonster = GetNextAvailablePlayerMonster();
            if (nextMonster != null)
            {
                gameManager.Init_BattleSetup_SendOutPlayerMonsters();
            }
            yield break; // Skip further actions for this monster
        }

        // Execute the monster's chosen move
        //yield return monster.ExecuteChosenMove();
        Debug.LogWarning($"TODO: {monster.monsterSpeciesInfo.SPECIES} IS NOW ATTACKING! (IF THE ATTACK SCRIPT WORKED).");
        

        Debug.Log($"{monster.monsterSpeciesInfo.SPECIES} has completed its turn.");
    }





    /*private IEnumerator HandleTurns()
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
    }*/



    




    

    private IEnumerator EnemyTurn(Monster attackingMonster)
    {
        Debug.Log($"{attackingMonster.monsterSpeciesInfo.SPECIES}'s turn!");
        Anim_StartTurn_Bop(attackingMonster);

        //Enemy Chooses a Target
        Monster targetMonster = EnemySelectsATarget(attackingMonster);

        // Randomly select a move
        MoveInformation selectedMove = SelectRandomMove(attackingMonster.monsterMoves);

        yield return new WaitForSeconds(2f);

        Anim_EndTurn_Bop(attackingMonster);

        //Attacks the target
        Execute_Move(attackingMonster, targetMonster, selectedMove);

        // Check if the target is defeated
        CheckIfTargetIsDefeated(targetMonster);

        

        yield return new WaitForSeconds(2f); // Wait for animation or effects
    }

    private Monster EnemySelectsATarget(Monster attackingMonster)   //Current issue: All enemies attack the same target until the next turn.
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
        targetingSystem.SetCurrentDefendingMonster(targetMonster.Monster_GameObject);

        StartCoroutine(Anim_PerformAttackAnimation(targetMonster));



        //TODO: redo calcs for move damage instead.
        // Perform the attack (example: reduce target's HP)
        int damage = CalculateDamage(attackingMonster, targetMonster);
        targetMonster.monsterStatistics.current_HP -= damage;

        Debug.Log($"{attackingMonster.monsterSpeciesInfo.SPECIES} used {selectedMove.name} on {targetMonster.monsterSpeciesInfo.SPECIES} for {damage} damage!");
        
        StartCoroutine(Anim_PerformHitReactionAnimation(targetMonster));
    }

    
    public void CheckIfTargetIsDefeated(Monster targetMonster)
    {
        if (targetMonster.monsterStatistics.current_HP <= 0)
        {
            Debug.Log($"{targetMonster.monsterSpeciesInfo.SPECIES} has been defeated!");

            // Check if the current monster is an enemy or player
            if (gameManager.spawnedEnemyMonsters.Contains(targetMonster))
            {
                /*// Set the monster to inactive in battle
                Test_Monster_Controller monsterGameObject = targetMonster.Monster_GameObject.GetComponent<Test_Monster_Controller>();
                if (monsterGameObject != null)
                {
                    monsterGameObject.isActiveInBattle = false;
                }*/
                //Removes the defeated monster from battle.
                gameManager.spawnedEnemyMonsters.Remove(targetMonster);
                Destroy(targetMonster.Monster_GameObject);

                if (CheckNextAvailableMonster(gameManager.trainerParty))
                {
                    gameManager.Init_BattleSetup_SendOutPlayerMonsters();
                }
                else
                {
                    gameManager.CheckIfEndBattle();
                }
            }
            else if (gameManager.spawnedPlayerMonsters.Contains(targetMonster))
            {
                /*// Set the monster to inactive in battle
                Test_Monster_Controller monsterGameObject = targetMonster.Monster_GameObject.GetComponent<Test_Monster_Controller>();
                if (monsterGameObject != null)
                {
                    monsterGameObject.isActiveInBattle = false;
                }*/
                //Removes the defeated monster from battle.
                gameManager.spawnedPlayerMonsters.Remove(targetMonster);
                Destroy(targetMonster.Monster_GameObject);

                if (CheckNextAvailableMonster(gameManager.playerParty))
                {
                    gameManager.Init_BattleSetup_SendOutPlayerMonsters();
                }
                else
                {
                    gameManager.CheckIfEndBattle();
                }
            }
        }
    }

    public bool CheckNextAvailableMonster(List<Monster> party)
    {
        foreach (var monster in party)
        {
            if (monster.monsterStatistics.current_HP > 0 && !monster.monsterinBattleInfo.isActiveInBattle)
            {
                return true;
            }
        }
        return false;
    }

    public Monster GetNextAvailableTrainerMonster()
    {
        foreach (var monster in gameManager.trainerParty)
        {
            if (monster.monsterStatistics.current_HP > 0 && !monster.monsterinBattleInfo.isActiveInBattle)
            {
                return monster;
            }
        }
        return null;
    }

    public Monster GetNextAvailablePlayerMonster()
    {
        foreach (var monster in gameManager.playerParty)
        {
            if (monster.monsterStatistics.current_HP > 0 && !monster.monsterinBattleInfo.isActiveInBattle)
            {
                gameManager.currentPlayerMonsterIndex = gameManager.playerParty.IndexOf(monster);
                return monster;
            }
        }
        return null;
    }

    public void SwapPlayerMonster()
    {
        // Get the monster whose turn it currently is
        Monster currentTurnMonster = gameManager.spawnedPlayerMonsters[gameManager.currentPlayerMonsterIndex];

        // Check if there are other monsters in the player's party that are alive and not already active in battle
        if (CheckNextAvailableMonster(gameManager.playerParty))
        {
            currentTurnMonster.monsterinBattleInfo.isActiveInBattle = false;
            //Play Swapping out / Withdrawing from battle animation.
            //Removes the Swapped monster from battle.
            gameManager.spawnedEnemyMonsters.Remove(currentTurnMonster);
            Destroy(currentTurnMonster.Monster_GameObject);

            Debug.Log($"Swapped out {currentTurnMonster.monsterSpeciesInfo.SPECIES}.");
            gameManager.Init_BattleSetup_SendOutPlayerMonsters();
        }
        else
        {
            Debug.Log("No other monsters available to swap.");
        }
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








    private IEnumerator PlayerTurn(Monster playerMonster)
    {
        gameManager.currentPlayerMonsterIndex = gameManager.playerParty.IndexOf(playerMonster);
        Debug.Log($"Player's {playerMonster.monsterSpeciesInfo.SPECIES} turn! PartySlot:{gameManager.currentPlayerMonsterIndex}");
        //Anim_StartTurn_Bop(playerMonster);

        //OPTION MENU CHOOSE AN OPTION
        yield return StartCoroutine(UI_OptionMenu(playerMonster));
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

        Anim_EndTurn_Bop(playerMonster);

        uiController.BattleUI_FightMenu.SetActive(false);
        uiController.variables.canPlayerInteract = false;

        
        //Finishes Turn Choice
        Test_Monster_Controller monsterController = playerMonster.Monster_GameObject.GetComponent<Test_Monster_Controller>();
        monsterController.SetHasChosenAction(true);
        targetingSystem.HighlightClearTarget();

        //Player Attacks the target
        //yield return StartCoroutine(PlayerAttacks(playerMonster, selectedMove));

        yield break;
    }

    private IEnumerator PlayerAttacks(Monster attackingMonster, MoveInformation selectedMove)
    {
        targetingSystem.HighlightClearTarget();
        // Get the current target from the targeting system
        GameObject targetObject = targetingSystem.GetCurrentTarget();

        if (targetObject == null)
        {
            Debug.LogWarning("No target selected for player attack!");
            yield break;
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

            yield return new WaitForSeconds(2f); // Wait for animation or effects
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



























//Animations
    public void Anim_StartTurn_Bop(Monster monster)
    {
        // Start the bop animation for the current active monster
        Test_Monster_Controller activeController = targetingSystem.currentAttackingMonster.GetComponent<Test_Monster_Controller>();

        if (activeController != null)
        {
            activeController.StartBopAnimation();
        }
        else
        {
            Debug.LogError($"MonsterController component is missing on {targetingSystem.currentAttackingMonster}'s GameObject!");
        }
    }

    public void Anim_EndTurn_Bop(Monster monster)
    {
        // Start the bop animation for the current active monster
        Test_Monster_Controller activeController = targetingSystem.currentAttackingMonster.GetComponent<Test_Monster_Controller>();

        if (activeController != null)
        {
            activeController.StopBopAnimation();
        }
        else
        {
            Debug.LogError($"MonsterController component is missing on {targetingSystem.currentAttackingMonster}'s GameObject!");
        }
    }

    public IEnumerator Anim_PerformHitReactionAnimation(Monster monster)
    {
        // Start the bop animation for the current active monster
        Test_Monster_Controller activeController = targetingSystem.currentDefendingMonster.GetComponent<Test_Monster_Controller>();

        if (activeController != null)
        {
            yield return StartCoroutine(activeController.PerformHitReactionAnimation());
        }
        else
        {
            Debug.LogError($"MonsterController component is missing on {targetingSystem.currentDefendingMonster}'s GameObject!");
        }
        yield return new WaitForSeconds(2f);    // Wait for x seconds.
    }

    public IEnumerator Anim_PerformAttackAnimation(Monster defending_monster)
    {
        // Start the bop animation for the current active monster
        Test_Monster_Controller activeController_attacker = targetingSystem.currentAttackingMonster.GetComponent<Test_Monster_Controller>();
        Transform activeController_defender = defending_monster.Monster_GameObject.transform;

        if (activeController_attacker != null && activeController_defender != null )
        {
            yield return StartCoroutine(activeController_attacker.StartAttackAnimation(activeController_defender));
        }
        else
        {
            Debug.LogError($"MonsterController component is missing on {targetingSystem.currentDefendingMonster}'s GameObject!");
        }
        yield return new WaitForSeconds(1f);    // Wait for x seconds.
    }
}
