using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class UI_Controller : MonoBehaviour
{
    public GameObject PartyUI; // Reference to the Canvas component
    public GameObject BattleUI;
    public GameObject BattleUI_FightMenu;
    public GameObject BattleUI_BattleInfo;
    public GameObject BattleUI_EncounterText; // Start of battle messages.
    public GameManager gameManager;
    public Variables variables;
    public BattleType currentBattleType;

    public List<GameObject> spawnedEnemies = new List<GameObject>();
    
    
    void Start()
    {
        variables.canPlayerInteract = true;
    }


    void Update()
    {
        if (canYouOpenAMenu()) {   //Checks to see if you can open a menu.
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                // Use PartyUI.gameObject to access the GameObject
                if (PartyUI.gameObject.activeSelf) // Check if the GameObject is active
                {
                    PartyUI.gameObject.SetActive(false); // Disable the GameObject
                    variables.isInAMenu = false;
                    Debug.Log("Closed the Party Menu.");
                }
                else
                {
                    PartyUI.gameObject.SetActive(true); // Enable the GameObject
                    variables.isInAMenu = true;
                    Debug.Log("Opened the Party Menu.");
                }
            }
            if (PartyUI.gameObject.activeSelf) // Check if the GameObject is active
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


    //Battle scene
        if (!variables.isInAMenu)
        {
            //if you are not in a battle. and can interact.
            if ((!variables.isInABattle)
            && variables.canPlayerInteract) 
            {
                //Start the battle.
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {
                    gameManager.StartBattle();
                }
            }

            //if you are in a battle. and can interact.
            if ((variables.isInABattle)
            && variables.canPlayerInteract)
            {
                //End the battle.
                if (Keyboard.current.backspaceKey.wasPressedThisFrame)
                {
                    gameManager.EndBattle();
                }
                //If you are in a battle, press i to open/close battle information.
                if (Keyboard.current.iKey.wasPressedThisFrame)
                {
                    if (BattleUI_BattleInfo.gameObject.activeSelf) 
                    {
                        // Disable all enemy info slots dynamically based on the number of spawned enemies
                        foreach (var enemy in spawnedEnemies)
                        {
                            EnemyController enemyController = enemy.GetComponent<EnemyController>();
                            if (enemyController != null && enemyController.enemyInfoUI != null)
                            {
                                enemyController.enemyInfoUI.gameObject.SetActive(false);
                            }
                        }
                        Debug.Log("Closed Battle Information.");
                    }
                    else
                    {
                        // Enable all enemy info slots dynamically based on the number of spawned enemies
                        foreach (var enemy in spawnedEnemies)
                        {
                            EnemyController enemyController = enemy.GetComponent<EnemyController>();
                            if (enemyController != null && enemyController.enemyInfoUI != null)
                            {
                                enemyController.enemyInfoUI.gameObject.SetActive(true);
                            }
                        }
                        Debug.Log("Opened Battle Information.");
                    }
                }
            }
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