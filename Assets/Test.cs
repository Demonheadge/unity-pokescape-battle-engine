using UnityEngine;
using UnityEngine.InputSystem;


public class Test : MonoBehaviour
{
   // public BattleEngine BattleEngine; // Reference to the BattleEngine


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            //BattleEngine.StartBattle();
        }
    }
}
