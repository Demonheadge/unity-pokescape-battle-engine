// 26/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class BattleUIManager : MonoBehaviour
{
    public TextMeshProUGUI fightText;
    public TextMeshProUGUI runText;
    public TextMeshProUGUI[] moveTexts; // Array for move options

    private int currentSelectionIndex = 0;
    private TextMeshProUGUI[] currentTextGroup;
    private bool isSelectingMove = false;
    private bool isSelectingEnemy = false;

    private bool chooseOptionsInteractable = false; // New variable to track interactivity

    public GameObject ChooseOptionUI;
    public GameObject ChooseMovesUI;
    public Canvas BattleSceneUI;

    private void Start()
    {
        // Set initial text group to action texts
        currentTextGroup = new TextMeshProUGUI[] { fightText, runText };
        HighlightText(currentSelectionIndex);
    }

    private void Update()
    {
        if (chooseOptionsInteractable)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            MoveSelection(-1);
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            MoveSelection(1);
        }
        else if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            ConfirmSelection();
        }
    }

    private void MoveSelection(int direction)
    {
        // Reset current text color to white
        currentTextGroup[currentSelectionIndex].color = Color.white;

        // Update selection index
        currentSelectionIndex += direction;
        if (currentSelectionIndex < 0) currentSelectionIndex = currentTextGroup.Length - 1;
        if (currentSelectionIndex >= currentTextGroup.Length) currentSelectionIndex = 0;

        // Highlight new text
        HighlightText(currentSelectionIndex);
    }

    private void HighlightText(int index)
    {
        currentTextGroup[index].color = Color.yellow;
    }

    private void ConfirmSelection()
    {
        TextMeshProUGUI selectedText = currentTextGroup[currentSelectionIndex];
        if (selectedText == fightText)
        {
            OnFightSelected();
        }
        else if (selectedText == runText)
        {
            OnRunSelected();
        }
        else
        {
            for (int i = 0; i < moveTexts.Length; i++)
            {
                if (selectedText == moveTexts[i])
                {
                    OnMoveSelected(i);
                    return;
                }
            }
        }
    }

    private void OnFightSelected()
    {
        Debug.Log("Fight selected!");
        ChooseOptionUI.gameObject.SetActive(false); //Turns off the Choose Option.
        ChooseMovesUI.gameObject.SetActive(true); //Turns on Choose Moves.
        isSelectingMove = true;
        currentTextGroup = moveTexts;
        currentSelectionIndex = 0;
        HighlightText(currentSelectionIndex);
    }

    private void OnRunSelected()
    {
        Debug.Log("Run selected!");
        BattleSceneUI.gameObject.SetActive(false); //Turns off the Battle UI.
        FindObjectOfType<BattleEngine>().PlayerAction("Run");
    }

    private void OnMoveSelected(int moveIndex)
    {
        Debug.Log($"Move {moveIndex + 1} selected!");
        isSelectingMove = false;
        isSelectingEnemy = true;
        currentSelectionIndex = 0;
        HighlightText(currentSelectionIndex);
        ChooseMovesUI.gameObject.SetActive(false); //Turns off the Move UI.
        //Excute move move. ( i )
    }

    private void OnEnemySelected(int enemyIndex)
    {
        Debug.Log($"Enemy {enemyIndex + 1} selected!");
        GameObject targetEnemy = FindObjectOfType<BattleEngine>().enemyMonsters[enemyIndex];
        FindObjectOfType<BattleEngine>().PlayerAction("Fight", targetEnemy);
        isSelectingEnemy = false;
        currentTextGroup = new TextMeshProUGUI[] { fightText, runText };
        currentSelectionIndex = 0;
        HighlightText(currentSelectionIndex);
    }

    public void SetChooseOptionsInteractable(bool interactable)
    {
        chooseOptionsInteractable = interactable;
        ChooseOptionUI.gameObject.SetActive(true);
    }
}