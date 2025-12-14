using UnityEngine;

public class Variables : MonoBehaviour
{
    public bool isInAMenu;
    public bool isInABattle;
    public bool canPlayerInteract;
    public BattleType battleType;
    public float HP_BAR_Speed_duration = 1f;    // Duration of the hp bar animation
    //public int PlayerParty_HowManyMonsters_InBattle = 1;






//BATTLE INFORMATION
    public bool BATTLE_TYPE_WILD_MONSTER;
    public bool BATTLE_TYPE_TRAINER;

    //Type of Battle
    public bool BATTLE_1_VS_1;
    public bool BATTLE_1_VS_2;
    public bool BATTLE_2_VS_1;
    public bool BATTLE_2_VS_2;
    public bool BATTLE_3_VS_3;
    
    //Battle Outcomes
    public bool BATTLE_OUTCOME_WON;
    public bool BATTLE_OUTCOME_LOST;
    public bool BATTLE_OUTCOME_DRAW;
    public bool BATTLE_OUTCOME_PLAYER_RAN;
    public bool BATTLE_OUTCOME_ENEMY_RAN;
}

public enum BattleType
{
    BATTLE_1_VS_1,
    BATTLE_1_VS_2,
    BATTLE_2_VS_1,
    BATTLE_2_VS_2,
    BATTLE_3_VS_3,

    BattleType_1v1,
    BattleType_1v2
}


