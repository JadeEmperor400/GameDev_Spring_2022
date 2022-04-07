using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
   
    public LineRenderer lineRenderer;
   // private bool dragging;
    private bool isPlaced = false;
    private int lnXID;
    private int lnYID;
     
    private void Start()
    {
        //  dragging = false;
        lineRenderer.sortingLayerName ="Display";
    }

    // Update is called once per frame
    void Update()
    {
        
        
        LineDrag();
    }
    

    private void LineDrag()
    {
        if (isPlaced == true)
            return;
 
            FollowMouse();

    }


    public void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition; // returns "mouse coordinates", must convert to game vector position 
        Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        convertedMousePosition.z = 0; //making sure the z position is 0         
        transform.position = convertedMousePosition;
        Vector3 positionDifference = convertedMousePosition - lineRenderer.transform.position;
        lineRenderer.SetPosition(2, positionDifference);
    }

   private void OnMouseDown()
    {            
    //    dragging = true;
    }

    private void OnMouseUp()
    {
       // dragging = false;
    }

    public void DisableDrag()
    {
       // dragging = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
           // Debug.Log(collision.gameObject.name + " is the collider");
            var tile = collision.GetComponent<Tile>();
            if (tile != null)
            {
                if ((GetLineXID() == tile.GetXID()) && (GetLineYID() == tile.GetYID()))
                {
                    
                    isPlaced = false;
                }
                else if (tile.GetInUseLine() == true)
                {
                    isPlaced = false;
                }
                else
                {
                   
                    SetIsPlaced();
                }
                   
            }
        }
        
    }

    //GETTERS AND SETTERS 
    public void SetIsPlaced()
    {
        
        isPlaced = true;
    }

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
