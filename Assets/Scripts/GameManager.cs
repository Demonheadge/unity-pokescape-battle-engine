// 29/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Generate_Monster generate_Monster;
    public TurnBasedBattleSystem TurnbasedSystem;
    
    public int playerPartySize = 6; //Max amount of monsters allowed in the players party.
    public List<SpawnedMonster> playerParty = new List<SpawnedMonster>();
    
    public Variables variables;
    public UI_Controller UI_controller;
    public GameObject BattleScene;
    public GameObject Background;
    public BattleType currentBattleType;
    public GameObject enemyMonsterPrefab; // Prefab for the enemy monster
    public GameObject playerMonsterPrefab; // Prefab for the enemy monster
    private GameObject playerMonsterInstance;
    private GameObject currentEnemy;
    private GameObject secondEnemy;
    private Vector3 SpawnPoint;
    public List<EnemyController> spawnedEnemies = new List<EnemyController>(); // Correctly define the list to store EnemyController instances
    public int selectedTargetIndex = -1; // Index of the currently selected target

    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void SetInitialTarget()
    {
        if (spawnedEnemies.Count > 0)
        {
            selectedTargetIndex = 0; // Set the initial target to the first monster in the list
            HighlightSelectedTarget();
        }
        else
        {
            Debug.LogWarning("No enemies available to set as the initial target.");
        }
    }

    public void SwapTarget()
    {
        if (spawnedEnemies.Count > 0)
        {
            // Increment the selected target index
            selectedTargetIndex++;
            if (selectedTargetIndex >= spawnedEnemies.Count)
            {
                selectedTargetIndex = 0; // Wrap around to the first enemy
            }

            // Highlight the selected target
            HighlightSelectedTarget();
        }
    }

    public void HighlightSelectedTarget()
    {
        // Reset highlight for all enemies
        foreach (var enemy in spawnedEnemies)
        {
            var spriteRenderer = enemy.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white; // Reset color
            }
        }

        // Highlight the selected enemy
        if (spawnedEnemies.Count > 0 && selectedTargetIndex >= 0 && selectedTargetIndex < spawnedEnemies.Count)
        {
            var selectedEnemy = spawnedEnemies[selectedTargetIndex];
            var selectedSpriteRenderer = selectedEnemy.GetComponent<SpriteRenderer>();
            if (selectedSpriteRenderer != null)
            {
                selectedSpriteRenderer.color = Color.yellow; // Highlight color
            }

            Debug.Log($"Selected target: {selectedEnemy.enemyData.speciesInfo.species}");
        }
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        if (spawnedEnemies.Contains(enemy))
        {
            int removedIndex = spawnedEnemies.IndexOf(enemy);
            spawnedEnemies.Remove(enemy);

            // Adjust the selected target index
            if (removedIndex == selectedTargetIndex)
            {
                if (spawnedEnemies.Count > 0)
                {
                    selectedTargetIndex = Mathf.Clamp(removedIndex, 0, spawnedEnemies.Count - 1);
                    HighlightSelectedTarget();
                }
                else
                {
                    selectedTargetIndex = -1; // No valid targets left
                    Debug.Log("No more enemies left.");
                }
            }
            else if (removedIndex < selectedTargetIndex)
            {
                selectedTargetIndex--; // Adjust index if a preceding enemy was removed
            }
        }
    }

    public void AddMonsterToParty()
    {
        if (playerParty.Count >= playerPartySize)
        {
            Debug.Log("Party is full!");
            return;
        }
        // Call SpawnMonster and get the returned SpawnedMonster object
        SpawnedMonster newMonster = generate_Monster.SpawnMonster();
        playerParty.Add(newMonster);

        // Update Party_Slot
        switch (playerParty.Count)
        {
            case 1:
                UpdatePartySlotUI(UI_controller.partySlot1, newMonster);
                break;
            case 2:
                UpdatePartySlotUI(UI_controller.partySlot2, newMonster);
                break;
            case 3:
                UpdatePartySlotUI(UI_controller.partySlot3, newMonster);
                break;
            case 4:
                UpdatePartySlotUI(UI_controller.partySlot4, newMonster);
                break;
            case 5:
                UpdatePartySlotUI(UI_controller.partySlot5, newMonster);
                break;
            case 6:
                UpdatePartySlotUI(UI_controller.partySlot6, newMonster);
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
                case "ID":
                    text.text = monster.speciesInfo.ID.ToString();
                    break;
                case "Nickname":
                    if (monster.speciesInfo.name == null)
                    {
                        text.text = monster.speciesInfo.species.ToString();
                    }
                    else
                    {
                        text.text = monster.speciesInfo.name;
                    }
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

    public void StartBattle()
    {
        if (playerParty.Count == 0)
        {
            Debug.Log("No monsters in the player's party!");
            return;
        }

        //Optional, battle type here. How many enemies, etc.
        // Randomly select a battle type
        currentBattleType = (BattleType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BattleType)).Length);

        // Enable the battle scene
        variables.isInABattle = true;
        variables.canPlayerInteract = false;
        Debug.Log("CanInteract: " + variables.canPlayerInteract);
        Debug.Log("InABattle: " + variables.isInABattle);
        UI_controller.BattleUI.gameObject.SetActive(true);
        BattleScene.gameObject.SetActive(true);

        // Handle different battle types
        switch (currentBattleType)
        {
            case BattleType.BattleType_1v1:
                Start1v1Battle();   //Wild Encounters.
                break;

            case BattleType.BattleType_1v2:
                Start1v2Battle();   //Wild Encounters.
                break;

            default:
                Debug.LogError("Unknown battle type!");
                break;
        }
    }

    private void SendOutPlayersMonster(SpawnedMonster monster)
    {
        SpawnPoint = new Vector3(0.1f, 0.1f, 0f);  //Set the spawn position.
        playerMonsterInstance = Instantiate(playerMonsterPrefab, SpawnPoint, Quaternion.identity);
        PlayerMonsterController playerMonsterController = playerMonsterInstance.GetComponent<PlayerMonsterController>();

        if (playerMonsterController != null)
        {
            playerMonsterController.InitializeMonster(monster);
            HandleSendOutMonsterText(monster);
        }
        else
        {
            Debug.LogError("PlayerMonsterController component is missing on the Player_Monster_Prefab!");
        }
    }

    private void HandleSendOutMonsterText(SpawnedMonster monster)
    {
        Debug.Log($"Player's monster {monster.speciesInfo.species} has been sent out!");
        UI_controller.BattleUI_EncounterText.gameObject.SetActive(true);
        TextMeshProUGUI[] texts = UI_controller.BattleUI_EncounterText.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            switch (text.name)
            {
                case "EncounterText":
                    text.text = "I choose you " + monster.speciesInfo.species.ToString() + "!";
                    break;
            }
        }
    }


    public void Start1v1Battle()
    {
        // Spawn one enemy
        SpawnPoint = new Vector3(2.1f, 0.9f, 0f);  //Set the spawn position.
        SpawnedMonster enemyMonsterData1 = generate_Monster.SpawnMonster(); // Generate a new monster to spawn
        UpdatePartySlotUI(UI_controller.enemySlot1, enemyMonsterData1);
        currentEnemy = Instantiate(enemyMonsterPrefab, SpawnPoint, Quaternion.identity); // Instantiate the enemy prefab at the spawn point

        // Assign the spawned monster's data to the enemy GameObject
        EnemyController enemyController = currentEnemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.InitializeEnemy(enemyMonsterData1);
        }
        else
        {
            Debug.LogError("EnemyController component is missing on the enemy prefab!");
        }

        //Play battle music.
        //Battle transition.
        //Set the background
        Background.gameObject.SetActive(true); //Change the background sprite later.
        //Wait Transtion
        //Play Monster entry animations.
        //Play cry Monster Sound effect.

        // Create a list of enemy monsters
        List<SpawnedMonster> enemyMonsters = new List<SpawnedMonster> { enemyMonsterData1};
        // Start the coroutine to handle the encounter text for both enemies
        StartCoroutine(HandleEncounterText(enemyMonsters));
    }

    public void Start1v2Battle()
    {
        // Clear the list of spawned enemies
        spawnedEnemies.Clear();

        // Spawn two enemies
        SpawnedMonster enemyMonsterData1 = generate_Monster.SpawnMonster();
        SpawnedMonster enemyMonsterData2 = generate_Monster.SpawnMonster();

        // Spawn the first enemy
        SpawnPoint = new Vector3(2.6f, 0.7f, 0f);  //Set the spawn position.
        currentEnemy = Instantiate(enemyMonsterPrefab, SpawnPoint, Quaternion.identity);
        EnemyController enemyController1 = currentEnemy.GetComponent<EnemyController>();
        if (enemyController1 != null)
        {
            enemyController1.InitializeEnemy(enemyMonsterData1);
        }
        else
        {
            Debug.LogError("EnemyController component is missing on the enemy prefab!");
        }
        UpdatePartySlotUI(UI_controller.enemySlot1, enemyMonsterData1);

        // Spawn the second enemy
        SpawnPoint = new Vector3(1.4f, 0.9f, 0f);  //Set the spawn position.
        secondEnemy = Instantiate(enemyMonsterPrefab, SpawnPoint, Quaternion.identity); // Adjust position for second enemy (SpawnPoint + new Vector3(2, 0, 0))
        EnemyController enemyController2 = secondEnemy.GetComponent<EnemyController>();
        if (enemyController2 != null)
        {
            enemyController2.InitializeEnemy(enemyMonsterData2);
        }
        else
        {
            Debug.LogError("EnemyController component is missing on the second enemy prefab!");
        }
        UpdatePartySlotUI(UI_controller.enemySlot2, enemyMonsterData2);
        Background.gameObject.SetActive(true); //Change the background sprite later.
        List<SpawnedMonster> enemyMonsters = new List<SpawnedMonster> { enemyMonsterData1, enemyMonsterData2 };
        StartCoroutine(HandleEncounterText(enemyMonsters));
    }

    private IEnumerator HandleEncounterText(List<SpawnedMonster> enemyMonsters)
    {
        UI_controller.BattleUI_EncounterText.gameObject.SetActive(true);
        TextMeshProUGUI[] texts = UI_controller.BattleUI_EncounterText.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            switch (text.name)
            {
                case "EncounterText":
                    // Create a dynamic message based on the number of enemies
                    string encounterMessage = "A wild ";
                    for (int i = 0; i < enemyMonsters.Count; i++)
                    {
                        encounterMessage += enemyMonsters[i].speciesInfo.species;
                        if (i < enemyMonsters.Count - 1)
                        {
                            encounterMessage += " and ";
                        }
                    }
                    encounterMessage += " have appeared!";
                    text.text = encounterMessage;
                    break;
            }
        }
        yield return new WaitForSeconds(2f);    // Wait for x seconds.
        UI_controller.BattleUI_EncounterText.gameObject.SetActive(false);   // Disable the Encounter Text
        
        if (playerParty != null && playerParty.Count > 0)   // Check if the player's party has at least one monster
        {
            SpawnedMonster playersMonster = playerParty[0]; // Get the first monster in the player's party
            SendOutPlayersMonster(playersMonster);  
            yield return new WaitForSeconds(2f);
            UI_controller.BattleUI_EncounterText.gameObject.SetActive(false);
            SetInitialTarget(); // Set the initial target to the first monster in the list when the battle begins
            TurnbasedSystem.BeginTurns();
        }
        else
        {
            Debug.LogError("Player's party is empty! No monster to send out.");
        }
    }



    public void CheckIfEndBattle()
    {
        if (spawnedEnemies.Count == 0)  //WIN
        {
            if (currentEnemy != null)
            {
                Destroy(currentEnemy);
                currentEnemy = null;
                ClearPartySlotUI(UI_controller.enemySlot1);
            }
            if (secondEnemy != null)
            {
                Destroy(secondEnemy);
                secondEnemy = null;
                ClearPartySlotUI(UI_controller.enemySlot2);
            }
            if (playerMonsterInstance != null)
            {
                Destroy(playerMonsterInstance);
                playerMonsterInstance = null;
            }
            variables.isInABattle = false;
            Debug.Log("InABattle: " + variables.isInABattle);
            BattleScene.gameObject.SetActive(false);
            UI_controller.BattleUI.gameObject.SetActive(false);
            UI_controller.BattleUI_FightMenu.gameObject.SetActive(false);
            Background.gameObject.SetActive(false);
            UI_controller.ResetFightMenuSelection();
            Debug.Log("Battle ended! You Won!");
        }
        //else if (spawnedEnemies.Count > 0 && playerParty.Count == 0)  //LOSE        //Need to adjust playerpartycount later for checking if hp is 0.
        //{
        //    variables.isInABattle = false;
        //    Debug.Log("InABattle: " + variables.isInABattle);
        //    BattleScene.gameObject.SetActive(false);
        //    UI_controller.BattleUI.gameObject.SetActive(false);
        //    UI_controller.BattleUI_FightMenu.gameObject.SetActive(false);
        //    Background.gameObject.SetActive(false);
        //    Debug.Log("Battle ended! You Lost!");
        //}
        //Debug.Log("The Battle continues!");

    }

    public void ClearPlayerParty()
    {
        playerParty.Clear();
        Debug.Log("Player's party cleared!");
        // Clear Party_Slot UI
        ClearPartySlotUI(UI_controller.partySlot1);
        ClearPartySlotUI(UI_controller.partySlot2);
        ClearPartySlotUI(UI_controller.partySlot3);
        ClearPartySlotUI(UI_controller.partySlot4);
        ClearPartySlotUI(UI_controller.partySlot5);
        ClearPartySlotUI(UI_controller.partySlot6);
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
            || (text.text != "Party Slot 6")
            || (text.text != "Enemy Slot 1"))
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



    



    public void SwapMonstersInParty(int index1, int index2)
    {
        // Validate indices
        if (index1 < 0 || index1 >= playerParty.Count || index2 < 0 || index2 >= playerParty.Count)
        {
            Debug.LogError("Invalid indices for swapping monsters in the party.");
            return;
        }

        // Swap the monsters in the playerParty list
        SpawnedMonster temp = playerParty[index1];
        playerParty[index1] = playerParty[index2];
        playerParty[index2] = temp;

        UpdatePartyInterface();
    }

    private void UpdatePartyInterface()
    {
        if (UI_controller != null)
        {
            // Convert playerParty to List<GameObject> for compatibility
            List<GameObject> playerPartyGameObjects = new List<GameObject>();
            // Call the UpdatePartyUI method in UI_Controller
            UI_controller.SendMessage("UpdatePartyUI", playerPartyGameObjects);
        }
        else
        {
            Debug.LogError("UI_controller is not assigned.");
        }
    }



    
    


}
