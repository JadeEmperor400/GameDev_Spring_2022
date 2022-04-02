using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBAR : MonoBehaviour
{
    public BattleEntity ownerStats;
    public GameObject gauge;

    // Update is called once per frame
    void Update()
    {
        gauge.transform.localScale = new Vector3(((float)ownerStats.HP / (float)ownerStats.MaxHP),1,1);    
    }
}
