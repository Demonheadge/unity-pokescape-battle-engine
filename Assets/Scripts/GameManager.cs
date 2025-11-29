// 29/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public List<SpawnedMonster> playerParty = new List<SpawnedMonster>();
    public GameObject partySlot1; // Reference to the Party_Slot_1 GameObject
    public GameObject partySlot2; // Reference to the Party_Slot_2 GameObject
    public GameObject partySlot3; // Reference to the Party_Slot_3 GameObject
    public GameObject partySlot4; // Reference to the Party_Slot_4 GameObject
    public GameObject partySlot5; // Reference to the Party_Slot_5 GameObject
    public GameObject partySlot6; // Reference to the Party_Slot_6 GameObject
    public MonsterDatabase monsterDatabase; // Reference to the MonsterDatabase asset
    public GameObject enemyMonsterPrefab; // Prefab for the enemy monster
    public Transform enemySpawnPoint; // Transform where the enemy monster will spawn
    private GameObject currentEnemy;
    public Variables variables;

    private void Update()
    {
        if (variables.isInAMenu == false)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                StartBattle();
            }

            if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            {
                EndBattle();
            }
        }
        
    }

    public void AddMonsterToParty()
    {
        if (playerParty.Count >= 6)
        {
            Debug.Log("Party is full!");
            return;
        }

        // Randomly select a monster from the MonsterDatabase
        SpeciesInfo randomMonster = monsterDatabase.monsters[UnityEngine.Random.Range(0, monsterDatabase.monsters.Count)];

        // Create a new SpawnedMonster instance
        Extra_1_Monster_Info extra1Info = new Extra_1_Monster_Info
        {
            experience = 0,
            HP_IV = UnityEngine.Random.Range(0, 31),
            Attack_IV = UnityEngine.Random.Range(0, 31),
            Defense_IV = UnityEngine.Random.Range(0, 31),
            Speed_IV = UnityEngine.Random.Range(0, 31),
            move_1 = Move.BLOOD_BARRAGE,
            move_2 = Move.NONE,
            move_3 = Move.WIND_BLAST,
            move_4 = Move.NONE
        };

        Extra_2_Monster_Info extra2Info = new Extra_2_Monster_Info
        {
            level = 99,
            max_HP = randomMonster.baseHP,
            current_HP = randomMonster.baseHP,
            current_Attack = randomMonster.baseAttack,
            current_Defense = randomMonster.baseDefense,
            current_Speed = randomMonster.baseSpeed
        };

        SpawnedMonster newMonster = new SpawnedMonster(randomMonster, extra1Info, extra2Info);
        playerParty.Add(newMonster);

        // Update Party_Slot
        switch (playerParty.Count)
        {
            case 1:
                UpdatePartySlotUI(partySlot1, newMonster);
                break;
            case 2:
                UpdatePartySlotUI(partySlot2, newMonster);
                break;
            case 3:
                UpdatePartySlotUI(partySlot3, newMonster);
                break;
            case 4:
                UpdatePartySlotUI(partySlot4, newMonster);
                break;
            case 5:
                UpdatePartySlotUI(partySlot5, newMonster);
                break;
            case 6:
                UpdatePartySlotUI(partySlot6, newMonster);
                break;
        }
    }

    private void UpdatePartySlotUI(GameObject slot, SpawnedMonster monster)
    {
        TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            switch (text.name)
            {
                case "Species":
                    text.text = monster.speciesInfo.species.ToString();
                    break;
                case "Nickname":
                    text.text = monster.speciesInfo.name;
                    break;
                case "Type":
                    text.text = "Type: " + monster.speciesInfo.type;
                    break;
                case "Level":
                    text.text = "Level: " + monster.extra2Info.level;
                    break;
                case "Experience":
                    text.text = "EXP: " + monster.extra1Info.experience;
                    break;
                case "Current_HP":
                    text.text = "HP: " + monster.extra2Info.current_HP + "/" + monster.extra2Info.max_HP;
                    break;
                case "HP":
                    text.text = "HP IV: " + monster.extra1Info.HP_IV;
                    break;
                case "ATK":
                    text.text = "ATK IV: " + monster.extra1Info.Attack_IV;
                    break;
                case "DEF":
                    text.text = "DEF IV: " + monster.extra1Info.Defense_IV;
                    break;
                case "SPEED":
                    text.text = "SPEED IV: " + monster.extra1Info.Speed_IV;
                    break;
                case "MOVE_SLOT_1":
                    text.text = monster.extra1Info.move_1.ToString();
                    break;
                case "MOVE_SLOT_2":
                    text.text = monster.extra1Info.move_2.ToString();
                    break;
                case "MOVE_SLOT_3":
                    text.text = monster.extra1Info.move_3.ToString();
                    break;
                case "MOVE_SLOT_4":
                    text.text = monster.extra1Info.move_4.ToString();
                    break;
            }
        }
        // Update the FRONT_SPRITE and BACK_SPRITE images
        Image[] images = slot.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            switch (image.name)
            {
                case "Front_Sprite":
                    image.sprite = monster.speciesInfo.front_sprite;
                    image.preserveAspect = true; // Ensure the aspect ratio is preserved
                    break;
                case "Back_Sprite":
                    image.sprite = monster.speciesInfo.back_sprite;
                    image.preserveAspect = true; // Ensure the aspect ratio is preserved
                    break;
                case "PartyIcon":
                    image.sprite = monster.speciesInfo.partyicon;
                    image.preserveAspect = true; // Ensure the aspect ratio is preserved
                    break;
            }
        }
    }

    private void StartBattle()
    {
        if (playerParty.Count == 0)
        {
            Debug.Log("No monsters in the player's party!");
            return;
        }

        // Randomly select an enemy monster from the MonsterDatabase
        SpeciesInfo randomEnemyMonster = monsterDatabase.monsters[UnityEngine.Random.Range(0, monsterDatabase.monsters.Count)];

        // Instantiate the enemy monster
        currentEnemy = Instantiate(enemyMonsterPrefab, enemySpawnPoint.position, Quaternion.identity);
        currentEnemy.GetComponent<MonsterDisplay>().Setup(randomEnemyMonster); // Assuming MonsterDisplay is a script to display monster info
    }

    private void EndBattle()
    {
        if (currentEnemy != null)
        {
            Destroy(currentEnemy);
            currentEnemy = null;
            Debug.Log("Battle ended!");
        }
        else
        {
            Debug.Log("No battle to end!");
        }
    }

    public void ClearPlayerParty()
    {
        playerParty.Clear();
        Debug.Log("Player's party cleared!");
        // Clear Party_Slot UI
        ClearPartySlotUI(partySlot1);
        ClearPartySlotUI(partySlot2);
        ClearPartySlotUI(partySlot3);
        ClearPartySlotUI(partySlot4);
        ClearPartySlotUI(partySlot5);
        ClearPartySlotUI(partySlot6);
    }

    private void ClearPartySlotUI(GameObject slot)
    {
        TextMeshProUGUI[] texts = slot.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            // Skip clearing the text if it matches "Party Slot"
            if ((text.text != "Party Slot 1") 
            || (text.text != "Party Slot 2")
            || (text.text != "Party Slot 3")
            || (text.text != "Party Slot 4")
            || (text.text != "Party Slot 5")
            || (text.text != "Party Slot 6"))
            {
                text.text = "";
            }
        }

        Image[] images = slot.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            if (image != null)
            {
                image.sprite = null;
            }
        }

    }
}
