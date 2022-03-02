using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCatcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   void OnTriggerEnter2D(Collider2D col )
    {
        Debug.Log("Collisison detected");
        var Line = col.gameObject.GetComponent<Line>();

        if ( Line != null )
        {
            Debug.Log("line script found");
            Line.DisableDrag();
        }


        //another check based on changing the position of the line renderer
    }
}
