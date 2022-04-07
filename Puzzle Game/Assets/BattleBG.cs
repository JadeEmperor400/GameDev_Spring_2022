using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBG : MonoBehaviour
{
    [SerializeField]
     private MeshRenderer mR;

    // Start is called before the first frame update
    void Awake()
    {
        mR = GetComponent<MeshRenderer>();
        if (mR == null) {
            Debug.Log("no mesh renderer");
            enabled = false;
            return;
        }
        mR.sortingLayerName = "BattleBG";

    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.BM != null && BattleManager.BM.state != State.Off)
        {
            mR.enabled = false;
        }
        else {
            mR.enabled = true;
        }
    }
}
