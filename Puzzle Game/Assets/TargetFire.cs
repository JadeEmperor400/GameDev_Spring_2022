using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.BM != null && (BattleManager.BM.state == State.EnemyPhase || BattleManager.BM.state == State.PlayerPhase || BattleManager.BM.state == State.Calculating)) {
            EnemyStats es = BattleManager.BM.TargetEnemy;
            transform.position = new Vector3( es.transform.position.x - 1, es.transform.position.y, 0);
            if (es.HP <= 0)
            {
                GetComponent<SpriteRenderer>().color = Color.clear;
            }
            else {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else {
            GetComponent<SpriteRenderer>().color = Color.clear;
        }
    }
}
