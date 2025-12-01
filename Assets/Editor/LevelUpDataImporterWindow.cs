// 1/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
// 29/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.


public class LevelUpDataImporterWindow : EditorWindow
{
    private string csvFilePath = "Assets/Editor/LevelUp_Data.csv"; // Default path to your CSV file
    private LevelUpDatabase levelUpDatabase;

    [MenuItem("Tools/Level Up Data Importer")]
    public static void ShowWindow()
    {
        GetWindow<LevelUpDataImporterWindow>("Level Up Data Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Level Up Data Importer", EditorStyles.boldLabel);

        // Input field for CSV file path
        EditorGUILayout.LabelField("CSV File Path:");
        csvFilePath = EditorGUILayout.TextField(csvFilePath);

        // Field to assign the LevelUpDatabase ScriptableObject
        EditorGUILayout.LabelField("Level Up Database:");
        levelUpDatabase = (LevelUpDatabase)EditorGUILayout.ObjectField(levelUpDatabase, typeof(LevelUpDatabase), false);

        // Import button
        if (GUILayout.Button("Import Level Up Data"))
        {
            ImportLevelUpData();
        }
    }

    private void ImportLevelUpData()
    {
        if (levelUpDatabase == null)
        {
            Debug.LogError("LevelUpDatabase is not assigned!");
            return;
        }

        if (!File.Exists(csvFilePath))
        {
            Debug.LogError($"CSV file not found at path: {csvFilePath}");
            return;
        }

        string[] lines = File.ReadAllLines(csvFilePath);
        levelUpDatabase.levelUpData.Clear();

        foreach (string line in lines.Skip(1)) // Skip the header line
        {
            string[] values = line.Split(',');

            // Ensure the line has enough data
            if (values.Length < 4) // Adjust the number based on the fields in LevelUpInformation
            {
                Debug.LogWarning($"Line has insufficient data: {line}. Filling missing fields with default values.");
                values = FillMissingValues(values, 4); // Ensure the array has 4 elements
            }

            // Parse data and handle invalid values
            int id = ParseInt(values[0], 0); // Default ID to 0 if invalid
            Species name = ParseSpecies(values[1]); // Parse Species enum or set to default
            int level = ParseInt(values[2], 0); // Default level to 0 if invalid
            Move move = ParseMove(values[3]); // Parse Move enum or set to default if invalid

            // Create and add the level up data to the database
            LevelUpInformation levelUpInfo = new LevelUpInformation
            {
                ID = id,
                name = name,
                level = level,
                move = move
            };

            levelUpDatabase.levelUpData.Add(levelUpInfo);
        }

        EditorUtility.SetDirty(levelUpDatabase); // Mark the ScriptableObject as dirty to save changes
        AssetDatabase.SaveAssets(); // Save the changes to the asset

        Debug.Log($"Imported {levelUpDatabase.levelUpData.Count} level up entries from {csvFilePath}");
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

    private Move ParseMove(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogWarning($"Move value is empty. Setting to default.");
            return default(Move); // Default value for Move
        }

        // Assuming Move is an enum, handle parsing accordingly
        if (System.Enum.TryParse(value, out Move move))
        {
            return move;
        }
        else
        {
            Debug.LogWarning($"Invalid Move value: {value}. Setting to default.");
            return default(Move); // Default value for Move
        }
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
