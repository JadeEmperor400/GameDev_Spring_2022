using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType {
    Player,
    Ally,
    Self,
    Any
}

public enum ActType {
    Attack,
    Heal,
    Status
}

[CreateAssetMenu]
public class EnemyAction : BattleAction
{
    public ActType actionType = ActType.Attack;
    public TargetType targetType = TargetType.Player;
    public float timeReduction = 0;
    public float barrierReduction = 0;
}
