using UnityEngine;
using System.Collections.Generic;

public class Test_Generate_Monster : MonoBehaviour
{
    public TEST_MonsterDatabase monsterDatabase; // Reference to MonsterDatabase
    public MoveDatabase moveDatabase;       // Reference to MoveDatabase
    public LevelUpDatabase levelUpDatabase; // Reference to LevelUpDatabase

    public GameObject monsterPrefab; // Assign your Monster prefab in the Inspector

    
    public TEST_MonsterDatabase GetMonsterDatabase()
    {
        return monsterDatabase;
    }
    public MoveDatabase GetMoveDatabase()
    {
        return moveDatabase;
    }
    public LevelUpDatabase GetLevelUpDatabase()
    {
        return levelUpDatabase;
    }

    private void Awake()
    {
        // Ensure all references are assigned
        if (monsterDatabase == null || moveDatabase == null || levelUpDatabase == null)
        {
            Debug.LogError("One or more database references are not assigned in Generate_Monster!");
        }
    }

    
    
    public Monster CreateMonster()
    {
        // Generate random experience points for the monster (you can adjust this logic)
        int randomExperience = UnityEngine.Random.Range(0, 1000000);

        // Calculate the level based on experience
        int calculatedLevel = CalculateLevel(randomExperience);

        // Randomly select a monster from the MonsterDatabase
        Monster.SpeciesInfo randomMonster = monsterDatabase.monsters[UnityEngine.Random.Range(0, monsterDatabase.monsters.Count)];

        // Get usable moves based on LevelUpDatabase
        List<Move> usableMoves = GetUsableMoves(randomMonster.SPECIES, randomMonster.ID, calculatedLevel);

        // Randomly select moves from the usable moves list
        Monster.Moves monsterMoves = new Monster.Moves
        {
            MOVE_1 = SelectRandomMove(usableMoves),
            MOVE_2 = SelectRandomMove(usableMoves),
            MOVE_3 = SelectRandomMove(usableMoves),
            MOVE_4 = SelectRandomMove(usableMoves)
        };


        Monster.Skills monsterSkills = new Monster.Skills
        {
            // Randomizes for now. (Later do some different math to decide.)
            skill_attack = UnityEngine.Random.Range(1, 99),
            skill_defense = UnityEngine.Random.Range(1, 99),
            skill_strength = UnityEngine.Random.Range(1, 99),
            skill_magic = UnityEngine.Random.Range(1, 99),
            skill_ranged = UnityEngine.Random.Range(1, 99),
            skill_necromancy = UnityEngine.Random.Range(1, 99),
            skill_prayer = UnityEngine.Random.Range(1, 99),
            skill_summoning = UnityEngine.Random.Range(1, 99),
            skill_hitpoints = UnityEngine.Random.Range(1, 99),
            skill_slayer = UnityEngine.Random.Range(1, 99),
            skill_agility = UnityEngine.Random.Range(1, 99),
            skill_mining = UnityEngine.Random.Range(1, 99),
            skill_smithing = UnityEngine.Random.Range(1, 99),
            skill_fishing = UnityEngine.Random.Range(1, 99),
            skill_woodcutting = UnityEngine.Random.Range(1, 99),
            skill_cooking = UnityEngine.Random.Range(1, 99),
            skill_fletching = UnityEngine.Random.Range(1, 99),
            skill_crafting = UnityEngine.Random.Range(1, 99),
            skill_firemaking = UnityEngine.Random.Range(1, 99),
            skill_runecrafting = UnityEngine.Random.Range(1, 99),
            skill_dungeoneering = UnityEngine.Random.Range(1, 99),
            skill_sailing = UnityEngine.Random.Range(1, 99),
            skill_herblore = UnityEngine.Random.Range(1, 99),
            skill_farming = UnityEngine.Random.Range(1, 99),
            skill_construction = UnityEngine.Random.Range(1, 99),
            skill_divination = UnityEngine.Random.Range(1, 99),
            skill_hunter = UnityEngine.Random.Range(1, 99),
            skill_archaeology = UnityEngine.Random.Range(1, 99),
            skill_thieving = UnityEngine.Random.Range(1, 99)
        };
        // Specific IV skills that affect stats
        int atk_melee_iv_total = monsterSkills.skill_summoning; // + extra3Info.skill_necromancy + extra3Info.skill_prayer;
        int atk_ranged_iv_total = monsterSkills.skill_summoning;
        int atk_magic_iv_total = monsterSkills.skill_summoning;
        int def_melee_iv_total = monsterSkills.skill_summoning;
        int def_ranged_iv_total = monsterSkills.skill_summoning;
        int def_magic_iv_total = monsterSkills.skill_summoning;


        Monster.Statistics monsterStatistics = new Monster.Statistics
        {
            experience = randomExperience, // Randomly generated experience points
            level = calculatedLevel, // Use the calculated level
            max_HP = randomMonster.baseHP,
            current_HP = randomMonster.baseHP,
            current_Speed = randomMonster.baseSpeed,

            // Overall = ( ( (2 × Base) + (IV_Total / 4) ) × Level) / 100 )
            // Attack
            current_Attack_Melee = ((2 * randomMonster.baseAttack_Melee + (atk_melee_iv_total / 4)) * calculatedLevel) / 100,
            current_Attack_Ranged = ((2 * randomMonster.baseAttack_Ranged + (atk_ranged_iv_total / 4)) * calculatedLevel) / 100,
            current_Attack_Magic = ((2 * randomMonster.baseAttack_Magic + (atk_magic_iv_total / 4)) * calculatedLevel) / 100,
            // Defense
            current_Defense_Melee = ((2 * randomMonster.baseDefense_Melee + (def_melee_iv_total / 4)) * calculatedLevel) / 100,
            current_Defense_Ranged = ((2 * randomMonster.baseDefense_Ranged + (def_ranged_iv_total / 4)) * calculatedLevel) / 100,
            current_Defense_Magic = ((2 * randomMonster.baseDefense_Magic + (def_magic_iv_total / 4)) * calculatedLevel) / 100,
        };

        // Instantiate a new GameObject and add the Test_Monster_Controller component
        //GameObject monsterObject = Instantiate(monsterPrefab);
        //GameObject monsterObject = Instantiate(monsterPrefab, GetSpawnPosition(0, 2.1f, 0.9f), Quaternion.identity);    //TODO: Spawn postition
        //Test_Monster_Controller monsterController = monsterObject.GetComponent<Test_Monster_Controller>();

        // Create a new Monster object and assign all the generated data
        Monster newMonster = new Monster
        {
            //Monster_GameObject = this.gameObject,
            //Monster_GameObject = null,
            monsterSpeciesInfo = randomMonster,
            monsterMoves = monsterMoves,
            monsterStatistics = monsterStatistics,
            monsterSkills = monsterSkills
        };

        // Assign the Monster data to the Test_Monster_Controller
        //monsterController.monsterData = newMonster;

        // Return the Monster object instead of Test_Monster_Controller
        return newMonster;
    }


    private Vector3 GetSpawnPosition(int index, float customX, float customY)
    {
        // Define spawn positions based on the index
        // You can customize this logic to suit your game's requirements
        float xOffset = customX + index * 1.0f; // Example: space monsters 1 units apart
        float yOffset = customY;
        return new Vector3(xOffset, yOffset, 0); // Adjust as needed
    }


    private int CalculateLevel(int experience)
    {
        const int maxLevel = 99;
        const int maxExperience = 1000000;

        float level = Mathf.Floor(Mathf.Sqrt((float)experience / maxExperience) * maxLevel);

        return Mathf.Clamp((int)level, 1, maxLevel);
    }

    private List<Move> GetUsableMoves(Species species, int id, int currentLevel)
    {
        List<Move> usableMoves = new List<Move>();

        foreach (var levelUpInfo in levelUpDatabase.levelUpData)
        {
            // Check if the ID and species match
            if (/*levelUpInfo.ID == id || */levelUpInfo.name == species)
            {
                foreach (var moveLevelPair in levelUpInfo.moves)
                {
                    // Check if the move is valid (not NONE and level > 0), not already in the list, and meets the level requirement
                    if (moveLevelPair.move != Move.NONE && moveLevelPair.level > 0 && moveLevelPair.level <= currentLevel && !usableMoves.Contains(moveLevelPair.move))
                    {
                        usableMoves.Add(moveLevelPair.move);
                        //Debug.Log($"Added move: {moveLevelPair.move} (Level: {moveLevelPair.level}) to usable moves list.");
                    }
                    else if (moveLevelPair.move == Move.NONE || moveLevelPair.level == 0)
                    {
                        //Debug.Log($"Skipped move: {moveLevelPair.move} (Level: {moveLevelPair.level}) as it is invalid.");
                    }
                }
            }
        }

        // Log the final list of usable moves
        Debug.Log($"Usable moves for {species} (ID: {id}, Level: {currentLevel}): {string.Join(", ", usableMoves)}");

        return usableMoves;
    }

    // Helper method to select a random move and remove it from the list to avoid duplicates
    private Move SelectRandomMove(List<Move> usableMoves)
    {
        // Check if there are any usable moves left
        if (usableMoves.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, usableMoves.Count);
            Move selectedMove = usableMoves[randomIndex];
            usableMoves.RemoveAt(randomIndex); // Remove the selected move to avoid duplicates
            return selectedMove;
        }

        // If no usable moves are left, return NONE
        //Debug.LogWarning("No usable moves left to select. Defaulting to Move.NONE.");
        return Move.NONE;
    }
}
