// 3/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

// 3/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager
    public Info_Bar_Updater Info_Bar_Updater;
    

    [SerializeField]
    public SpawnedMonster enemyData; // Holds the enemy's data and makes it visible in the Inspector
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component



    private void Awake()
    {
        // Get the SpriteRenderer component from the enemy GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on the enemy prefab!");
        }
        EnemyController enemyController = this.GetComponent<EnemyController>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager instance not found in the scene!");
        }

        if (enemyData != null)
        {
            InitializeEnemy(enemyData);
            gameManager.spawnedEnemies.Add(this);
        }
        else
        {
            Debug.LogError("Enemy data is not assigned!");
        }
    }


    public void InitializeEnemy(SpawnedMonster data)
    {
        enemyData = data;

        // Set the sprite of the enemy based on the SpawnedMonster data
        if (spriteRenderer != null && enemyData.speciesInfo.front_sprite != null)
        {
            spriteRenderer.sprite = enemyData.speciesInfo.front_sprite;
        }
        else
        {
            Debug.LogError("Failed to set enemy sprite. Ensure the SpawnedMonster has a valid sprite.");
        }
        Info_Bar_Updater.InitializeInfoBar(enemyData);
    }

    

    




    public void TakeDamage(int damage) //change to select a target.
    {
        if (enemyData != null)
        {
            int previousHP = enemyData.extra2Info.current_HP;
            enemyData.extra2Info.current_HP -= damage;
            enemyData.extra2Info.current_HP = Mathf.Clamp(enemyData.extra2Info.current_HP, 0, enemyData.extra2Info.max_HP);

            // Start coroutine to animate health bar and text
            StartCoroutine(Info_Bar_Updater.AnimateHealthBarAndText(enemyData, previousHP, enemyData.extra2Info.current_HP));

            Debug.Log(enemyData.speciesInfo.species.ToString() + " took " + damage + " damage.");
        }
    }

}