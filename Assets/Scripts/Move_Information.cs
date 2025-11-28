using System;

[System.Serializable]
public class Move_Information
{
    public string moveName;
    public int damage;
    public string type; // e.g., Fire, Water, Grass, etc.
    public float accuracy; // Value between 0 and 1
    public string effect; // e.g., Burn, Freeze, Poison, etc.

    public bool IsMoveSuccessful()
    {
        float randomValue = UnityEngine.Random.value;
        return randomValue <= accuracy;
    }
}