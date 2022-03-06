using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private bool dragging = false;

    private int lnXID;
    private int lnYID;

    // Start is called before the first frame update
    void Start()
    {
        
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
            Vector3 mousePosition = Input.mousePosition; // returns "mouse coordinates", must convert to game vector position 
            Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            convertedMousePosition.z = 0; //making sure the z position is 0


           

         
            transform.position = convertedMousePosition;
            Vector3 positionDifference = convertedMousePosition - lineRenderer.transform.position;
            lineRenderer.SetPosition(2, positionDifference);
           

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

    public void DisableDrag()
    {
        dragging = false;
    }

    //GETTERS AND SETTERS 
    public int GetLineXID()
    {
        return lnXID;
    }
    
    public int GetLineYID()
    {
        return lnYID;
    }

    public void SetLineID(int xId, int yId)
    {
        lnXID = xId;
        lnYID = yId;
    }
}
