using UnityEngine;
using System.Collections;

public class Test_Monster_Controller : MonoBehaviour
{
    [SerializeField]
    public Monster monsterData;
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


}