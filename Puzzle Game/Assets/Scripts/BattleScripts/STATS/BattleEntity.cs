using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleEntity : MonoBehaviour
{
    [Tooltip ("Max Amount of damage this entity can take in battle")]
    public int MaxHP = 100;
    [SerializeField]
    public int HP = 100;

    public virtual void Init()
    {
        HP = MaxHP;
    }

    public abstract void StartTurn();

    public abstract void PassTurn();

    public virtual void TakeDamage(int damage) {
        if (damage < 0) { damage = 0; }
        HP -= damage;
        if (HP < 0) {
            HP = 0;
        }
    }

    public void HealDamage(int healing) {
        if (healing < 0) {
            healing = 1;
        }

        HP += healing;
        if (HP > MaxHP) {
            HP = MaxHP;
        }
    }
}
