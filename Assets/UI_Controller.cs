using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;



public class UI_Controller : MonoBehaviour
{
    
    public GameManager gameManager; // Reference to the GameManager
    public Variables variables;

    public GameObject BattleUI;
    public GameObject BattleUI_FightMenu;
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
    }

    void Update()
    {
        if (canYouOpenAMenu()) {   //Checks to see if you can open a menu.
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
            }
        }
        //BattleScene
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (!variables.isInAMenu)
            {
                ToggleBattleScene();
            }
        }


        if (variables.isInABattle)
        {
            if (Keyboard.current.uKey.wasPressedThisFrame && gameManager.variables.canPlayerInteract)
            {
                gameManager.SwapTarget();
            }

            // Check if the I key is pressed to deal damage to the selected target
            if (Keyboard.current.iKey.wasPressedThisFrame && gameManager.variables.canPlayerInteract)
            {
                gameManager.variables.canPlayerInteract = false;
                Debug.Log("CanInteract: " + gameManager.variables.canPlayerInteract);

                // Deal damage to the selected target
                if (gameManager.spawnedEnemies.Count > 0)
                {
                    EnemyController selectedEnemy = gameManager.spawnedEnemies[gameManager.selectedTargetIndex];
                    selectedEnemy.TakeDamage(1000);
                }
            }
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
    //Battle scene
        //if you are not in a battle. and can interact.
        if ((!variables.isInABattle)
        && variables.canPlayerInteract) 
        {
            gameManager.StartBattle();
        }
        //if you are in a battle. and can interact.
        if ((variables.isInABattle)
        && variables.canPlayerInteract)
        {
            gameManager.spawnedEnemies.Clear();
            gameManager.CheckIfEndBattle();
        }
    }


    public bool canYouOpenAMenu()
    {
        //If you are in a battle you cannot open the menu.
        if ((variables.isInABattle) 
        || !variables.canPlayerInteract)
        {
            return false;
        }
        return true;
    }

    


}