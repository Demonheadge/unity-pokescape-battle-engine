using UnityEngine;

public class Variables : MonoBehaviour
{
    public bool isInAMenu;
    public bool isInABattle;
    public bool canPlayerInteract;
    public BattleType battleType;
    public float HP_BAR_Speed_duration = 1f;    // Duration of the hp bar animation
    //public int PlayerParty_HowManyMonsters_InBattle = 1;
}

public enum BattleType
{
    BattleType_1v1,
    BattleType_1v2
}