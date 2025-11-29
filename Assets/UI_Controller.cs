using UnityEngine;
using UnityEngine.InputSystem;

public class UI_Controller : MonoBehaviour
{
    public GameObject PartyUI; // Reference to the Canvas component
    public GameManager gameManager;
    public Variables variables;
    

    void Update()
    {
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
}
