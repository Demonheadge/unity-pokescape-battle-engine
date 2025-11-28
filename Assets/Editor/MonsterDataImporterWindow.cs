// 29/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

// 29/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MonsterDataImporterWindow : EditorWindow
{
    private string csvFilePath = "Assets/Editor/MonsterData.csv"; // Default path to your CSV file
    private MonsterDatabase monsterDatabase;

    [MenuItem("Tools/Monster Data Importer")]
    public static void ShowWindow()
    {
        GetWindow<MonsterDataImporterWindow>("Monster Data Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Monster Data Importer", EditorStyles.boldLabel);

        // Input field for CSV file path
        EditorGUILayout.LabelField("CSV File Path:");
        csvFilePath = EditorGUILayout.TextField(csvFilePath);

        // Field to assign the MonsterDatabase ScriptableObject
        EditorGUILayout.LabelField("Monster Database:");
        monsterDatabase = (MonsterDatabase)EditorGUILayout.ObjectField(monsterDatabase, typeof(MonsterDatabase), false);

        // Import button
        if (GUILayout.Button("Import Monster Data"))
        {
            ImportMonsterData();
        }
    }

    private void ImportMonsterData()
    {
        if (monsterDatabase == null)
        {
            Debug.LogError("MonsterDatabase is not assigned!");
            return;
        }

        if (!File.Exists(csvFilePath))
        {
            Debug.LogError($"CSV file not found at path: {csvFilePath}");
            return;
        }

        string[] lines = File.ReadAllLines(csvFilePath);
        monsterDatabase.monsters.Clear();

        foreach (string line in lines.Skip(1)) // Skip the header line
        {
            string[] values = line.Split(',');

            // Ensure the line has enough data
            if (values.Length < 7)
            {
                Debug.LogWarning($"Line has insufficient data: {line}. Filling missing fields with default values.");
                values = FillMissingValues(values, 7); // Ensure the array has 7 elements
            }

            // Parse data and handle invalid values
            Species species = ParseSpecies(values[0]); // Parse Species enum or set to default
            string name = CapitalizeName(values[0]); // Capitalize the name
            MonsterType type = ParseMonsterType(values[1]); // Parse MonsterType enum or set to default
            int baseHP = ParseInt(values[2], 0); // Default baseHP to 0 if invalid
            int baseAttack = ParseInt(values[3], 0); // Default baseAttack to 0 if invalid
            int baseDefense = ParseInt(values[4], 0); // Default baseDefense to 0 if invalid
            int baseSpeed = ParseInt(values[5], 0); // Default baseSpeed to 0 if invalid

            // Assign sprites based on folder structure
            Sprite frontSprite = FindSprite(species.ToString(), "front.png");
            Sprite backSprite = FindSprite(species.ToString(), "back.png");
            Sprite partyIcon = FindSprite(species.ToString(), "icon.png");

            // Create and add the monster to the database
            SpeciesInfo monsterInfo = new SpeciesInfo
            {
                species = species,
                name = name,
                type = type,
                baseHP = baseHP,
                baseAttack = baseAttack,
                baseDefense = baseDefense,
                baseSpeed = baseSpeed,
                front_sprite = frontSprite,
                back_sprite = backSprite,
                partyicon = partyIcon
            };

            monsterDatabase.monsters.Add(monsterInfo);
        }

        EditorUtility.SetDirty(monsterDatabase); // Mark the ScriptableObject as dirty to save changes
        AssetDatabase.SaveAssets(); // Save the changes to the asset

        Debug.Log($"Imported {monsterDatabase.monsters.Count} monsters from {csvFilePath}");
    }

    private int ParseInt(string value, int defaultValue)
    {
        if (int.TryParse(value, out int result))
        {
            return result;
        }
        else
        {
            Debug.LogWarning($"Invalid integer value: {value}. Defaulting to {defaultValue}.");
            return defaultValue;
        }
    }

    private MonsterType ParseMonsterType(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogWarning($"MonsterType value is empty. Setting to default.");
            return default; // Default value for MonsterType
        }

        if (System.Enum.TryParse(value, out MonsterType type))
        {
            return type;
        }
        else
        {
            Debug.LogWarning($"Invalid MonsterType value: {value}. Setting to default.");
            return default; // Default value for MonsterType
        }
    }

    private Species ParseSpecies(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogWarning($"Species value is empty. Setting to default.");
            return default; // Default value for Species
        }

        if (System.Enum.TryParse(value, out Species species))
        {
            return species;
        }
        else
        {
            Debug.LogWarning($"Invalid Species value: {value}. Setting to default.");
            return default; // Default value for Species
        }
    }

    private string CapitalizeName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return string.Empty; // Return empty string if name is empty
        }

        // Capitalize the first letter of each word
        return string.Join(" ", name.Split('_').Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
    }

    private Sprite FindSprite(string folderName, string spriteFileName)
    {
        string path = $"Assets/Resources/Pokescape_Monsters/{folderName}/{spriteFileName}";
        return AssetDatabase.LoadAssetAtPath<Sprite>(path);
    }

    private string[] FillMissingValues(string[] values, int requiredLength)
    {
        List<string> filledValues = new List<string>(values);
        while (filledValues.Count < requiredLength)
        {
            filledValues.Add(""); // Add empty strings for missing values
        }
        return filledValues.ToArray();
    }
}