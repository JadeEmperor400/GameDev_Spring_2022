using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionSO : ScriptableObject
{
    public int power = 100;

    public float drainRt = 0.0f; //rate of draining based on damage

    public GameObject attackAnim;//ACTION ANIM HERE
}
