using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : BattleEntity
{
    [SerializeField]
    protected string enemyName = "Enemy";
    public string EnemyName {
        get { return enemyName; }
    }

    [Tooltip("How much Stagger an enemy can have before skipping their turn")]
    public int staggerLimit = 10;
    [Tooltip("How much Stagger an enemy has on it")]
    public int staggerCount = 0;

    [Tooltip("Damage Multipliers for cerain colors")]
    [SerializeField]
    private float redAff = 1.0f, blueAff = 1.0f, greenAff = 1.0f;

    public float RedAff {
        get { return redAff; }
    }

    public float BlueAff
    {
        get { return blueAff; }
    }

    public float GreenAff
    {
        get { return greenAff; }
    }

    [Tooltip("Amount of turns for enemy to get an extra action")]
    public int extraTurnTimer = 3;
    public int extraCounter = 0;

    public List<EnemyAction> enemyActions = new List<EnemyAction>();

    public override void StartTurn()
    {
        Debug.Log(gameObject.name + " Started Turn");
    }

    public override void PassTurn()
    {
        Debug.Log(gameObject.name + " Passed Turn");

        if (staggerCount >= staggerLimit) {
            staggerCount = 0;
        }

        if (extraCounter >= extraTurnTimer)
        {
            extraCounter = 0;
        }
        else {
            extraCounter++;
        }
    }

    public void OnMouseDown()
    {
        if (BattleManager.BM != null) {
            BattleManager.BM.ChangeTarget(this);
        }
    }

    public virtual EnemyAction SelectAction() {
        if (enemyActions.Count < 1) {
            Debug.Log("This enemy has no actions");
            return null;
        }

        int choose = Random.Range(0, enemyActions.Count);

        return enemyActions[choose];
    }
}
