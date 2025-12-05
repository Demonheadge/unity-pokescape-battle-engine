// 4/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

// 3/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace
using System.Collections.Generic;

public class PlayerMonsterController : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager

    [SerializeField]
    public SpawnedMonster monsterData; // Holds the monster's data and makes it visible in the Inspector

    public GameObject infoBarPrefab; // Reference to the INFO_BAR prefab
    private GameObject infoBarInstance; // Instance of the INFO_BAR prefab
    private Slider healthBar; // Reference to the health bar slider
    private TextMeshProUGUI levelText; // Reference to the level text (TextMeshPro)
    private TextMeshProUGUI current_hp; // Reference to the HP text (TextMeshPro)
    private TextMeshProUGUI nameText; // Reference to the Name text (TextMeshPro)
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private static List<GameObject> activeInfoBars = new List<GameObject>(); // List to track active HP bars
    public float duration = 1f; // Duration of the hp bar animation

    private void Awake()
    {
        // Get the SpriteRenderer component from the monster GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on the monster prefab!");
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager instance not found in the scene!");
        }

        if (monsterData != null)
        {
            InitializeMonster(monsterData);
        }
        else
        {
            Debug.LogError("Monster data is not assigned!");
        }
    }

    public void InitializeMonster(SpawnedMonster data)
    {
        monsterData = data;

        // Set the sprite of the monster based on the SpawnedMonster data
        if (spriteRenderer != null && monsterData.speciesInfo.front_sprite != null)
        {
            spriteRenderer.sprite = monsterData.speciesInfo.front_sprite;
        }
        else
        {
            Debug.LogError("Failed to set monster sprite. Ensure the SpawnedMonster has a valid sprite.");
        }

        // Ensure only one INFO_BAR prefab is instantiated
        if (infoBarInstance == null)
        {
            // Instantiate the INFO_BAR prefab and set it as a child of the monster
            infoBarInstance = Instantiate(infoBarPrefab, transform);
            infoBarInstance.transform.localPosition = new Vector3(0, 0.8f, 0); // Adjust position above the monster

            // Check for collisions with other HP bars and adjust position
            AdjustInfoBarPosition(infoBarInstance);

            // Add the new HP bar to the list of active HP bars
            activeInfoBars.Add(infoBarInstance);

            // Get references to the UI components dynamically
            healthBar = infoBarInstance.transform.Find("HP_Bar_Slider").GetComponent<Slider>();
            levelText = infoBarInstance.transform.Find("Level").GetComponent<TextMeshProUGUI>();
            current_hp = infoBarInstance.transform.Find("CURRENT_HP").GetComponent<TextMeshProUGUI>();
            nameText = infoBarInstance.transform.Find("Name").GetComponent<TextMeshProUGUI>();

            // Initialize the UI
            InitializeUI();
        }
        else
        {
            Debug.LogWarning("INFO_BAR instance already exists!");
        }
    }

    private void AdjustInfoBarPosition(GameObject infoBar)
    {
        Vector3 originalPosition = infoBar.transform.localPosition;
        float offset = 0.1f; // Distance to move the HP bar upwards if it overlaps
        bool isColliding;

        do
        {
            isColliding = false;

            foreach (GameObject otherInfoBar in activeInfoBars)
            {
                if (otherInfoBar != infoBar && otherInfoBar != null)
                {
                    // Check if the current infoBar overlaps with another active infoBar
                    if (Mathf.Abs(infoBar.transform.localPosition.y - otherInfoBar.transform.localPosition.y) < offset)
                    {
                        isColliding = true;
                        infoBar.transform.localPosition += new Vector3(0, offset, 0); // Move the infoBar upwards
                        break;
                    }
                }
            }
        } while (isColliding);

        Debug.Log($"Adjusted position of HP bar from {originalPosition} to {infoBar.transform.localPosition}");
    }

    private void InitializeUI()
    {
        if (monsterData != null)
        {
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("No monster data available to initialize UI.");
        }
    }

    private void UpdateHealthBarColor(float currentHealthPercentage)
    {
        healthBar.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, currentHealthPercentage);
    }

    private void UpdateUI()
    {
        if (monsterData != null)
        {
            // Set the name text using TextMeshPro
            nameText.text = monsterData.speciesInfo.species.ToString();
            // Update level text
            levelText.text = $"{monsterData.extra2Info.level}";
            // Initialize health bar
            healthBar.maxValue = monsterData.extra2Info.max_HP;
            healthBar.value = monsterData.extra2Info.current_HP;
            // Update HP text
            current_hp.text = $"{monsterData.extra2Info.current_HP}/{monsterData.extra2Info.max_HP}";
            // Update health bar color
            UpdateHealthBarColor((float)monsterData.extra2Info.current_HP / monsterData.extra2Info.max_HP);
        }
        else
        {
            Debug.LogWarning("No monster data available to update UI.");
        }
    }

    public void TakeDamage(int damage)
    {
        if (monsterData != null)
        {
            int previousHP = monsterData.extra2Info.current_HP;
            monsterData.extra2Info.current_HP -= damage;
            monsterData.extra2Info.current_HP = Mathf.Clamp(monsterData.extra2Info.current_HP, 0, monsterData.extra2Info.max_HP);

            // Start coroutine to animate health bar and text
            StartCoroutine(AnimateHealthBarAndText(previousHP, monsterData.extra2Info.current_HP));

            Debug.Log(monsterData.speciesInfo.species.ToString() + " took " + damage + " damage.");
        }
    }

    private System.Collections.IEnumerator AnimateHealthBarAndText(int startHP, int targetHP)
    {
        float elapsedTime = 0f;
        float startValue = startHP;
        float targetValue = targetHP;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Update health bar value
            healthBar.value = Mathf.Lerp(startValue, targetValue, t);

            // Update HP text value
            int currentHPValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, t));
            current_hp.text = $"{currentHPValue}/{monsterData.extra2Info.max_HP}";

            // Update health bar color gradually
            float currentHealthPercentage = Mathf.Lerp((float)startValue / monsterData.extra2Info.max_HP, (float)targetValue / monsterData.extra2Info.max_HP, t);
            UpdateHealthBarColor(currentHealthPercentage);

            yield return null;
        }

        // Ensure the final values are set
        healthBar.value = targetValue;
        current_hp.text = $"{targetHP}/{monsterData.extra2Info.max_HP}";
        UpdateHealthBarColor((float)targetHP / monsterData.extra2Info.max_HP);
        UpdateUI(); // Update the UI after the animation is complete

        // Check if the monster's HP is 0 and call Die()
        if (targetHP <= 0)
        {
            StartCoroutine(ShrinkAndDie());
            Debug.Log(monsterData.speciesInfo.species.ToString() + " fainted!");
        }
        gameManager.variables.canPlayerInteract = true;
        Debug.Log("CanInteract: " + gameManager.variables.canPlayerInteract);
    }

    private System.Collections.IEnumerator ShrinkAndDie()
    {
        float shrinkDuration = 0.5f;
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;

        while (elapsedTime < shrinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / shrinkDuration;

            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);

            yield return null;
        }

        transform.localScale = Vector3.zero;

        //gameManager.RemoveEnemy(this); // Notify GameManager to remove this monster and update target

        gameManager.CheckIfEndBattle();

        Destroy(gameObject);
    }
}