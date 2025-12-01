// 2/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SpawnedMonster enemyData; // Holds the enemy's data
    public GameObject enemyInfoUI; // Reference to the UI element for this enemy's info

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

        // Additional initialization logic (e.g., setting stats, animations, etc.)
        Debug.Log($"Enemy {enemyData.speciesInfo.species} spawned with level {enemyData.extra2Info.level} and HP {enemyData.extra2Info.current_HP}");
    }
}