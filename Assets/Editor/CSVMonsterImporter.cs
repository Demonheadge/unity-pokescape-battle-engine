// 26/11/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using System;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class CSVMonsterImporter : EditorWindow
{
    private string csvFilePath = "Assets/Editor/MonsterData.csv"; // Path to the .csv file
    private string outputFolder = "Assets/Resources/monsters"; // Folder to save ScriptableObjects

    [MenuItem("Tools/Import Monsters from CSV")]
    public static void ShowWindow()
    {
        GetWindow<CSVMonsterImporter>("Import Monsters from CSV");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Monsters from CSV", EditorStyles.boldLabel);

        csvFilePath = EditorGUILayout.TextField("CSV File Path:", csvFilePath);
        outputFolder = EditorGUILayout.TextField("Output Folder:", outputFolder);

        if (GUILayout.Button("Import Monsters"))
        {
            ImportMonstersFromCSV();
        }
    }

    private void ImportMonstersFromCSV()
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

            if (values.Length < 8)
            {
                Debug.LogWarning($"Skipping invalid row at line {i + 1}: {lines[i]}");
                continue;
            }

            int id = int.Parse(values[0].Trim());
            string speciesName = values[1].Trim();
            string typeString = values[2].Trim();
            int health = int.Parse(values[3].Trim());
            int attack = int.Parse(values[4].Trim());
            int defense = int.Parse(values[5].Trim());
            int speed = int.Parse(values[6].Trim());
            int gender = int.Parse(values[7].Trim());

            // Parse enums
            if (!System.Enum.TryParse(typeString, out MonsterType type))
            {
                Debug.LogWarning($"Invalid type at line {i + 1}: {typeString}");
                continue;
            }

            // Create or update ScriptableObject
            string assetPath = $"{outputFolder}/{speciesName}.asset";
            MonsterInformation monster = AssetDatabase.LoadAssetAtPath<MonsterInformation>(assetPath);

            if (monster == null)
            {
                monster = ScriptableObject.CreateInstance<MonsterInformation>();
                AssetDatabase.CreateAsset(monster, assetPath);
            }

            monster.id = id;
            monster.speciesName = speciesName;
            monster.type = type;
            monster.health = health;
            monster.attack = attack;
            monster.defense = defense;
            monster.speed = speed;
            monster.gender = gender;

            EditorUtility.SetDirty(monster);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Monster data imported successfully from CSV.");
    }
}
