// 29/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class MoveDataImporterWindow : EditorWindow
{
    private string csvFilePath = "Assets/Editor/MoveData.csv"; // Default path to your CSV file
    private MoveDatabase moveDatabase;

    [MenuItem("Tools/Move Data Importer")]
    public static void ShowWindow()
    {
        GetWindow<MoveDataImporterWindow>("Move Data Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Move Data Importer", EditorStyles.boldLabel);

        // Input field for CSV file path
        EditorGUILayout.LabelField("CSV File Path:");
        csvFilePath = EditorGUILayout.TextField(csvFilePath);

        // Field to assign the MoveDatabase ScriptableObject
        EditorGUILayout.LabelField("Move Database:");
        moveDatabase = (MoveDatabase)EditorGUILayout.ObjectField(moveDatabase, typeof(MoveDatabase), false);

        // Import button
        if (GUILayout.Button("Import Move Data"))
        {
            ImportMoveData();
        }
    }

    private void ImportMoveData()
    {
        if (moveDatabase == null)
        {
            Debug.LogError("MoveDatabase is not assigned!");
            return;
        }

        if (!File.Exists(csvFilePath))
        {
            Debug.LogError($"CSV file not found at path: {csvFilePath}");
            return;
        }

        string[] lines = File.ReadAllLines(csvFilePath);
        moveDatabase.moves.Clear();

        foreach (string line in lines)
        {
            string[] values = line.Split(',');

            // Ensure the line has enough data
            if (values.Length < 7) // Adjust the number based on the new fields in MoveInformation
            {
                Debug.LogWarning($"Line has insufficient data: {line}. Filling missing fields with default values.");
                values = FillMissingValues(values, 7); // Ensure the array has 7 elements
            }

            // Parse data and handle invalid values
            Move move = ParseMove(values[0]); // Parse Move enum or set to default if invalid
            string name = FormatMoveName(values[0]); // Format name from MoveID
            MoveType type = ParseMoveType(values[1]); // Parse MoveType enum or set to default
            MoveEffect effect = ParseMoveEffect(values[2]); // Parse MoveEffect enum or set to default
            int damage = ParseInt(values[3], 0); // Default damage to 0 if invalid
            int accuracy = ParseInt(values[4], 0); // Default accuracy to 0 if invalid
            int effectSecondary = ParseInt(values[5], 0); // Default effectSecondary to 0 if invalid
            MoveCatagory catagory = ParseMoveCatagory(values[6]); // Parse MoveCatagory enum or set to default

            // Create and add the move to the database
            MoveInformation moveInfo = new MoveInformation
            {
                move = move,
                name = name,
                type = type,
                effect = effect,
                damage = damage,
                accuracy = accuracy,
                effectSecondary = effectSecondary,
                catagory = catagory
            };

            moveDatabase.moves.Add(moveInfo);
        }

        EditorUtility.SetDirty(moveDatabase); // Mark the ScriptableObject as dirty to save changes
        AssetDatabase.SaveAssets(); // Save the changes to the asset

        Debug.Log($"Imported {moveDatabase.moves.Count} moves from {csvFilePath}");
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

    private MoveType ParseMoveType(string value)
    {
        if (string.IsNullOrEmpty(value) || value == "-")
        {
            Debug.LogWarning($"MoveType value is empty or invalid. Setting to default.");
            return default; // Default value for MoveType
        }

        if (System.Enum.TryParse(value, out MoveType type))
        {
            return type;
        }
        else
        {
            Debug.LogWarning($"Invalid MoveType value: {value}. Setting to default.");
            return default; // Default value for MoveType
        }
    }

    private MoveEffect ParseMoveEffect(string value)
    {
        if (string.IsNullOrEmpty(value) || value == "-")
        {
            Debug.LogWarning($"MoveEffect value is empty or invalid. Setting to default.");
            return default; // Default value for MoveEffect
        }

        if (System.Enum.TryParse(value, out MoveEffect effect))
        {
            return effect;
        }
        else
        {
            Debug.LogWarning($"Invalid MoveEffect value: {value}. Setting to default.");
            return default; // Default value for MoveEffect
        }
    }

    private MoveCatagory ParseMoveCatagory(string value)
    {
        if (string.IsNullOrEmpty(value) || value == "-")
        {
            Debug.LogWarning($"MoveCatagory value is empty or invalid. Setting to default.");
            return default; // Default value for MoveCatagory
        }

        if (System.Enum.TryParse(value, out MoveCatagory catagory))
        {
            return catagory;
        }
        else
        {
            Debug.LogWarning($"Invalid MoveCatagory value: {value}. Setting to default.");
            return default; // Default value for MoveCatagory
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

    private string FormatMoveName(string moveID)
    {
        if (string.IsNullOrEmpty(moveID))
        {
            return null; // Return null if moveID is empty
        }

        // Replace underscores with spaces and capitalize each word
        return Regex.Replace(moveID.ToLower(), "_", " ")
                    .Split(' ')
                    .Select(word => char.ToUpper(word[0]) + word.Substring(1))
                    .Aggregate((current, next) => current + " " + next);
    }
}