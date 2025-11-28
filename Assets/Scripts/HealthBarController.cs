// 26/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBarImage; // Reference to the UI Image component
    public Gradient healthGradient; // Gradient for health color (green to red)

    // Update the health bar based on the current health percentage
    public void UpdateHealthBar(float healthPercentage)
    {
        // Scale the health bar
        healthBarImage.fillAmount = healthPercentage;

        // Change the color based on health percentage
        healthBarImage.color = healthGradient.Evaluate(healthPercentage);
    }
}
