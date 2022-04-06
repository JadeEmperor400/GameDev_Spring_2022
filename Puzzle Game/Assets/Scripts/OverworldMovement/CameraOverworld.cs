using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOverworld : MonoBehaviour
{

    public Transform target;

    void Update()
    {
        if (BattleManager.BM != null && BattleManager.BM.state != State.Off) {
            transform.position = new Vector3(0,0,-10);
            return;
        }

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
