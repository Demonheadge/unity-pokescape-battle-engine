//// 13/12/2025 AI-Tag
//// This was created with the help of Assistant, a Unity Artificial Intelligence product.
//
//using System;
//using UnityEditor;
//using UnityEngine;
//using System.Collections.Generic;
//
//public class BattleManager : MonoBehaviour
//{
//    public BattleRules battleRules;
//    public List<TrainerConfig> trainers;
//
//    private List<GameObject> enemyTeam = new List<GameObject>();
//    private List<GameObject> playerTeam = new List<GameObject>();
//
//    private TargetingSystem targetingSystem;
//
//    void Start()
//    {
//        targetingSystem = GetComponent<TargetingSystem>();
//        SetupBattle();
//    }
//
//    private void SetupBattle()
//    {
//        // Example: Set up enemy and player teams based on battle rules and trainer configurations
//        foreach (var trainer in trainers)
//        {
//            foreach (var monster in trainer.party)
//            {
//                //GameObject monsterObject = InstantiateMonster(monster);
//                /*if (trainer.trainerClass == "Enemy")
//                {
//                    enemyTeam.Add(monsterObject);
//                }
//                else
//                {
//                    playerTeam.Add(monsterObject);
//                }*/
//            }
//        }
//
//        targetingSystem.SetTeams(enemyTeam, playerTeam);
//    }
//
//    /*private GameObject InstantiateMonster(Monster monster)
//    {
//        // Instantiate monster GameObject based on the TrainerMon data
//        GameObject monsterObject = new GameObject(monster.nickname);
//        // Add components and set properties based on monster data
//        return monsterObject;
//    }*/
//}
//