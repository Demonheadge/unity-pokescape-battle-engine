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
            move_1 = Move.BLOOD_BARRAGE,
            move_2 = Move.NONE,
            move_3 = Move.WIND_BLAST,
            move_4 = Move.NONE
        };

        Extra_3_Monster_Info extra3Info = new Extra_3_Monster_Info
        {
            //Randomises for now. (Later do some different math to decide.)
            skill_attack = UnityEngine.Random.Range(1, 99),
            skill_defense = UnityEngine.Random.Range(1, 99),
            skill_strength = UnityEngine.Random.Range(1, 99),
            skill_magic = UnityEngine.Random.Range(1, 99),
            skill_ranged = UnityEngine.Random.Range(1, 99),
            skill_necromancy = UnityEngine.Random.Range(1, 99),
            skill_prayer = UnityEngine.Random.Range(1, 99),
            skill_summoning = UnityEngine.Random.Range(1, 99),
            skill_hitpoints = UnityEngine.Random.Range(1, 99),
            skill_slayer = UnityEngine.Random.Range(1, 99),
            skill_agility = UnityEngine.Random.Range(1, 99),
            skill_mining = UnityEngine.Random.Range(1, 99),
            skill_smithing = UnityEngine.Random.Range(1, 99),
            skill_fishing = UnityEngine.Random.Range(1, 99),
            skill_woodcutting = UnityEngine.Random.Range(1, 99),
            skill_cooking = UnityEngine.Random.Range(1, 99),
            skill_fletching = UnityEngine.Random.Range(1, 99),
            skill_crafting = UnityEngine.Random.Range(1, 99),
            skill_firemaking = UnityEngine.Random.Range(1, 99),
            skill_runecrafting = UnityEngine.Random.Range(1, 99),
            skill_dungeoneering = UnityEngine.Random.Range(1, 99),
            skill_sailing = UnityEngine.Random.Range(1, 99),
            skill_herblore = UnityEngine.Random.Range(1, 99),
            skill_farming = UnityEngine.Random.Range(1, 99),
            skill_construction = UnityEngine.Random.Range(1, 99),
            skill_divination = UnityEngine.Random.Range(1, 99),
            skill_hunter = UnityEngine.Random.Range(1, 99),
            skill_archaeology = UnityEngine.Random.Range(1, 99),
            skill_thieving = UnityEngine.Random.Range(1, 99)
        };

        // Generate random experience points for the monster (you can adjust this logic)
        int randomExperience = UnityEngine.Random.Range(0, 1000000);
        // Calculate the level based on experience
        int calculatedLevel = CalculateLevel(randomExperience);
        //Specific IV skills that affect stats
        int atk_melee_iv_total = extra3Info.skill_summoning; //+ extra3Info.skill_necromancy + extra3Info.skill_prayer;
        int atk_ranged_iv_total = extra3Info.skill_summoning;
        int atk_magic_iv_total= extra3Info.skill_summoning;
        int def_melee_iv_total= extra3Info.skill_summoning;
        int def_ranged_iv_total = extra3Info.skill_summoning;
        int def_magic_iv_total = extra3Info.skill_summoning;

        Extra_2_Monster_Info extra2Info = new Extra_2_Monster_Info
        {
            experience = randomExperience,  //Needs to Swap to randomly generate the level. Depending on the level generated determines the xp.
            level = calculatedLevel, // Use the calculated level
            max_HP = randomMonster.baseHP,
            current_HP = randomMonster.baseHP,
            current_Speed = randomMonster.baseSpeed,

            //Overall = ( ( (2 × Base) + (IV_Total / 4) ) × Level) / 100 )
            //attack
            current_Attack_Melee = ((2 * randomMonster.baseAttack_Melee + (atk_melee_iv_total / 4)) * calculatedLevel) / 100, 
            current_Attack_Ranged = ((2 * randomMonster.baseAttack_Ranged + (atk_ranged_iv_total / 4)) * calculatedLevel) / 100, 
            current_Attack_Magic = ((2 * randomMonster.baseAttack_Magic + (atk_magic_iv_total / 4)) * calculatedLevel) / 100, 
            //defense
            current_Defense_Melee = ((2 * randomMonster.baseDefense_Melee + (def_melee_iv_total / 4)) * calculatedLevel) / 100, 
            current_Defense_Ranged = ((2 * randomMonster.baseDefense_Ranged + (def_ranged_iv_total / 4)) * calculatedLevel) / 100,
            current_Defense_Magic = ((2 * randomMonster.baseDefense_Magic + (def_magic_iv_total / 4)) * calculatedLevel) / 100, 
        };

        

        SpawnedMonster newMonster = new SpawnedMonster(randomMonster, extra1Info, extra2Info, extra3Info);
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
                
                ///Base Species Info
                //attack
                case "base_hp":
                    text.text = "Species HP: " + monster.speciesInfo.baseHP;
                    break;
                case "base_speed":
                    text.text = "Species Speed: " + monster.speciesInfo.baseSpeed;
                    break;
                case "base_attack_MELEE":
                    text.text = "Species Melee (atk): " + monster.speciesInfo.baseAttack_Melee;
                    break;
                case "base_attack_RANGED":
                    text.text = "Species Ranged (atk): " + monster.speciesInfo.baseAttack_Ranged;
                    break;
                case "base_attack_MAGIC":
                    text.text = "Species Magic (atk): " + monster.speciesInfo.baseAttack_Magic;
                    break;
                //defense
                case "base_defense_MELEE":
                    text.text = "Species Melee (def): " + monster.speciesInfo.baseDefense_Melee;
                    break;
                case "base_defense_RANGED":
                    text.text = "Species Ranged (def): " + monster.speciesInfo.baseDefense_Ranged;
                    break;
                case "base_defense_MAGIC":
                    text.text = "Species Magic (def): " + monster.speciesInfo.baseDefense_Magic;
                    break;
                ///
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
                    text.text = "EXP: " + monster.extra2Info.experience;
                    break;
                case "Current_HP":
                    text.text = "HP: " + monster.extra2Info.current_HP + " / " + monster.extra2Info.max_HP;
                    break;
                case "SPEED":
                    text.text = "Speed / Agility: " + monster.extra2Info.current_Speed;
                    break;
                //attack
                case "attack_MELEE":
                    text.text = "Melee: " + monster.extra2Info.current_Attack_Melee;
                    break;
                case "attack_RANGED":
                    text.text = "Ranged: " + monster.extra2Info.current_Attack_Ranged;
                    break;
                case "attack_MAGIC":
                    text.text = "Magic: " + monster.extra2Info.current_Attack_Magic;
                    break;
                //defense
                case "defense_MELEE":
                    text.text = "Melee: " + monster.extra2Info.current_Defense_Melee;
                    break;
                case "defense_RANGED":
                    text.text = "Ranged: " + monster.extra2Info.current_Defense_Ranged;
                    break;
                case "defense_MAGIC":
                    text.text = "Magic: " + monster.extra2Info.current_Defense_Magic;
                    break;
                //Moves
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
                //Skills
                case "Attack":
                    text.text = "Attack: " + monster.extra3Info.skill_attack;
                    break;
                case "Defense":
                    text.text = "Defence: " + monster.extra3Info.skill_defense;
                    break;
                case "Strength":
                    text.text = "Strength: " + monster.extra3Info.skill_strength;
                    break;
                case "Magic":
                    text.text = "Magic: " + monster.extra3Info.skill_magic;
                    break;
                case "Ranged":
                    text.text = "Ranged: " + monster.extra3Info.skill_ranged;
                    break;
                case "Necromancy":
                    text.text = "Necromancy: " + monster.extra3Info.skill_necromancy;
                    break;
                case "Prayer":
                    text.text = "Prayer: " + monster.extra3Info.skill_prayer;
                    break;
                case "Summoning":
                    text.text = "Summoning: " + monster.extra3Info.skill_summoning;
                    break;
                case "Hitpoints":
                    text.text = "Hitpoints: " + monster.extra3Info.skill_hitpoints;
                    break;
                case "Slayer":
                    text.text = "Slayer: " + monster.extra3Info.skill_slayer;
                    break;
                case "Mining":
                    text.text = "Mining: " + monster.extra3Info.skill_mining;
                    break;
                case "Smithing":
                    text.text = "Smithing: " + monster.extra3Info.skill_smithing;
                    break;
                case "Fishing":
                    text.text = "Fishing: " + monster.extra3Info.skill_fishing;
                    break;
                case "Fletching":
                    text.text = "Fletching: " + monster.extra3Info.skill_fletching;
                    break;
                case "Cooking":
                    text.text = "Cooking: " + monster.extra3Info.skill_cooking;
                    break;
                case "Woodcutting":
                    text.text = "Woodcutting: " + monster.extra3Info.skill_woodcutting;
                    break;
                case "Crafting":
                    text.text = "Crafting: " + monster.extra3Info.skill_crafting;
                    break;
                case "Firemaking":
                    text.text = "Firemaking: " + monster.extra3Info.skill_firemaking;
                    break;
                case "Runecrafting":
                    text.text = "Runecrafting: " + monster.extra3Info.skill_runecrafting;
                    break;
                case "Dungeoneering":
                    text.text = "Dungeoneering: " + monster.extra3Info.skill_dungeoneering;
                    break;
                case "Sailing":
                    text.text = "Sailing: " + monster.extra3Info.skill_sailing;
                    break;
                case "Herblore":
                    text.text = "Herblore: " + monster.extra3Info.skill_herblore;
                    break;
                case "Farming":
                    text.text = "Farming: " + monster.extra3Info.skill_farming;
                    break;
                case "Construction":
                    text.text = "Construction: " + monster.extra3Info.skill_construction;
                    break;
                case "Divination":
                    text.text = "Divination: " + monster.extra3Info.skill_divination;
                    break;
                case "Hunter":
                    text.text = "Hunter: " + monster.extra3Info.skill_hunter;
                    break;
                case "Invention":
                    text.text = "Invention: " + monster.extra3Info.skill_invention;
                    break;
                case "Archaeology":
                    text.text = "Archaeology: " + monster.extra3Info.skill_archaeology;
                    break;
                case "Thieving":
                    text.text = "Thieving: " + monster.extra3Info.skill_thieving;
                    break;
                case "Agility":
                    text.text = "Agility: " + monster.extra3Info.skill_agility;
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



    private int CalculateLevel(int experience)
    {
        // Max level and max experience
        const int maxLevel = 99;
        const int maxExperience = 1000000;

        // Calculate the level based on experience
        // Using a quadratic formula for leveling progression
        float level = Mathf.Floor(Mathf.Sqrt((float)experience / maxExperience) * maxLevel);

        // Ensure the level is at least 1 and does not exceed maxLevel
        return Mathf.Clamp((int)level, 1, maxLevel);
    }
}
