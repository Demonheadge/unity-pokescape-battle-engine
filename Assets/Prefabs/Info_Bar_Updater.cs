using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Info_Bar_Updater : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject infoBarPrefab; // Reference to the INFO_BAR prefab
    private GameObject infoBarInstance; // Instance of the INFO_BAR prefab
    private Slider healthBar; // Reference to the health bar slider
    private TextMeshProUGUI levelText; // Reference to the level text (TextMeshPro)
    private TextMeshProUGUI current_hp; // Reference to the HP text (TextMeshPro)
    private TextMeshProUGUI nameText; // Reference to the Name text (TextMeshPro)
    private static List<GameObject> activeInfoBars = new List<GameObject>(); // List to track active HP bars

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager instance not found in the scene!");
        }
    }

    public void InitializeInfoBar(SpawnedMonster monster)
    {
        // Ensure only one INFO_BAR prefab is instantiated
        if (infoBarInstance == null)
        {
            // Instantiate the INFO_BAR prefab and set it as a child of the enemy
            infoBarInstance = Instantiate(infoBarPrefab, transform);
            infoBarInstance.transform.localPosition = new Vector3(0, 0.8f, 0); // Adjust position above the enemy

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
            InitializeUI(monster);
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

    public void InitializeUI(SpawnedMonster monster)
    {
        if (monster != null)
        {
            UpdateUI(monster);
        }
        else
        {
            Debug.LogWarning("No enemy data available to initialize UI.");
        }
    }

    public void UpdateUI(SpawnedMonster monster)
    {
        if (monster != null)
        {
            // Set the name text using TextMeshPro
            nameText.text = monster.speciesInfo.species.ToString();
            // Update level text
            levelText.text = $"{monster.extra2Info.level}";
            // Initialize health bar
            healthBar.maxValue = monster.extra2Info.max_HP;
            healthBar.value = monster.extra2Info.current_HP;
            // Update HP text
            current_hp.text = $"{monster.extra2Info.current_HP}/{monster.extra2Info.max_HP}";
            // Update health bar color
            UpdateHealthBarColor((float)monster.extra2Info.current_HP / monster.extra2Info.max_HP);
            //Debug.Log("INFO BAR UI - Updated.");
        }
        else
        {
            Debug.LogWarning("No enemy data available to update UI.");
        }
    }


    public System.Collections.IEnumerator AnimateHealthBarAndText(SpawnedMonster monster, int startHP, int targetHP)
    {
        float elapsedTime = 0f;
        float startValue = startHP;
        float targetValue = targetHP;

        while (elapsedTime < gameManager.variables.HP_BAR_Speed_duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / gameManager.variables.HP_BAR_Speed_duration;

            // Update health bar value
            healthBar.value = Mathf.Lerp(startValue, targetValue, t);

            // Update HP text value
            int currentHPValue = Mathf.RoundToInt(Mathf.Lerp(startValue, targetValue, t));
            current_hp.text = $"{currentHPValue}/{monster.extra2Info.max_HP}";

            // Update health bar color gradually
            float currentHealthPercentage = Mathf.Lerp((float)startValue / monster.extra2Info.max_HP, (float)targetValue / monster.extra2Info.max_HP, t);
            UpdateHealthBarColor(currentHealthPercentage);

            yield return null;
        }

        // Ensure the final values are set
        healthBar.value = targetValue;
        current_hp.text = $"{targetHP}/{monster.extra2Info.max_HP}";
        UpdateHealthBarColor((float)targetHP / monster.extra2Info.max_HP);
        UpdateUI(monster); // Update the UI after the animation is complete

        // Check if the enemy's HP is 0 and call Die()
        if (targetHP <= 0)
        {
            StartCoroutine(ShrinkAndDie());
            Debug.Log(monster.speciesInfo.species.ToString() + " fainted!");
        }
        gameManager.variables.canPlayerInteract = true;
        Debug.Log("CanInteract: " + gameManager.variables.canPlayerInteract);
    }


    public System.Collections.IEnumerator ShrinkAndDie()
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
        gameManager.RemoveEnemy(this.GetComponent<EnemyController>()); // Notify GameManager to remove this enemy and update target
        gameManager.CheckIfEndBattle();

        Destroy(gameObject);
    }

    public void UpdateHealthBarColor(float currentHealthPercentage)
    {
        healthBar.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, currentHealthPercentage);
    }

    
}
