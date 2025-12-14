// 14/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;

public class TestSpawnMonster : MonoBehaviour
{
    public Test_Generate_Monster generate_Monster;

    public Vector3 spawnPosition; // Set the position where the monster should spawn

    public void SpawnMonster()
    {
            // Generate a new Monster data
            Monster CreatedMonster = generate_Monster.CreateMonster();
            Debug.Log($"Spawned monster: {CreatedMonster.monsterSpeciesInfo.SPECIES} at position {spawnPosition}");

            Monster CreatedMonster2 = generate_Monster.CreateMonster();
            Debug.Log($"Spawned monster: {CreatedMonster2.monsterSpeciesInfo.SPECIES} at position {spawnPosition}");

            // Instantiate the monster's GameObject in the scene
            //GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

            // Access the Monster component
            /*Monster monsterComponent = CreatedMonster.GetComponent<Monster>();

            if (monsterComponent != null)
            {
                // Assign the generated Monster data to the Monster component
                //monsterComponent.Monster_GameObject = spawnedMonster;
                //monsterComponent.monsterSpecies = MonsterData.monsterSpecies;

                Debug.Log($"Spawned monster: {monsterComponent.monsterSpecies.SPECIES} at position {spawnPosition}");
            }
            else
            {
                Debug.LogError("The spawned object does not have a Monster component attached.");
            }*/
    }

    private void Start()
    {
        // Example: Automatically spawn a monster when the game starts
        SpawnMonster();
    }
}
