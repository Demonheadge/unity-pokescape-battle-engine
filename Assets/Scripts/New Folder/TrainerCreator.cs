// 13/12/2025 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.
/*
using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class TrainerCreator : MonoBehaviour
{
    public LevelUpDatabase levelUpDatabase;
    public MonsterDatabase monsterDatabase;

    public TrainerConfig CreateTrainer(string trainerName, string trainerClass, List<TrainerMon> party)
    {
        TrainerConfig trainer = ScriptableObject.CreateInstance<TrainerConfig>();
        trainer.trainerName = trainerName;
        trainer.trainerClass = trainerClass;
        trainer.party = new List<TrainerMon>();

        foreach (var trainerMon in party)
        {
            TrainerMon newMon = GenerateTrainerMon(trainerMon);
            trainer.party.Add(newMon);
        }

        return trainer;
    }

    private TrainerMon GenerateTrainerMon(TrainerMon baseMon)
    {
        TrainerMon newMon = new TrainerMon
        {
            species = baseMon.species,
            nickname = baseMon.nickname,
            level = baseMon.level,
            moves = GenerateMoves(baseMon.species, baseMon.level),
            skills = GenerateSkills(baseMon.species)
        };

        return newMon;
    }

    private List<string> GenerateMoves(Species species, int level)
    {
        List<string> moves = new List<string>();
        LevelUpInformation levelUpInfo = levelUpDatabase.levelUpData.Find(info => info.name == species);

        if (levelUpInfo != null)
        {
            foreach (var movePair in levelUpInfo.moves)
            {
                if (movePair.level <= level && moves.Count < 4)
                {
                    moves.Add(movePair.move.ToString());
                }
            }
        }

        return moves;
    }

    private Dictionary<string, int> GenerateSkills(Species species)
    {
        Dictionary<string, int> skills = new Dictionary<string, int>();
        SpeciesInfo speciesInfo = monsterDatabase.monsters.Find(info => info.name == species);

        if (speciesInfo != null)
        {
            foreach (var skill in speciesInfo.skills)
            {
                skills.Add(skill.Key, skill.Value);
            }
        }

        return skills;
    }
}*/
