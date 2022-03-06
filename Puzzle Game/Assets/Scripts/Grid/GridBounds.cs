using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBounds : MonoBehaviour
{


    [SerializeField]
    private GridManager gridManager;
    private List<GameObject> allTilesInGrid = new List<GameObject>();

    private void Start()
    {
        if (gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
           
          
        }
        allTilesInGrid = gridManager.getAllTilesInGrid();
        
        DetermineBounds();
    }

    private void Update()
    {
        

   
    }

    public void CreateBoxCollider2D()
    {

    }


    public void DetermineBounds()
    {
        var bottomPos = allTilesInGrid[0].transform.position.y;
        var leftPos = allTilesInGrid[0].transform.position.x;
        
        var upPos = allTilesInGrid[allTilesInGrid.Count - 1].transform.position.y;
        var rightPos =allTilesInGrid[allTilesInGrid.Count - 1].transform.position.x;

        float x_Midpoint = (leftPos + rightPos) / 2;
        float y_Midpoint = (upPos + bottomPos) / 2;

        var Midpoint = new Vector3(x_Midpoint, y_Midpoint, 0f);
        Debug.Log(Midpoint);
    }




}
