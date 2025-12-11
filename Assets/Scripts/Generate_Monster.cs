using UnityEngine;
using System.Collections.Generic;

public class Generate_Monster : MonoBehaviour
{
    public MonsterDatabase monsterDatabase; // Reference to MonsterDatabase
    public MoveDatabase moveDatabase;       // Reference to MoveDatabase
    public LevelUpDatabase levelUpDatabase; // Reference to LevelUpDatabase

    
    public MonsterDatabase GetMonsterDatabase()
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
    
    public SpawnedMonster SpawnMonster()
    {
        // Generate random experience points for the monster (you can adjust this logic)
        int randomExperience = UnityEngine.Random.Range(0, 1000000);
        // Calculate the level based on experience
        int calculatedLevel = CalculateLevel(randomExperience);
        // Randomly select a monster from the MonsterDatabase
        SpeciesInfo randomMonster = monsterDatabase.monsters[UnityEngine.Random.Range(0, monsterDatabase.monsters.Count)];

        // Get usable moves based on LevelUpDatabase
        List<Move> usableMoves = GetUsableMoves(randomMonster.species, randomMonster.ID, calculatedLevel);

        // Randomly select moves from the usable moves list
        Extra_1_Monster_Info extra1Info = new Extra_1_Monster_Info
        {
            move_1 = SelectRandomMove(usableMoves),
            move_2 = SelectRandomMove(usableMoves),
            move_3 = SelectRandomMove(usableMoves),
            move_4 = SelectRandomMove(usableMoves)
        };

        Extra_3_Monster_Info extra3Info = new Extra_3_Monster_Info
        {
            //Randomises for now. (Later do some different math to decide.)
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

        //Specific IV skills that affect stats
        int atk_melee_iv_total = extra3Info.skill_summoning; //+ extra3Info.skill_necromancy + extra3Info.skill_prayer;
        int atk_ranged_iv_total = extra3Info.skill_summoning;
        int atk_magic_iv_total= extra3Info.skill_summoning;
        int def_melee_iv_total= extra3Info.skill_summoning;
        int def_ranged_iv_total = extra3Info.skill_summoning;
        int def_magic_iv_total = extra3Info.skill_summoning;

        Extra_2_Monster_Info extra2Info = new Extra_2_Monster_Info
        {
            experience = randomExperience,  //Needs to Swap to randomly generate the level. Depending on the level generated determines the xp.
            level = calculatedLevel, // Use the calculated level
            max_HP = randomMonster.baseHP,
            current_HP = randomMonster.baseHP,
            current_Speed = randomMonster.baseSpeed,

            //Overall = ( ( (2 × Base) + (IV_Total / 4) ) × Level) / 100 )
            //attack
            current_Attack_Melee = ((2 * randomMonster.baseAttack_Melee + (atk_melee_iv_total / 4)) * calculatedLevel) / 100, 
            current_Attack_Ranged = ((2 * randomMonster.baseAttack_Ranged + (atk_ranged_iv_total / 4)) * calculatedLevel) / 100, 
            current_Attack_Magic = ((2 * randomMonster.baseAttack_Magic + (atk_magic_iv_total / 4)) * calculatedLevel) / 100, 
            //defense
            current_Defense_Melee = ((2 * randomMonster.baseDefense_Melee + (def_melee_iv_total / 4)) * calculatedLevel) / 100, 
            current_Defense_Ranged = ((2 * randomMonster.baseDefense_Ranged + (def_ranged_iv_total / 4)) * calculatedLevel) / 100,
            current_Defense_Magic = ((2 * randomMonster.baseDefense_Magic + (def_magic_iv_total / 4)) * calculatedLevel) / 100, 
        };

        SpawnedMonster spawnedMonster = new SpawnedMonster(randomMonster, extra1Info, extra2Info, extra3Info);
        
        return spawnedMonster;
    }


    private int CalculateLevel(int experience)
    {
        // Max level and max experience
        const int maxLevel = 99;
        const int maxExperience = 1000000;

        // Calculate the level based on experience
        // Using a quadratic formula for leveling progression
        float level = Mathf.Floor(Mathf.Sqrt((float)experience / maxExperience) * maxLevel);

        // Ensure the level is at least 1 and does not exceed maxLevel
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
        Debug.LogWarning("No usable moves left to select. Defaulting to Move.NONE.");
        return Move.NONE;
    }
}
