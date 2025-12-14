using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager
    public Info_Bar_Updater Info_Bar_Updater;
    
    [SerializeField]
    public SpawnedMonster monsterData; // Holds the monsterData's data and makes it visible in the Inspector
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private Coroutine bopCoroutine; // To manage the bop animation
    private Vector3 turnStartPosition; // Store the position of the monster at the start of its turn
    private Coroutine attackCoroutine; // To manage the attack animation


    private void Awake()
    {
        // Get the SpriteRenderer component from the enemy GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on the enemy prefab!");
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
            Debug.LogError("Enemy data is not assigned!");
        }
    }
    
    // Method to flip the sprite horizontally
    public void FlipSprite(bool shouldFlip)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = shouldFlip;
        }
        else
        {
            Debug.LogError("SpriteRenderer component is not assigned!");
        }
    }


    public void InitializeMonster(SpawnedMonster data)
    {
        monsterData = data;

        // Set the sprite of the enemy based on the SpawnedMonster data
        if (spriteRenderer != null && monsterData.speciesInfo.front_sprite != null)
        {
            spriteRenderer.sprite = monsterData.speciesInfo.front_sprite;
        }
        else
        {
            Debug.LogError("Failed to set enemy sprite. Ensure the SpawnedMonster has a valid sprite.");
        }
        Info_Bar_Updater.InitializeInfoBar(monsterData);
    }

    
    public IEnumerator PerformJumpAnimation()
    {
        Transform monsterTransform = transform;

        // Perform a simple jump animation
        Vector3 originalPosition = monsterTransform.position;
        Vector3 jumpUpPosition = originalPosition + new Vector3(0, 0.5f, 0); // Adjust the jump height as needed

        float jumpDuration = 0.2f; // Duration of the jump
        float elapsedTime = 0;

        // Move up
        while (elapsedTime < jumpDuration)
        {
            monsterTransform.position = Vector3.Lerp(originalPosition, jumpUpPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        monsterTransform.position = jumpUpPosition;

        // Move down
        elapsedTime = 0;
        while (elapsedTime < jumpDuration)
        {
            monsterTransform.position = Vector3.Lerp(jumpUpPosition, originalPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        monsterTransform.position = originalPosition;
    }

    public void StartBopAnimation()
    {
        if (bopCoroutine != null)
        {
            StopCoroutine(bopCoroutine);
        }

        // Store the current position as the turn start position
        turnStartPosition = transform.position;

        bopCoroutine = StartCoroutine(PerformBopAnimation());
    }

    public void StopBopAnimation()
    {
        if (bopCoroutine != null)
        {
            StopCoroutine(bopCoroutine);
            bopCoroutine = null;

            // Reset position to turn start position
            transform.position = turnStartPosition;
        }
    }

    public IEnumerator PerformBopAnimation()
    {
        Transform monsterTransform = transform;

        // Perform a subtle bop animation
        Vector3 bopUpPosition = turnStartPosition + new Vector3(0, 0.1f, 0); // Adjust the bop height to be subtle
        float bopDuration = 0.3f; // Duration of each bop

        while (true)
        {
            float elapsedTime = 0;

            // Move up
            while (elapsedTime < bopDuration / 2)
            {
                monsterTransform.position = Vector3.Lerp(turnStartPosition, bopUpPosition, elapsedTime / (bopDuration / 2));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            monsterTransform.position = bopUpPosition;

            // Move down
            elapsedTime = 0;
            while (elapsedTime < bopDuration / 2)
            {
                monsterTransform.position = Vector3.Lerp(bopUpPosition, turnStartPosition, elapsedTime / (bopDuration / 2));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            monsterTransform.position = turnStartPosition;
        }
    }

    /*public IEnumerator PerformAttackAnimation(Transform targetTransform)
    {
        Transform attackerTransform = transform;

        // Get the original position of the attacker
        Vector3 originalPosition = attackerTransform.position;

        // Calculate the target position for the tackle/slide animation
        Vector3 tacklePosition = Vector3.Lerp(originalPosition, targetTransform.position, 0.5f); // Move halfway towards the target

        float tackleDuration = 0.3f; // Duration of the tackle
        float returnDuration = 0.3f; // Duration of the return
        float elapsedTime = 0;

        // Move forward (tackle)
        while (elapsedTime < tackleDuration)
        {
            attackerTransform.position = Vector3.Lerp(originalPosition, tacklePosition, elapsedTime / tackleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        attackerTransform.position = tacklePosition;

        // Perform attack (you can add attack effects or logic here)
        yield return new WaitForSeconds(0.2f); // Small delay to simulate the attack

        // Move back to the original position (slide back)
        elapsedTime = 0;
        while (elapsedTime < returnDuration)
        {
            attackerTransform.position = Vector3.Lerp(tacklePosition, originalPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        attackerTransform.position = originalPosition;
    }*/

    public IEnumerator PerformHitReactionAnimation()
    {
        Transform monsterTransform = transform;

        // Perform a simple hit reaction animation
        Vector3 originalPosition = monsterTransform.position;
        Vector3 hitBackPosition = originalPosition - new Vector3(0.2f, 0, 0); // Adjust the hit back distance as needed

        float hitDuration = 0.2f; // Duration of the hit reaction
        float elapsedTime = 0;

        // Move back
        while (elapsedTime < hitDuration)
        {
            monsterTransform.position = Vector3.Lerp(originalPosition, hitBackPosition, elapsedTime / hitDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        monsterTransform.position = hitBackPosition;

        // Move forward to the original position
        elapsedTime = 0;
        while (elapsedTime < hitDuration)
        {
            monsterTransform.position = Vector3.Lerp(hitBackPosition, originalPosition, elapsedTime / hitDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        monsterTransform.position = originalPosition;
    }

    

    

    public void StartAttackAnimation(Transform targetTransform)
    {
        // If an attack animation is already running, stop it
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        // Start the attack animation coroutine
        attackCoroutine = StartCoroutine(PerformAttackAnimation(targetTransform));
    }

    public IEnumerator PerformAttackAnimation(Transform targetTransform)
    {
        Transform attackerTransform = transform;

        // Get the original position of the attacker
        Vector3 originalPosition = attackerTransform.position;

        // Calculate the target position for the tackle/slide animation
        Vector3 tacklePosition = Vector3.Lerp(originalPosition, targetTransform.position, 0.5f); // Move halfway towards the target

        float tackleDuration = 0.3f; // Duration of the tackle
        float returnDuration = 0.3f; // Duration of the return
        float elapsedTime = 0;

        // Move forward (tackle)
        while (elapsedTime < tackleDuration)
        {
            attackerTransform.position = Vector3.Lerp(originalPosition, tacklePosition, elapsedTime / tackleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        attackerTransform.position = tacklePosition;

        // Perform attack (you can add attack effects or logic here)
        yield return new WaitForSeconds(0.2f); // Small delay to simulate the attack

        // Move back to the original position (slide back)
        elapsedTime = 0;
        while (elapsedTime < returnDuration)
        {
            attackerTransform.position = Vector3.Lerp(tacklePosition, originalPosition, elapsedTime / returnDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        attackerTransform.position = originalPosition;

        // Reset the coroutine reference after completion
        attackCoroutine = null;
    }































    public void TakeDamage(int damage) //change to select a target.
    {
        if (monsterData != null)
        {
            int previousHP = monsterData.extra2Info.current_HP;
            monsterData.extra2Info.current_HP -= damage;
            monsterData.extra2Info.current_HP = Mathf.Clamp(monsterData.extra2Info.current_HP, 0, monsterData.extra2Info.max_HP);

            // Start coroutine to animate health bar and text
            StartCoroutine(Info_Bar_Updater.AnimateHealthBarAndText(monsterData, previousHP, monsterData.extra2Info.current_HP));

            Debug.Log(monsterData.speciesInfo.species.ToString() + " took " + damage + " damage.");
        }
    }

}