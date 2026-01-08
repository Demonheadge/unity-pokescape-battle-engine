using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test_Monster_Controller : MonoBehaviour
{
    [SerializeField]
    public Monster monsterData;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private Coroutine attackCoroutine; // To manage the attack animation

    public GameManager gameManager;
    public Test_Generate_Monster test_Generate_Monster;
    public Monster.Statistics monsterStatistics; // Contains stats like current_HP, max_HP, current_Speed, etc.
    public Monster.SpeciesInfo monsterSpeciesInfo; // Contains species-specific info
    public List<Move> availableMoves; // List of moves the monster can use
    public bool HasChosenAction { get; private set; } // Tracks if the monster has chosen an action
    public bool IsSwappingOut { get; private set; } // Tracks if the monster is swapping out
    public Move chosenMove; // The move chosen by the monster
    public Monster targetMonster; // The target for the chosen move

    public void SetHasChosenAction(bool value)
    {
        HasChosenAction = value;
    }

    // Fetch usable moves based on the MoveDatabase
    public List<MoveInformation> GetUsableMoves(Move species, int id, int level)
    {
        List<MoveInformation> usableMoves = new List<MoveInformation>();
        foreach (var moveInfo in test_Generate_Monster.moveDatabase.moves)
        {
            if (moveInfo.move == species && moveInfo.damage <= level) // Example condition
            {
                usableMoves.Add(moveInfo);
            }
        }
        return usableMoves;
    }



    public void ChooseAction(Monster monster)
    {
        // Reset action flags
        SetHasChosenAction(false);
        IsSwappingOut = false;

        // Check if the monster's health is critically low
        if (monster.monsterStatistics.current_HP <= monster.monsterStatistics.max_HP * 0.2f)
        {
            // If health is critically low, decide whether to swap out
            /*if (ShouldSwapOut())
            {
                IsSwappingOut = true;
                HasChosenAction = true;
                Debug.Log($"{monsterSpeciesInfo.SPECIES} has chosen to swap out due to low health.");
                return;
            }*/
            Debug.LogError($"{monster.monsterSpeciesInfo.SPECIES} considered swapping out...");
        }

        // If not swapping, choose the best move
        chosenMove = ChooseBestMove(monster);
        targetMonster = ChooseTarget();

        if (chosenMove != Move.NONE && targetMonster != null)
        {
            SetHasChosenAction(true);
            Debug.Log($"{monster.monsterSpeciesInfo.SPECIES} has chosen to use {chosenMove} on {targetMonster.monsterSpeciesInfo.SPECIES}.");
        }
        else
        {
            Debug.LogError($"{monster.monsterSpeciesInfo.SPECIES} could not choose a valid action.");
        }
    }

    private bool ShouldSwapOut()
    {
        // Example logic for swapping out:
        // Check if there are other monsters in the party that are alive and not already active in battle
        Monster nextMonster = gameManager.test_TurnBasedBattleSystem.GetNextAvailableTrainerMonster();
        return nextMonster != null; // Swap out if there is a valid monster to replace this one
    }


    private Move ChooseBestMove(Monster monster)
    {
        List<MoveInformation> moves = new List<MoveInformation>
        {
            gameManager.UI_controller.GetMoveInformation(monster.monsterMoves.MOVE_1),
            gameManager.UI_controller.GetMoveInformation(monster.monsterMoves.MOVE_2),
            gameManager.UI_controller.GetMoveInformation(monster.monsterMoves.MOVE_3),
            gameManager.UI_controller.GetMoveInformation(monster.monsterMoves.MOVE_4)
        };
        // Remove null moves and moves with Move.None
        moves.RemoveAll(move => move == null || move.move == Move.NONE);

        if (moves.Count == 0)
        {
            Debug.LogError("No valid moves available for enemy!");
            return Move.NONE;
        }

        //Return For randomness.
        //return moves[UnityEngine.Random.Range(0, moves.Count)];
        

        // Example logic for choosing the best move:
        // Select the move with the highest damage
        MoveInformation bestMove = null;

        int highestDamage = 0;

        foreach (var moveInfo in moves)
        {
            if (moveInfo.damage > highestDamage)
            {
                bestMove = moveInfo;
                highestDamage = moveInfo.damage;
            }
        }

        return bestMove.move;
    }

    private Monster ChooseTarget()
    {
        // Check if gameManager is null
        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is null! Cannot choose a target.");
            return null;
        }

        // Check if spawnedEnemyMonsters is null or empty
        if (gameManager.spawnedEnemyMonsters == null || gameManager.spawnedEnemyMonsters.Count == 0)
        {
            Debug.LogError("No enemy monsters available to choose a target!");
            return null;
        }

        // Select the enemy monster with the lowest health that is still alive
        Monster target = null;
        int lowestHealth = int.MaxValue;

        foreach (var enemyMonster in gameManager.spawnedEnemyMonsters)
        {
            if (enemyMonster != null && enemyMonster.monsterStatistics.current_HP > 0 && enemyMonster.monsterStatistics.current_HP < lowestHealth)
            {
                target = enemyMonster;
                lowestHealth = enemyMonster.monsterStatistics.current_HP;
            }
        }

        if (target == null)
        {
            Debug.LogWarning("No valid target found among enemy monsters!");
        }

        return target;
    }

























/// /////////////////////////////////////////////////////////////////////////////





    private void Awake()
    {
        // Get the SpriteRenderer component from the enemy GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on the enemy prefab!");
        }
    }

    private void Start()
    {        
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager instance not found in the scene!");
        }

        if (monsterData != null)
        {
            InitializeMonster(monsterData);
        }
        else
        {
            Debug.LogError("Monster data is not assigned!");
        }
    }
    
    // Method to flip the sprite horizontally
    public void FlipSprite(bool shouldFlip)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = shouldFlip;
        }
        else
        {
            Debug.LogError("SpriteRenderer component is not assigned!");
        }
    }


    public void InitializeMonster(Monster data)
    {
        monsterData = data;

        // Set the sprite of the enemy based on the Monster data
        if (spriteRenderer != null && monsterData.monsterSpeciesInfo.front_sprite != null)
        {
            spriteRenderer.sprite = monsterData.monsterSpeciesInfo.front_sprite;
        }
        else
        {
            Debug.LogError("Failed to set monster sprite. Ensure the Monster has a valid sprite.");
        }
    }




































//Animations

    private Coroutine bopCoroutine; // To manage the bop animation
    private Vector3 turnStartPosition; // Store the position of the monster at the start of its turn

    
    public void StartBopAnimation()
    {
        if (bopCoroutine != null)
        {
            StopCoroutine(bopCoroutine);
        }

        // Store the current position as the turn start position
        turnStartPosition = transform.position;

        bopCoroutine = StartCoroutine(PerformBopAnimation());
    }

    public void StopBopAnimation()
    {
        if (bopCoroutine != null)
        {
            StopCoroutine(bopCoroutine);
            bopCoroutine = null;

            // Reset position to turn start position
            transform.position = turnStartPosition;
        }
    }

    public IEnumerator PerformBopAnimation()
    {
        Transform monsterTransform = transform;

        // Perform a subtle bop animation
        Vector3 bopUpPosition = turnStartPosition + new Vector3(0, 0.1f, 0); // Adjust the bop height to be subtle
        float bopDuration = 0.3f; // Duration of each bop

        while (true)
        {
            float elapsedTime = 0;

            // Move up
            while (elapsedTime < bopDuration / 2)
            {
                monsterTransform.position = Vector3.Lerp(turnStartPosition, bopUpPosition, elapsedTime / (bopDuration / 2));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            monsterTransform.position = bopUpPosition;

            // Move down
            elapsedTime = 0;
            while (elapsedTime < bopDuration / 2)
            {
                monsterTransform.position = Vector3.Lerp(bopUpPosition, turnStartPosition, elapsedTime / (bopDuration / 2));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            monsterTransform.position = turnStartPosition;
        }
    }

    public IEnumerator PerformHitReactionAnimation()
    {
        Transform monsterTransform = transform;

        // Perform a simple hit reaction animation
        Vector3 originalPosition = monsterTransform.position;
        Vector3 hitBackPosition = originalPosition - new Vector3(0.2f, 0, 0); // Adjust the hit back distance as needed

        float hitDuration = 0.2f; // Duration of the hit reaction
        float elapsedTime = 0;

        // Move back
        while (elapsedTime < hitDuration)
        {
            monsterTransform.position = Vector3.Lerp(originalPosition, hitBackPosition, elapsedTime / hitDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        monsterTransform.position = hitBackPosition;

        // Move forward to the original position
        elapsedTime = 0;
        while (elapsedTime < hitDuration)
        {
            monsterTransform.position = Vector3.Lerp(hitBackPosition, originalPosition, elapsedTime / hitDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        monsterTransform.position = originalPosition;
    }

    public IEnumerator StartAttackAnimation(Transform targetTransform)
    {
        // If an attack animation is already running, stop it
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        // Start the attack animation coroutine
        yield return attackCoroutine = StartCoroutine(PerformAttackAnimation(targetTransform));
    }

    public IEnumerator PerformAttackAnimation(Transform targetTransform)
    {
        Transform attackerTransform = transform;

        // Get the original position of the attacker
        Vector3 originalPosition = attackerTransform.position;

        // Calculate the target position for the tackle/slide animation
        Vector3 tacklePosition = Vector3.Lerp(originalPosition, targetTransform.position, 0.5f); // Move halfway towards the target

        float tackleDuration = 0.3f; // Duration of the tackle
        float returnDuration = 0.3f; // Duration of the return
        float elapsedTime = 0;

        // Move forward (tackle)
        while (elapsedTime < tackleDuration)
        {
            attackerTransform.position = Vector3.Lerp(originalPosition, tacklePosition, elapsedTime / tackleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        attackerTransform.position = tacklePosition;

        // Perform attack (you can add attack effects or logic here)
        yield return new WaitForSeconds(0.2f); // Small delay to simulate the attack

        // Move back to the original position (slide back)
        elapsedTime = 0;
        while (elapsedTime < returnDuration)
        {
            attackerTransform.position = Vector3.Lerp(tacklePosition, originalPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        attackerTransform.position = originalPosition;

        // Reset the coroutine reference after completion
        attackCoroutine = null;
    }

}