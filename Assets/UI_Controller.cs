// 5/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager
    public Test_Generate_Monster test_Generate_Monster; 
    public Variables variables;

    public GameObject BattleUI;
    public GameObject BattleUI_BattleInfo;
    public GameObject BattleUI_EncounterText;
    public GameObject partyMenuCanvas; // Instance of the PartyMenu_UI Canvas
    public GameObject partySlot1; // Reference to the Party_Slot_1 GameObject
    public GameObject partySlot2; // Reference to the Party_Slot_2 GameObject
    public GameObject partySlot3; // Reference to the Party_Slot_3 GameObject
    public GameObject partySlot4; // Reference to the Party_Slot_4 GameObject
    public GameObject partySlot5; // Reference to the Party_Slot_5 GameObject
    public GameObject partySlot6; // Reference to the Party_Slot_6 GameObject
    public GameObject enemySlot1; // Reference to the Enemy_Slot_1 GameObject
    public GameObject enemySlot2; // Reference to the Enemy_Slot_2 GameObject

    //Options Menu
    public GameObject BattleUI_OptionMenu;

    // Fight Menu
    public GameObject BattleUI_FightMenu;
    public GameObject FightMenu_Move_Slot_1;
    public GameObject FightMenu_Move_Slot_2;
    public GameObject FightMenu_Move_Slot_3;
    public GameObject FightMenu_Move_Slot_4;

    public int selectedMoveIndex = 0; // Tracks the currently selected move in the fight menu
    private List<GameObject> fightMenuSlots;
    private Dictionary<string, int> monsterLastMoveIndex = new Dictionary<string, int>(); // Dictionary to store last move index for each monster



    void Start()
    {
        variables.canPlayerInteract = true;
        Debug.Log("CanInteract: " + variables.canPlayerInteract);

        if (partyMenuCanvas != null)
        {
            partyMenuCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("PartyMenuCanvas is not assigned!");
        }

        // Initialize fight menu slots
        fightMenuSlots = new List<GameObject>
        {
            FightMenu_Move_Slot_1,
            FightMenu_Move_Slot_2,
            FightMenu_Move_Slot_3,
            FightMenu_Move_Slot_4
        };
    }

    void Update()
    {
        if (canYouOpenAMenu())
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (partyMenuCanvas.activeSelf) // Check if the GameObject is active
                {
                    TogglePartyMenu();
                }
                else
                {
                    TogglePartyMenu();
                }
            }
            if (partyMenuCanvas.activeSelf) // Check if the GameObject is active
            {
                if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    gameManager.AddMonsterToParty();
                }
                if (Keyboard.current.qKey.wasPressedThisFrame)
                {
                    gameManager.ClearPlayerParty();
                }

                if (Keyboard.current.oKey.wasPressedThisFrame)
                {
                    //gameManager.SwapMonstersInParty(0, 1); // Swaps the first and second monsters in the party
                }
            }
        }
        // BattleScene
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (!variables.isInAMenu)
            {
                ToggleBattleScene();
            }
        }

        if (variables.isInABattle)
        {
            //if (Keyboard.current.sKey.wasPressedThisFrame )//&& gameManager.variables.canPlayerInteract)
            //{
            //    gameManager.targetingSystem.SelectNextTarget();
            //}
            //else if (Keyboard.current.aKey.wasPressedThisFrame )//&& gameManager.variables.canPlayerInteract)
            //{
            //    gameManager.targetingSystem.SelectPreviousTarget();
            //}
            if (Keyboard.current.spaceKey.wasPressedThisFrame )//&& gameManager.variables.canPlayerInteract)
            {
                GameObject currentTarget = gameManager.targetingSystem.GetCurrentTarget();
                if (currentTarget != null)
                {
                    Debug.Log($"Current target is: {currentTarget.name}");
                }
            }

            // Check if the I key is pressed to deal damage to the selected target
            //if (Keyboard.current.iKey.wasPressedThisFrame && gameManager.variables.canPlayerInteract)
            //{
            //    gameManager.variables.canPlayerInteract = false;
            //    Debug.Log("CanInteract: " + gameManager.variables.canPlayerInteract);
//
            //    // Deal damage to the selected target
            //    if (gameManager.spawnedEnemies.Count > 0)
            //    {
            //        MonsterController selectedEnemy = gameManager.spawnedEnemies[gameManager.selectedTargetIndex];
            //        selectedEnemy.TakeDamage(1000);
            //    }
            //}
        }
    }

    public void TogglePartyMenu()
    {
        if (partyMenuCanvas != null)
        {
            if (partyMenuCanvas.activeSelf)
            {
                partyMenuCanvas.SetActive(false);
                variables.isInAMenu = false;
                Debug.Log("Closed the Party Menu.");
            }
            else
            {
                partyMenuCanvas.SetActive(true);
                variables.isInAMenu = true;
                Debug.Log("Opened the Party Menu.");
            }
        }
        else
        {
            Debug.LogError("PartyMenu instance is null!");
        }
    }

    public void ToggleBattleScene()
    {
        // Battle scene
        // If you are not in a battle and can interact
        if ((!variables.isInABattle) && variables.canPlayerInteract)
        {
            gameManager.Trigger_WildEncounter();
            //gameManager.StartBattle();
        }
        // If you are in a battle and can interact
        if ((variables.isInABattle) && variables.canPlayerInteract)
        {
            //gameManager.spawnedEnemies.Clear();
            //gameManager.CheckIfEndBattle();
        }
    }

    public bool canYouOpenAMenu()
    {
        // If you are in a battle, you cannot open the menu
        if ((variables.isInABattle) || !variables.canPlayerInteract)
        {
            return false;
        }
        return true;
    }

    public void UpdatePartyUI(List<GameObject> playerParty)
    {
        // Array of party slots
        GameObject[] partySlots = { partySlot1, partySlot2, partySlot3, partySlot4, partySlot5, partySlot6 };

        if (partySlots.Length < playerParty.Count)
        {
            Debug.LogError("Not enough party slots to display all monsters in the party.");
            return;
        }

        for (int i = 0; i < partySlots.Length; i++)
        {
            if (i < playerParty.Count)
            {
                GameObject monster = playerParty[i];
                Text slotText = partySlots[i].GetComponentInChildren<Text>();
                Image slotImage = partySlots[i].GetComponentInChildren<Image>();

                if (slotText != null)
                {
                    slotText.text = monster.name; // Display the monster's name
                }
                else
                {
                    Debug.LogError($"Party slot {i + 1} does not have a Text component.");
                }

                if (slotImage != null)
                {
                    Sprite monsterSprite = monster.GetComponent<SpriteRenderer>()?.sprite;
                    if (monsterSprite != null)
                    {
                        slotImage.sprite = monsterSprite; // Display the monster's sprite
                    }
                    else
                    {
                        Debug.LogError($"Monster {monster.name} does not have a SpriteRenderer component or sprite.");
                    }
                }
                else
                {
                    Debug.LogError($"Party slot {i + 1} does not have an Image component.");
                }
            }
            else
            {
                // Clear unused slots
                Text slotText = partySlots[i].GetComponentInChildren<Text>();
                Image slotImage = partySlots[i].GetComponentInChildren<Image>();

                if (slotText != null)
                {
                    slotText.text = "";
                }

                if (slotImage != null)
                {
                    slotImage.sprite = null;
                }
            }
        }
    }

    public void DebugHighlightedMoveInfo()
    {
        // Ensure the selectedMoveIndex is within bounds
        if (selectedMoveIndex >= 0 && selectedMoveIndex < fightMenuSlots.Count)
        {
            // Get the currently highlighted slot
            GameObject selectedSlot = fightMenuSlots[selectedMoveIndex];
            MoveSlot moveSlot = selectedSlot.GetComponent<MoveSlot>();

            if (moveSlot != null && moveSlot.moveInfo != null)
            {
                // Log the move information
                Debug.Log($"Highlighted Move Info: {moveSlot.moveInfo.name}");
            }
            else
            {
                Debug.LogWarning("Highlighted slot does not have a valid MoveSlot component or moveInfo is null!");
            }
        }
        else
        {
            Debug.LogWarning("Selected move index is out of bounds!");
        }
    }
    
    public void NavigateFightMenu(int direction)
    {
        // Update the selected move index based on the direction (-1 for up, 1 for down)
        do
        {
            selectedMoveIndex += direction;

            // Ensure the index stays within bounds
            if (selectedMoveIndex < 0)
            {
                selectedMoveIndex = fightMenuSlots.Count - 1;
            }
            else if (selectedMoveIndex >= fightMenuSlots.Count)
            {
                selectedMoveIndex = 0;
            }

            // Check if the selected slot contains a valid move
            MoveSlot moveSlot = fightMenuSlots[selectedMoveIndex].GetComponent<MoveSlot>();
            if (moveSlot != null && moveSlot.moveInfo != null && moveSlot.moveInfo.move != Move.NONE)
            {
                //DebugHighlightedMoveInfo(); // Debug the highlighted move info
                break; // Valid move found, exit the loop
            }
        } while (true);

        // Highlight the selected move slot by changing the text color
        for (int i = 0; i < fightMenuSlots.Count; i++)
        {
            TextMeshProUGUI slotText = fightMenuSlots[i].GetComponentInChildren<TextMeshProUGUI>();
            if (slotText != null)
            {
                if (i == selectedMoveIndex)
                {
                    slotText.color = Color.yellow; // Highlight selected slot
                    
                }
                else
                {
                    slotText.color = Color.white; // Reset color for other slots
                }
            }
            else
            {
                Debug.LogError($"Fight menu slot {i + 1} does not have a TextMeshProUGUI component!");
            }
        }
    }

    public MoveInformation GetMoveInformation(Move move)
    {
        if (test_Generate_Monster.moveDatabase == null)
        {
            Debug.LogError("MoveDatabase is not assigned in TurnBasedBattleSystem!");
            return null;
        }

        if (test_Generate_Monster.moveDatabase.moves == null || test_Generate_Monster.moveDatabase.moves.Count == 0)
        {
            Debug.LogError("MoveDatabase does not contain any moves!");
            return null;
        }

        foreach (MoveInformation moveInfo in test_Generate_Monster.moveDatabase.moves)
        {
            if (moveInfo.move == move)
            {
                return moveInfo;
            }
        }

        Debug.LogError($"Move {move} not found in the MoveDatabase!");
        return null;
    }

    public void PopulateFightMenu(Monster.Moves monsterMoves)
    {
        // Assign move information to each fight menu slot
        AssignMoveToSlot(FightMenu_Move_Slot_1, GetMoveInformation(monsterMoves.MOVE_1));
        AssignMoveToSlot(FightMenu_Move_Slot_2, GetMoveInformation(monsterMoves.MOVE_2));
        AssignMoveToSlot(FightMenu_Move_Slot_3, GetMoveInformation(monsterMoves.MOVE_3));
        AssignMoveToSlot(FightMenu_Move_Slot_4, GetMoveInformation(monsterMoves.MOVE_4));
    }
    
    private void AssignMoveToSlot(GameObject slot, MoveInformation moveInfo)
    {
        // Assign the move information to the MoveSlot component
        MoveSlot moveSlot = slot.GetComponent<MoveSlot>();
        if (moveSlot != null)
        {
            moveSlot.moveInfo = moveInfo;

            // Update the TextMeshProUGUI text with the move name
            TextMeshProUGUI slotText = slot.GetComponentInChildren<TextMeshProUGUI>();
            if (slotText != null && moveInfo != null)
            {
                if (moveInfo.move != Move.NONE)
                {
                    slotText.text = moveInfo.name;
                }
                else
                {
                    slotText.text = "Unavailable"; // Display "Unavailable" for Move.None
                    moveSlot.moveInfo = null; // Prevent selection of this move
                }
            }
            else
            {
                Debug.LogError("Move slot does not have a TextMeshProUGUI component or moveInfo is null!");
            }
        }
        else
        {
            Debug.LogError("MoveSlot component is missing on the fight menu slot!");
        }
    }



    public MoveInformation GetSelectedMove()
    {
        // Get the MoveInformation from the currently selected move slot
        GameObject selectedSlot = fightMenuSlots[selectedMoveIndex];
        return selectedSlot.GetComponent<MoveSlot>().moveInfo; // Ensure MoveSlot script exists and has a moveInfo property
    }

    public void ResetFightMenuSelection()
    {
        // Reset the selected move index to the first slot
        selectedMoveIndex = 0;

        // Highlight the first move slot
        for (int i = 0; i < fightMenuSlots.Count; i++)
        {
            TextMeshProUGUI slotText = fightMenuSlots[i].GetComponentInChildren<TextMeshProUGUI>();
            if (slotText != null)
            {
                if (i == selectedMoveIndex)
                {
                    slotText.color = Color.yellow; // Highlight the first slot
                }
                else
                {
                    slotText.color = Color.white; // Reset color for other slots
                }
            }
            else
            {
                Debug.LogError($"Fight menu slot {i + 1} does not have a TextMeshProUGUI component!");
            }
        }
    }




    
    public void HighlightOption(string optionName, GameObject UIMenu)
    {
        // Find the parent Options_Menu GameObject
        //GameObject optionsMenu = GameObject.Find("Options_Menu");

        if (UIMenu == null)
        {
            Debug.LogError("Options_Menu not found in the scene.");
            return;
        }

        // Loop through all child objects of Options_Menu
        foreach (Transform child in UIMenu.transform)
        {
            // Check if the child name matches the optionName
            if (child.name == optionName)
            {
                // Highlight the selected option (e.g., change color or scale)
                var textComponent = child.GetComponent<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.color = Color.yellow; // Change text color to yellow
                }
            }
            else
            {
                // Reset the color of other options
                var textComponent = child.GetComponent<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.color = Color.white; // Change text color to white
                }
            }
        }
    }

    public void RememberLastMove(Monster monster, int moveIndex)
    {
        if (monster != null)
        {
            string monsterId = monster.monsterSpeciesInfo.NICKNAME; // Use the monster's name or unique ID as the key
            if (monsterLastMoveIndex.ContainsKey(monsterId))
            {
                monsterLastMoveIndex[monsterId] = moveIndex; // Update the last move index
            }
            else
            {
                monsterLastMoveIndex.Add(monsterId, moveIndex); // Add a new entry for the monster
            }
        }
    }

    public void HighlightLastMove(Monster monster)
    {
        if (monster != null)
        {
            string monsterId = monster.monsterSpeciesInfo.NICKNAME; // Use the monster's name or unique ID as the key
            if (monsterLastMoveIndex.ContainsKey(monsterId))
            {
                selectedMoveIndex = monsterLastMoveIndex[monsterId]; // Retrieve the last move index for the monster
            }
            else
            {
                selectedMoveIndex = 0; // Default to the first slot if no previous move is found
            }

            // Highlight the selected move slot
            for (int i = 0; i < fightMenuSlots.Count; i++)
            {
                TextMeshProUGUI slotText = fightMenuSlots[i].GetComponentInChildren<TextMeshProUGUI>();
                if (slotText != null)
                {
                    if (i == selectedMoveIndex)
                    {
                        slotText.color = Color.yellow; // Highlight selected slot
                    }
                    else
                    {
                        slotText.color = Color.white; // Reset color for other slots
                    }
                }
                else
                {
                    Debug.LogError($"Fight menu slot {i + 1} does not have a TextMeshProUGUI component!");
                }
            }
        }
    }





    

}