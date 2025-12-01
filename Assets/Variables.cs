using UnityEngine;

public class Variables : MonoBehaviour
{
    public bool isInAMenu;
    public bool isInABattle;
    public bool canPlayerInteract;
    public BattleType battleType;
}

public enum BattleType
{
    BattleType_1v1,
    BattleType_1v2
}