using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBAR : MonoBehaviour
{
    public BattleEntity ownerStats;
    public GameObject gauge;

    private void Start()
    {
        ownerStats = GetComponentInParent<BattleEntity>();
        transform.localPosition = new Vector3(0.5f,-1f,0);

        if (ownerStats == null) {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ownerStats == null) {
            gameObject.SetActive(false);
            return;
        }

        gauge.transform.localScale = new Vector3(((float)ownerStats.HP / (float)ownerStats.MaxHP),1,1);    
    }
}
