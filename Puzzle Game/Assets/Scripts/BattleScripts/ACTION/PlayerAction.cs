using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : Action
{
    [Tooltip("Amount of stagger applied to enemies")]
    public int stagger = 1;
    [Tooltip("Attack Type")]
    public AttackType type = AttackType.RED;
}
