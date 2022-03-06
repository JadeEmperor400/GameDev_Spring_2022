using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLine : MonoBehaviour
{
    public LineRenderer lr;
    private bool dragging = false;
    Camera camera;
    Vector3 startPos, endPos;

    Vector3 camOffset = new Vector3(0, 0, 10);
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        LineDrag();
    }


    private void LineDrag()
    {
        if (dragging)
        {
            if (lr == null)
            {
                lr = gameObject.AddComponent<LineRenderer>();
            }
            lr.enabled = true;
            lr.positionCount = 2;
            startPos = camera.ScreenToWorldPoint(Input.mousePosition) + camOffset;
            transform.position = startPos;
            lr.SetPosition(0, startPos);
            lr.useWorldSpace = true;
          //  lr.widthCurve = ac;
            lr.numCapVertices = 10;


        }

        if(lr != null)
        if(Vector3.Distance(lr.GetPosition(1), lr.GetPosition(0)) >= 5f)
        {
            Debug.Log("Time to destry ");
                Destroy(gameObject);
        }    

    }


    private void OnMouseDown()
    {


        dragging = true;


    }

    private void OnMouseUp()
    {
        dragging = false;
    }

  

  
}
