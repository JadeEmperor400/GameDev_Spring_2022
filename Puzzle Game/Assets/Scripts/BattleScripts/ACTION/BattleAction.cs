using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:Puzzle Game/Assets/Puzzle Assets/BattleScripts/ACTION/ActionSO.cs
public abstract class ActionSO : ScriptableObject
=======
public abstract class BattleAction : ScriptableObject
>>>>>>> BattleSystem:Puzzle Game/Assets/Scripts/BattleScripts/ACTION/BattleAction.cs
{
    public int power = 100;

    public float drainRt = 0.0f; //rate of draining based on damage

    public GameObject attackAnim;//ACTION ANIM HERE
}
