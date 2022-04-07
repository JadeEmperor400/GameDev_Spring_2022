using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBG : MonoBehaviour
{
    [SerializeField]
     private Renderer mR;

    // Start is called before the first frame update
    void Awake()
    {
        mR = GetComponent<Renderer>();
        if (mR == null) {
            Debug.Log("no mesh renderer");
            enabled = false;
            return;
        }
        mR.sortingLayerName = "BattleBG";
        mR.sortingOrder = -1;

    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.BM != null && BattleManager.BM.state != State.Off)
        {
            mR.enabled = true;
        }
        else {
            mR.enabled = false;
        }
    }
}
