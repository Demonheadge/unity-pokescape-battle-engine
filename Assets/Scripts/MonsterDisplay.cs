// 29/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonsterDisplay : MonoBehaviour
{
    public TextMeshProUGUI species_text;
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI type_text;
    public TextMeshProUGUI hp_text;
    public TextMeshProUGUI attack_text;
    public TextMeshProUGUI defense_text;
    public TextMeshProUGUI speed_text;
    public Image Front_Sprite;

    public void Setup(SpeciesInfo monster)
    {
        if (monster == null)
        {
            Debug.LogError("Monster data is null!");
            return;
        }

        species_text.text = monster.species.ToString();
        name_text.text = monster.name;
        type_text.text = monster.type.ToString();
        hp_text.text = "HP: " + monster.baseHP.ToString();
        attack_text.text = "Attack: " + monster.baseAttack.ToString();
        defense_text.text = "Defense: " + monster.baseDefense.ToString();
        speed_text.text = "Speed: " + monster.baseSpeed.ToString();

        if (monster.front_sprite != null)
        {
            Front_Sprite.sprite = monster.front_sprite;
        }
        else
        {
            Debug.LogWarning("Monster does not have a front sprite!");
        }
    }
}
