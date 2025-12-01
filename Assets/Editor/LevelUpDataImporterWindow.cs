// 1/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

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

        EditorGUILayout.LabelField("CSV File Path:");
        csvFilePath = EditorGUILayout.TextField(csvFilePath);

        EditorGUILayout.LabelField("Level Up Database:");
        levelUpDatabase = (LevelUpDatabase)EditorGUILayout.ObjectField(levelUpDatabase, typeof(LevelUpDatabase), false);

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

        var groupedData = lines.Skip(1)
            .Select(line => line.Split(','))
            .Where(values => values.Length >= 4)
            .GroupBy(values => new { ID = ParseInt(values[0], 0), Name = ParseSpecies(values[1]) })
            .ToDictionary(group => group.Key, group => group.Select(values => new MoveLevelPair
            {
                level = ParseInt(values[2], 0),
                move = ParseMove(values[3])
            }).ToList());

        levelUpDatabase.levelUpData.Clear();

        foreach (var group in groupedData)
        {
            LevelUpInformation levelUpInfo = new LevelUpInformation
            {
                ID = group.Key.ID,
                name = group.Key.Name,
                moves = group.Value
            };

            levelUpDatabase.levelUpData.Add(levelUpInfo);
        }

        EditorUtility.SetDirty(levelUpDatabase);
        AssetDatabase.SaveAssets();

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
}