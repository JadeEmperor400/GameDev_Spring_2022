using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : BattleEntity
{
    [SerializeField]
    [Tooltip("How much time the player has to solve the puzzle")]
    private float baseTime = 10.0f; // Base amount of time for player to solve puzzles
    [Tooltip("How much time the player has lost to solve the  puzzle")]
    public float timeReduction = 0; //reduction to the player's puzzle solving time

    public float BaseTime {
        get {
            if (timeReduction > baseTime / 2) {
                timeReduction = baseTime / 2;
            }

            return baseTime - timeReduction;
        }
    }

    public override void Init()
    {
        HP = MaxHP;
        timeReduction = 0;
    }

    public override void PassTurn()
    {
        Debug.Log(gameObject.name + " Passed Turn");
        timeReduction = 0;
    }
}
