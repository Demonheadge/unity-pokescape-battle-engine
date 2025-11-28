using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CSVMoveImporter : EditorWindow
{
    private string csvFilePath = "Assets/Editor/MoveData.csv"; // Path to the .csv file
    private string outputFolder = "Assets/Resources/moves"; // Folder to save ScriptableObjects

    [MenuItem("Tools/Import Moves from CSV")]
    public static void ShowWindow()
    {
        GetWindow<CSVMoveImporter>("Import Moves from CSV");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Moves from CSV", EditorStyles.boldLabel);

        csvFilePath = EditorGUILayout.TextField("CSV File Path:", csvFilePath);
        outputFolder = EditorGUILayout.TextField("Output Folder:", outputFolder);

        if (GUILayout.Button("Import Moves"))
        {
            ImportMovesFromCSV();
        }
    }

    private void ImportMovesFromCSV()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError($"CSV file not found at path: {csvFilePath}");
            return;
        }

        string[] lines = File.ReadAllLines(csvFilePath);
        if (lines.Length < 2)
        {
            Debug.LogError("CSV file is empty or does not contain valid data.");
            return;
        }

        // Ensure the output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        for (int i = 1; i < lines.Length; i++) // Skip the header row
        {
            string[] values = lines[i].Split(',');

            if (values.Length < 5)
            {
                Debug.LogWarning($"Skipping invalid row at line {i + 1}: {lines[i]}");
                continue;
            }

            string name = values[0].Trim();
            string typeString = values[1].Trim();
            string effectString = values[2].Trim();
            int effectsecondary = int.Parse(values[3].Trim());
            int damage = int.Parse(values[4].Trim());
            int accuracy = int.Parse(values[5].Trim());
            string catagorystring = values[6].Trim();

            // Parse enums
            if (!System.Enum.TryParse(effectString, out MoveEffect effect))
            {
                Debug.LogWarning($"Invalid effect at line {i + 1}: {effectString}");
                continue;
            }

            if (!System.Enum.TryParse(typeString, out MoveType type))
            {
                Debug.LogWarning($"Invalid type at line {i + 1}: {typeString}");
                continue;
            }
            
            if (!System.Enum.TryParse(catagorystring, out MoveCatagory catagory))
            {
                Debug.LogWarning($"Invalid type at line {i + 1}: {catagorystring}");
                continue;
            }

            // Create or update ScriptableObject
            string assetPath = $"{outputFolder}/{name}.asset";
            MoveInformation move = AssetDatabase.LoadAssetAtPath<MoveInformation>(assetPath);

            if (move == null)
            {
                move = ScriptableObject.CreateInstance<MoveInformation>();
                AssetDatabase.CreateAsset(move, assetPath);
            }

            move.moveName = name;
            move.effect = effect;
            move.effect_secondary = effectsecondary;
            move.damage = damage;
            move.type = type;
            move.accuracy = accuracy;
            move.catagory = catagory;

            EditorUtility.SetDirty(move);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Move data imported successfully from CSV.");
    }
}