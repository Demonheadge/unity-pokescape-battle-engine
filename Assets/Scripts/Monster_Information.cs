using System.Collections.Generic;
using UnityEngine;

public class Monster_Information : MonoBehaviour
{
    public string monsterName;
    public int hitPoints;
    public int maxHitPoints; // Maximum HP for health bar scaling
    public int speed;
    public List<Move_Information> moves;
    public bool isFainted = false; // Track if the monster has fainted
    public GameObject healthBar; // Reference to the health bar UI

    public int GetMoveDamage()
    {
        if (moves.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, moves.Count);
            return moves[randomIndex].damage;
        }
        return 0;
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints < 0)
            hitPoints = 0;

        UpdateHealthBar();

        if (hitPoints == 0)
        {
            Faint();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float healthPercentage = (float)hitPoints / maxHitPoints;
            HealthBarController healthBarController = healthBar.GetComponent<HealthBarController>();
            if (healthBarController != null)
            {
                healthBarController.UpdateHealthBar(healthPercentage);
            }
        }
    }

    private void Faint()
    {
        isFainted = true;
        Debug.Log($"{monsterName} has fainted!");
        // Additional logic for fainting (e.g., play animation, disable monster, etc.)
    }
}