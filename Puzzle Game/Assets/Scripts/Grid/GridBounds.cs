using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBounds : MonoBehaviour
{


    [SerializeField]
    private GridManager gridManager;
    private List<GameObject> allTilesInGrid = new List<GameObject>();
    public BoxCollider2D collider2d; 

    //Code for trying to wrap the grid with a boxcollider to delete any nodes that cross its boundaries, can be used to set up background images instead


    private void Start()
    {
        if (gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
           
          
        }
        allTilesInGrid = gridManager.getAllTilesInGrid();
        
        //DetermineMidpoint();
      // CreateBoxCollider2D();
    }

    private void Update()
    {
        

   
    }

    public void CreateBoxCollider2D()
    {
        //commented out 

        if(collider2d == null)
        {
            collider2d = gameObject.AddComponent<BoxCollider2D>();
        }
        collider2d.offset = DetermineMidpoint();
        collider2d.size = new Vector2(gridManager.getWidth() +0.5f, gridManager.getHeight() +0.5f);
       
        collider2d.isTrigger = true;
       

    }


    public Vector3 DetermineMidpoint()
    {
        var bottomPos = allTilesInGrid[0].transform.position.y;
        var leftPos = allTilesInGrid[0].transform.position.x;
        
        var upPos = allTilesInGrid[allTilesInGrid.Count - 1].transform.position.y;
        var rightPos =allTilesInGrid[allTilesInGrid.Count - 1].transform.position.x;

        float x_Midpoint = (leftPos + rightPos) / 2;
        float y_Midpoint = (upPos + bottomPos) / 2;
       
        var Midpoint = new Vector3(x_Midpoint, y_Midpoint, 0f);
        
        return Midpoint;
    }

   
    public void OnTriggerExit2D()
    {
       
    }

}
