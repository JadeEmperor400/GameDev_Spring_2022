using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOverworldOld2 : MonoBehaviour {

    public bool maintainHeight = true;
    [Range(-1, 1)]
    public int adaptPosition;
    float defaultWeidth, defaultHeight;

    Vector3 CameraPos;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Camera.main.aspect);
        Debug.Log(Camera.main.orthographicSize);
        CameraPos = Camera.main.transform.position;
        defaultWeidth = Camera.main.orthographicSize;
        defaultHeight = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (maintainHeight)
        {
            Camera.main.orthographicSize = defaultHeight / Camera.main.aspect;
            Camera.main.transform.position = new Vector3(CameraPos.x, adaptPosition * (defaultWeidth - Camera.main.orthographicSize), CameraPos.z);
        }
        else
        {
            Camera.main.transform.position = new Vector3(adaptPosition * (defaultHeight - Camera.main.orthographicSize * Camera.main.aspect), CameraPos.y, CameraPos.z);
        }
    }
}
