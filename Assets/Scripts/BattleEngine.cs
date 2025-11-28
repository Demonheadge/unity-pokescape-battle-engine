// 26/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections;
using System.Collections.Generic; // Added for List<>
using UnityEngine;
using UnityEngine.InputSystem; // Added for PlayerInput and InputAction
using TMPro;

public class BattleEngine : MonoBehaviour
{
    public Transform playerSpawnLocation1;
    public Transform playerSpawnLocation2; // Optional
    public Transform enemySpawnLocation1;
    public Transform enemySpawnLocation2; // Optional
    public Transform enemySpawnLocation3; // Optional
    public Transform enemySpawnLocation4; // Optional

    public GameObject playerMonsterPrefab1;
    public GameObject playerMonsterPrefab2; // Optional
    public GameObject enemyMonsterPrefab1;
    public GameObject enemyMonsterPrefab2; // Optional
    public GameObject enemyMonsterPrefab3; // Optional
    public GameObject enemyMonsterPrefab4; // Optional

    public TextMeshProUGUI battleStartText;
    public TextMeshProUGUI battleEndText;

    private GameObject playerMonster1;
    private GameObject playerMonster2; // Optional
    public List<GameObject> enemyMonsters = new List<GameObject>();

    private bool battleActive = false;

    private PlayerInput playerInput;
    private InputAction startBattleAction;

    public BattleUIManager battleUIManager; // Reference to BattleUIManager

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        startBattleAction = playerInput.actions["StartBattle"];
        startBattleAction.performed += ctx => StartBattle();
    }

    void StartBattle()
    {
        if (battleActive) return;

        battleActive = true;

        // Display "Battle Start!" text
        battleStartText.text = "Battle Start!";
        battleStartText.gameObject.SetActive(true);

        // Disable Choose_Options UI
        //battleUIManager.SetChooseOptionsInteractable(false);

        // Start coroutine to hide battleStartText after 5 seconds
        StartCoroutine(HideBattleStartText());

        // Spawn Player Monsters
        playerMonster1 = Instantiate(playerMonsterPrefab1, playerSpawnLocation1.position, Quaternion.identity);
        if (playerSpawnLocation2 != null && playerMonsterPrefab2 != null)
        {
            playerMonster2 = Instantiate(playerMonsterPrefab2, playerSpawnLocation2.position, Quaternion.identity);
        }

        // Spawn Enemy Monsters
        enemyMonsters.Add(Instantiate(enemyMonsterPrefab1, enemySpawnLocation1.position, Quaternion.identity));
        if (enemySpawnLocation2 != null && enemyMonsterPrefab2 != null)
            enemyMonsters.Add(Instantiate(enemyMonsterPrefab2, enemySpawnLocation2.position, Quaternion.identity));
        if (enemySpawnLocation3 != null && enemyMonsterPrefab3 != null)
            enemyMonsters.Add(Instantiate(enemyMonsterPrefab3, enemySpawnLocation3.position, Quaternion.identity));
        if (enemySpawnLocation4 != null && enemyMonsterPrefab4 != null)
            enemyMonsters.Add(Instantiate(enemyMonsterPrefab4, enemySpawnLocation4.position, Quaternion.identity));

        Debug.Log("Battle Started!");
    }

    IEnumerator HideBattleStartText()
    {
        yield return new WaitForSeconds(2);
        battleStartText.gameObject.SetActive(false);

        // Enable Choose_Options UI
        battleUIManager.SetChooseOptionsInteractable(true);
    }

    public void EndBattle(string winner)
    {
        battleActive = false;

        // Despawn Player Monsters
        if (playerMonster1 != null)
            Destroy(playerMonster1); // Fixed: Destroy only the GameObject
        if (playerMonster2 != null)
            Destroy(playerMonster2); // Fixed: Destroy only the GameObject

        // Despawn Enemy Monsters
        foreach (var enemy in enemyMonsters)
        {
            if (enemy != null)
                Destroy(enemy); // Fixed: Destroy only the GameObject
        }
        enemyMonsters.Clear();

        // Display "Battle End" text
        battleEndText.text = $"Battle End! {winner} wins!";
        battleEndText.gameObject.SetActive(true);

        Debug.Log("Battle Ended!");
    }

    public void PlayerAction(string action, GameObject targetEnemy = null)
    {
        if (!battleActive) return;

        if (action == "Run")
        {
            EndBattle("Enemy");
        }
        else if (action == "Fight")
        {
            if (targetEnemy != null)
            {
                ExecuteTurn(playerMonster1, targetEnemy);
            }
        }
    }

    void ExecuteTurn(GameObject attacker, GameObject target)
    {
        Monster_Information attackerStats = attacker.GetComponent<Monster_Information>();
        Monster_Information targetStats = target.GetComponent<Monster_Information>();

        if (attackerStats != null && targetStats != null)
        {
            int damage = attackerStats.GetMoveDamage();
            targetStats.TakeDamage(damage);

            Debug.Log($"{attackerStats.monsterName} attacked {targetStats.monsterName} for {damage} damage!");

            if (targetStats.hitPoints <= 0)
            {
                Debug.Log($"{targetStats.monsterName} has fainted!");
                Destroy(target); // Fixed: Destroy only the GameObject
                enemyMonsters.Remove(target);
            }

            CheckBattleEnd();
        }
    }

    void CheckBattleEnd()
    {
        if (enemyMonsters.Count == 0)
        {
            Debug.Log("Player wins!");
            EndBattle("Player");
        }
        else if (playerMonster1.GetComponent<Monster_Information>().hitPoints <= 0 && (playerMonster2 == null || playerMonster2.GetComponent<Monster_Information>().hitPoints <= 0))
        {
            Debug.Log("Player loses!");
            EndBattle("Enemy");
        }
    }
}