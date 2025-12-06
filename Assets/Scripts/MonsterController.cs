using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class MonsterController : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager
    public Info_Bar_Updater Info_Bar_Updater;
    
    [SerializeField]
    public SpawnedMonster monsterData; // Holds the monsterData's data and makes it visible in the Inspector
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component


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
            Debug.LogError("Enemy data is not assigned!");
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


    public void InitializeMonster(SpawnedMonster data)
    {
        monsterData = data;

        // Set the sprite of the enemy based on the SpawnedMonster data
        if (spriteRenderer != null && monsterData.speciesInfo.front_sprite != null)
        {
            spriteRenderer.sprite = monsterData.speciesInfo.front_sprite;
        }
        else
        {
            Debug.LogError("Failed to set enemy sprite. Ensure the SpawnedMonster has a valid sprite.");
        }
        Info_Bar_Updater.InitializeInfoBar(monsterData);
    }

    

    




    public void TakeDamage(int damage) //change to select a target.
    {
        if (monsterData != null)
        {
            int previousHP = monsterData.extra2Info.current_HP;
            monsterData.extra2Info.current_HP -= damage;
            monsterData.extra2Info.current_HP = Mathf.Clamp(monsterData.extra2Info.current_HP, 0, monsterData.extra2Info.max_HP);

            // Start coroutine to animate health bar and text
            StartCoroutine(Info_Bar_Updater.AnimateHealthBarAndText(monsterData, previousHP, monsterData.extra2Info.current_HP));

            Debug.Log(monsterData.speciesInfo.species.ToString() + " took " + damage + " damage.");
        }
    }

}